using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToBlueDtoDeserializer : IJsonDeserializer<BlueDto>
{
    private readonly IJsonValueReader _valueReader;

    public JsonToBlueDtoDeserializer(IJsonValueReader valueReader)
        => _valueReader = valueReader;

    public Optional<BlueDto> Deserialize(JsonNode? node)
    {
        if (node is null)
            return Optional<BlueDto>.Empty;

        var compra = _valueReader.GetRequiredValue<string>(node!, "compra");
        var venta = _valueReader.GetRequiredValue<string>(node!, "venta");

        return Optional<BlueDto>.Sure(new(compra, venta));
    }
}
