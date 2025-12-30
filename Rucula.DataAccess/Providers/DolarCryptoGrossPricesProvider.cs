using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Providers.CryptoYa;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

internal sealed class DolarCryptoGrossPricesProvider(ICryptoYaGrossPricesFetcher fetcher,
                               IJsonDeserializer<IEnumerable<DolarCryptoCurrencyGrossPriceDto>> deserializer,
                               INotifier notifier) : IDolarCryptoGrossPricesProvider
{
    public async Task<IEnumerable<DolarCryptoGrossPrices>> GetGrossPrices(double volume, IEnumerable<string> currencyKeys)
    {
        var fetchTasks = new List<Task<(string CurrencyKey, string Content)>>();

        foreach (var currencyKey in currencyKeys)
        {
            var fetchTask = FetchGrossPrice(CreateParameters(currencyKey, volume));
            fetchTasks.Add(fetchTask);
        }

        var fetchResults = await Task.WhenAll(fetchTasks);
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
        await notifier.Notify($"Consultando precios brutos {parameters.CryptoCurrencyKey}...");
        var content = await fetcher.Fetch(parameters);

        return new(parameters.CryptoCurrencyKey, content);
    }

    private IEnumerable<DolarCryptoCurrencyGrossPriceDto> ConvertContentToCurrencyGrossPriceDto(string content)
    {
        var pricesDto = deserializer.Deserialize(JsonNode.Parse(content)!);
        return pricesDto.HasValue ? pricesDto.Value : [];
    }

    private static CriptoYaGrossPricesFetcherParameters CreateParameters(string currencyKey, double volume)
        => new(currencyKey, volume);
}
