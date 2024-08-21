using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToBlueDtoDeserializer : IJsonDeserializer<BlueDto>
{
    private readonly IJsonValueReader _valueReader;

    public JsonToBlueDtoDeserializer(IJsonValueReader valueReader)
    {
        _valueReader = valueReader;
    }

    public BlueDto Deserialize(JsonNode node)
    {
        var compra = _valueReader.GetRequiredValue<string>(node, "compra");
        var venta = _valueReader.GetRequiredValue<string>(node, "venta");

        return new BlueDto(compra, venta);
    }
}
