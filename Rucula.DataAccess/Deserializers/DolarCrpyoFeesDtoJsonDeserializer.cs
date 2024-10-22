using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class DolarCrpyoFeesDtoJsonDeserializer : IJsonDeserializer<IEnumerable<DolarCryptoFeesDto>>
{
    public Optional<IEnumerable<DolarCryptoFeesDto>> Deserialize(JsonNode? node)
    {
        if (node is null)
        {
            return Optional<IEnumerable<DolarCryptoFeesDto>>.Empty;
        }

        var nodeObject = node.AsObject();
        var fees = CreateCryptoFees(nodeObject);

        return Optional<IEnumerable<DolarCryptoFeesDto>>.Sure(fees);
    }

    private static IEnumerable<DolarCryptoFeesDto> CreateCryptoFees(JsonObject nodeObject)
    {
        var fees = new List<DolarCryptoFeesDto>();

        foreach (var exchangeNode in nodeObject)
        {
            fees.Add(CreateDolarCryptoFeesDto(exchangeNode));
        }

        return fees;
    }

    private static DolarCryptoFeesDto CreateDolarCryptoFeesDto(KeyValuePair<string, JsonNode?> exchangeNode)
    {
        var currenciesNode = exchangeNode.Value!.AsObject();
        var currencies = CreateCurrencies(currenciesNode);

        return new(exchangeNode.Key, currencies);
    }

    private static IEnumerable<CryptoCurrencyFeesDto> CreateCurrencies(JsonObject currenciesNode)
    {
        var currencies = new List<CryptoCurrencyFeesDto>();

        foreach (var currencyNode in currenciesNode)
        {
            currencies.Add(CreateCryptoCurrencyFeesDto(currencyNode));
        }

        return currencies;
    }

    private static CryptoCurrencyFeesDto CreateCryptoCurrencyFeesDto(KeyValuePair<string, JsonNode?> currencyNode)
    {
        var blockchainsNode = currencyNode.Value!.AsObject();
        var blockchains = CreateBlockchains(blockchainsNode);

        return new(currencyNode.Key, blockchains);
    }

    private static IEnumerable<BlockchainDto> CreateBlockchains(JsonObject blockchainsNode)
    {
        var blockchains = new List<BlockchainDto>();

        foreach (var blockchainNode in blockchainsNode)
        {
            blockchains.Add(CreateBlockchainDto(blockchainNode));
        }

        return blockchains;
    }

    private static BlockchainDto CreateBlockchainDto(KeyValuePair<string, JsonNode?> blockchainNode)
    {
        var blockchainName = blockchainNode.Key;
        var blockchainFee = blockchainNode.Value!.GetValue<double>();

        return new(blockchainName, blockchainFee);
    }
}
