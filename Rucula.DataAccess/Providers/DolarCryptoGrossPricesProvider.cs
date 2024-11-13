using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Providers.CryptoYa;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

internal class DolarCryptoGrossPricesProvider : IDolarCryptoGrossPricesProvider
{
    private readonly ICryptoYaGrossPricesFetcher _fetcher;
    private readonly IJsonDeserializer<IEnumerable<DolarCryptoCurrencyGrossPriceDto>> _deserializer;
    private readonly INotifier _notifier;

    public DolarCryptoGrossPricesProvider(ICryptoYaGrossPricesFetcher fetcher,
                                   IJsonDeserializer<IEnumerable<DolarCryptoCurrencyGrossPriceDto>> deserializer,
                                   INotifier notifier)
    {
        _fetcher = fetcher;
        _deserializer = deserializer;
        _notifier = notifier;
    }

    public async Task<IEnumerable<DolarCryptoGrossPrices>> GetGrossPrices(double volume, IEnumerable<string> currencyKeys)
    {
        var fetchTasks = new List<Task<(string CurrencyKey, string Content)>>();

        foreach (var currencyKey in currencyKeys)
        {
            var fetchTask = FetchGrossPrice(CreateParameters(currencyKey, volume));
            fetchTasks.Add(fetchTask);
        }

        var fetchResults = await Task.WhenAll(fetchTasks).ConfigureAwait(false);
        var grossPrices = new Dictionary<string, DolarCryptoGrossPrices>();

        foreach (var result in fetchResults)
        {
            var pricesDto = ConvertContentToCurrencyGrossPriceDto(result.Content);
            AcumulateMappedGrossPrices(grossPrices, result.CurrencyKey, pricesDto);
        }

        return grossPrices.Values;
    }

    private static void AcumulateMappedGrossPrices(Dictionary<string, DolarCryptoGrossPrices> exchanges, string currencyKey, IEnumerable<DolarCryptoCurrencyGrossPriceDto> pricesDto)
    {
        foreach (var price in pricesDto)
        {
            if (!exchanges.TryGetValue(price.ExchangeName, out var currentPrices))
            {
                currentPrices = new(price.ExchangeName, []);
                _ = exchanges.TryAdd(price.ExchangeName, currentPrices);
            }

            var newGrossPrices = currentPrices.GrossPrices.Append(new(currencyKey, price.TotalBid));
            var newPrices = currentPrices with { GrossPrices = newGrossPrices };

            exchanges[price.ExchangeName] = newPrices;
        }
    }

    private async Task<(string CurrencyKey, string Content)> FetchGrossPrice(CriptoYaGrossPricesFetcherParameters parameters)
    {
        await _notifier.NotifyProgress($"Consultando precios brutos {parameters.CryptoCurrencyKey}...").ConfigureAwait(false);
        var content = await _fetcher.Fetch(parameters).ConfigureAwait(false);

        return new(parameters.CryptoCurrencyKey, content);
    }

    private IEnumerable<DolarCryptoCurrencyGrossPriceDto> ConvertContentToCurrencyGrossPriceDto(string content)
    {
        var pricesDto = _deserializer.Deserialize(JsonNode.Parse(content)!);
        return pricesDto.HasValue ? pricesDto.Value : [];
    }

    private static CriptoYaGrossPricesFetcherParameters CreateParameters(string currencyKey, double volume)
        => new(currencyKey, volume);
}
