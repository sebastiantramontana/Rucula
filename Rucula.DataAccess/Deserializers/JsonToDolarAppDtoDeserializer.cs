using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal sealed class JsonToDolarAppDtoDeserializer(IJsonValueReader valueReader) : IJsonDeserializer<DolarAppDto>
{
    private const string BidProperty = "bid";

    public Optional<DolarAppDto> Deserialize(JsonNode? node)
    {
        var array = node?.AsArray();
        var firstNode = array?[0];

        var grossPrice = GetNodeValue<string>(firstNode, BidProperty);
        return grossPrice.HasValue
            ? Optional<DolarAppDto>.Sure(new(double.Parse(grossPrice.Value)))
            : Optional<DolarAppDto>.Empty;
    }

    private Optional<T> GetNodeValue<T>(JsonNode? node, string property)
        => valueReader.GetValue<T>(node, property);
}
