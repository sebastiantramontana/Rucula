using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToDolarCryptoCurrencyGrossPriceDto : IJsonDeserializer<IEnumerable<DolarCryptoCurrencyGrossPriceDto>>
{
    private readonly IJsonValueReader _jsonValueReader;

    public JsonToDolarCryptoCurrencyGrossPriceDto(IJsonValueReader jsonValueReader)
    {
        _jsonValueReader = jsonValueReader;
    }

    public Optional<IEnumerable<DolarCryptoCurrencyGrossPriceDto>> Deserialize(JsonNode? node)
    {
        if (node is null)
        {
            return Optional<IEnumerable<DolarCryptoCurrencyGrossPriceDto>>.Empty;
        }

        var nodeObject = node.AsObject();
        var grossPriceDtos = CreateGrossPriceDtos(nodeObject);

        return Optional<IEnumerable<DolarCryptoCurrencyGrossPriceDto>>.Sure(grossPriceDtos);
    }

    private IEnumerable<DolarCryptoCurrencyGrossPriceDto> CreateGrossPriceDtos(JsonObject nodeObject)
        => nodeObject.Select(CreateGrossPriceDto);

    private DolarCryptoCurrencyGrossPriceDto CreateGrossPriceDto(KeyValuePair<string, JsonNode?> exchangeNode)
    {
        var valuesNode = exchangeNode.Value!.AsObject();
        var totalBid = _jsonValueReader.GetRequiredValue<double>(valuesNode, "totalBid");

        return new(exchangeNode.Key, totalBid);
    }
}
