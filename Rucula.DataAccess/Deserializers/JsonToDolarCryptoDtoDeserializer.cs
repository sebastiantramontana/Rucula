using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToDolarCryptoDtoDeserializer : IJsonDeserializer<DolarCryptoDto>
{
    private readonly IJsonValueReader _valueReader;

    public JsonToDolarCryptoDtoDeserializer(IJsonValueReader valueReader)
    {
        _valueReader = valueReader;
    }

    public DolarCryptoDto Deserialize(JsonNode node)
    {
        var compra = _valueReader.GetRequiredValue<string>(node, "compra");
        var venta = _valueReader.GetRequiredValue<string>(node, "venta");

        return new DolarCryptoDto(compra, venta);
    }
}
