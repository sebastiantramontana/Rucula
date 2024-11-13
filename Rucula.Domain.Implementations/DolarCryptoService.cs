using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal class DolarCryptoService : IDolarCryptoService
{
    private const string UsdcKey = "USDC";
    private const string UsdtKey = "USDT";
    private const string DaiKey = "DAI";
    private readonly string[] CurrencyKeys = [UsdcKey, UsdtKey, DaiKey];

    private readonly IDolarCryptoGrossPricesProvider _grossPricesProvider;
    private readonly IDolarCryptoFeesProvider _feesProvider;
    private readonly IDolarCryptoMaxPriceService _maxPriceService;

    public DolarCryptoService(IDolarCryptoGrossPricesProvider grossPricesProvider, IDolarCryptoFeesProvider feesProvider, IDolarCryptoMaxPriceService maxPriceService)
    {
        _grossPricesProvider = grossPricesProvider;
        _feesProvider = feesProvider;
        _maxPriceService = maxPriceService;
    }

    public async Task<IEnumerable<DolarCryptoPrices>> GetPriceRanking(DolarCryptoParameters cryptoParameters)
    {
        var (fees, allGrossPrices) = await GetDataFromProviders(cryptoParameters.Volume);

        return fees
            .Select(fee =>
            {
                var grossPrices = GetGrossPricesByExchange(allGrossPrices, fee.ExchangeName);

                if (grossPrices is null)
                {
                    return Optional<DolarCryptoPrices>.Empty;
                }

                var grossUsdc = GetExchangeGrossPriceByCurrency(grossPrices, UsdcKey);
                var grossUsdt = GetExchangeGrossPriceByCurrency(grossPrices, UsdtKey);
                var grossDai = GetExchangeGrossPriceByCurrency(grossPrices, DaiKey);

                if (grossUsdc.IsEmpty && grossUsdt.IsEmpty && grossDai.IsEmpty)
                {
                    return Optional<DolarCryptoPrices>.Empty;
                }

                var allNetPrices = CalculateAllNetPrices(cryptoParameters.Volume, grossUsdc, grossUsdt, grossDai, fee.CryptoCurrencyFees);

                if (allNetPrices.Count == 0)
                {
                    return Optional<DolarCryptoPrices>.Empty;
                }

                var rankedAllNetPrices = OrderAllNetPrices(allNetPrices);

                return Optional<DolarCryptoPrices>.Sure(new DolarCryptoPrices(fee.ExchangeName, grossUsdc, grossUsdt, grossDai, rankedAllNetPrices));
            })
            .Where(prices => prices.HasValue)
            .Select(prices => prices.Value)
            .OrderByDescending(prices => GetTopNetPrice(prices.DolarCryptoNetPrices.First()))
            .ToArray();
    }

    private static IEnumerable<DolarCryptoNetPrices> OrderAllNetPrices(IEnumerable<DolarCryptoNetPrices> allNetPrices)
        => allNetPrices.OrderByDescending(p => p.TopNetPrice.NetPrice);

    private static double GetTopNetPrice(DolarCryptoNetPrices prices)
        => prices.TopNetPrice.NetPrice;

    private static DolarCryptoGrossPrices? GetGrossPricesByExchange(IEnumerable<DolarCryptoGrossPrices> allGrossPrices, string exchangeName)
    {
        var normalizedExchangeName = NormalizeExchangeName(exchangeName);
        return allGrossPrices.SingleOrDefault(g => CompareExchanges(g.ExchangeName, normalizedExchangeName));
    }

    private static string NormalizeExchangeName(string exchangeName)
        => (exchangeName?.ToLowerInvariant() ?? string.Empty)
            .Replace(" ", string.Empty);

    private static bool CompareExchanges(string exchangeCode, string normalizedExchangeName)
    {
        exchangeCode = exchangeCode?.ToLowerInvariant() ?? string.Empty;
        return exchangeCode.Equals(normalizedExchangeName, StringComparison.OrdinalIgnoreCase);
    }

    private static Optional<double> GetExchangeGrossPriceByCurrency(DolarCryptoGrossPrices exchangeGrossPrices, string currencyKey)
        => Optional<double>.Maybe(exchangeGrossPrices
                                    .GrossPrices
                                    .SingleOrDefault(p => p.CurrencyKey == currencyKey)
                                    ?.GrossPrice);

    private async Task<(IEnumerable<DolarCryptoFees> Fees, IEnumerable<DolarCryptoGrossPrices> GrossPrices)> GetDataFromProviders(double volume)
    {
        var feesTask = _feesProvider.Get();
        var grossPricesTask = _grossPricesProvider.GetGrossPrices(volume, CurrencyKeys);

        await Task.WhenAll(feesTask, grossPricesTask).ConfigureAwait(false);

        var fees = await feesTask.ConfigureAwait(false);
        var grossPrices = await grossPricesTask.ConfigureAwait(false);

        return (fees, grossPrices);
    }

    private List<DolarCryptoNetPrices> CalculateAllNetPrices(double volume, Optional<double> grossUsdc, Optional<double> grossUsdt, Optional<double> grossDai, IEnumerable<CryptoCurrencyFees> fees)
    {
        var blockChains = fees
            .SelectMany(f => f.BlockchainFees)
            .DistinctBy(b => b.Blockchain)
            .Select(g => g.Blockchain);

        var usdcBlockchainFees = GetCurrencyBlockchainFees(UsdcKey, fees);
        var usdtBlockchainFees = GetCurrencyBlockchainFees(UsdtKey, fees);
        var daiBlockchainFees = GetCurrencyBlockchainFees(DaiKey, fees);

        if (!usdcBlockchainFees.Any() && !usdtBlockchainFees.Any() && !daiBlockchainFees.Any())
        {
            return [];
        }

        var allNetPrices = new List<DolarCryptoNetPrices>();

        foreach (var blockchain in blockChains)
        {
            var netUsdc = CreateCalculatedNetPriceByBlockchain(blockchain, usdcBlockchainFees, grossUsdc, volume);
            var netUsdt = CreateCalculatedNetPriceByBlockchain(blockchain, usdtBlockchainFees, grossUsdt, volume);
            var netDai = CreateCalculatedNetPriceByBlockchain(blockchain, daiBlockchainFees, grossDai, volume);

            if (netUsdc.HasValue || netUsdt.HasValue || netDai.HasValue)
            {
                var topPrice = _maxPriceService.MaxNetPrice(netUsdc, netUsdt, netDai);
                var netPrices = new DolarCryptoNetPrices(blockchain, netUsdc, netUsdt, netDai, topPrice!);

                allNetPrices.Add(netPrices);
            }
        }

        return allNetPrices;
    }

    private static Optional<DolarCryptoNetPrice> CreateCalculatedNetPriceByBlockchain(Blockchain blockchain, IEnumerable<CurrencyBlockchainFee> currencyBlockchainFees, Optional<double> grossPrice, double volume)
    {
        if (grossPrice.IsEmpty)
        {
            return Optional<DolarCryptoNetPrice>.Empty;
        }

        var currencyBlokchainFee = GetCurrencyFeeByBlockchain(currencyBlockchainFees, blockchain);

        if (currencyBlokchainFee is null)
        {
            return Optional<DolarCryptoNetPrice>.Empty;
        }

        var netPrice = CalculateNetPrice(volume, grossPrice.Value, currencyBlokchainFee.Fee);

        return Optional<DolarCryptoNetPrice>.Sure(new DolarCryptoNetPrice(netPrice, currencyBlokchainFee.Fee));
    }

    private static CurrencyBlockchainFee? GetCurrencyFeeByBlockchain(IEnumerable<CurrencyBlockchainFee> currencyBlockchainFees, Blockchain blockchain)
        => currencyBlockchainFees.SingleOrDefault(bf => bf.Blockchain == blockchain);

    private static double CalculateNetPrice(double volume, double grossPrice, double fee)
        => (volume * grossPrice) / (volume + fee);

    private static IEnumerable<CurrencyBlockchainFee> GetCurrencyBlockchainFees(string currencyKey, IEnumerable<CryptoCurrencyFees> fees)
        => fees
            .SingleOrDefault(c => c.CryptoCurrencyKey == currencyKey)
            ?.BlockchainFees
            ?? [];
}
