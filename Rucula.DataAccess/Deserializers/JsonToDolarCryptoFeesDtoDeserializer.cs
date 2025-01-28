using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal sealed class JsonToDolarCryptoFeesDtoDeserializer : IJsonDeserializer<IEnumerable<DolarCryptoFeesDto>>
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
        => nodeObject.Select(CreateDolarCryptoFeesDto);

    private static DolarCryptoFeesDto CreateDolarCryptoFeesDto(KeyValuePair<string, JsonNode?> exchangeNode)
    {
        var currenciesNode = exchangeNode.Value!.AsObject();
        var currencies = CreateCurrencies(currenciesNode);

        return new(exchangeNode.Key, currencies);
    }

    private static IEnumerable<CryptoCurrencyFeesDto> CreateCurrencies(JsonObject currenciesNode) 
        => currenciesNode.Select(CreateCryptoCurrencyFeesDto);

    private static CryptoCurrencyFeesDto CreateCryptoCurrencyFeesDto(KeyValuePair<string, JsonNode?> currencyNode)
    {
        var blockchainsNode = currencyNode.Value!.AsObject();
        var blockchains = CreateBlockchains(blockchainsNode);

        return new(currencyNode.Key, blockchains);
    }

    private static IEnumerable<BlockchainDto> CreateBlockchains(JsonObject blockchainsNode) 
        => blockchainsNode.Select(CreateBlockchainDto);

    private static BlockchainDto CreateBlockchainDto(KeyValuePair<string, JsonNode?> blockchainNode)
    {
        var blockchainName = blockchainNode.Key;
        var blockchainFee = blockchainNode.Value!.GetValue<double>();

        return new(blockchainName, blockchainFee);
    }
}
