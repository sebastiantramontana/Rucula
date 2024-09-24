using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToDolarCryptoDtoDeserializer : IJsonDeserializer<DolarCryptoDto>
{
    private readonly IJsonValueReader _valueReader;

    public JsonToDolarCryptoDtoDeserializer(IJsonValueReader valueReader)
        => _valueReader = valueReader;

    public Optional<DolarCryptoDto> Deserialize(JsonNode? node)
    {
        if (node is null)
            return Optional<DolarCryptoDto>.Empty;

        var compra = _valueReader.GetRequiredValue<string>(node!, "compra");
        var venta = _valueReader.GetRequiredValue<string>(node!, "venta");

        return Optional<DolarCryptoDto>.Sure(new(compra, venta));
    }
}
