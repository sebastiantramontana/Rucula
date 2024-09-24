using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToTituloDtoDeserializer : IJsonDeserializer<TituloDto>
{
    private readonly IJsonValueReader _valueReader;

    public JsonToTituloDtoDeserializer(IJsonValueReader valueReader)
    {
        _valueReader = valueReader;
    }

    public Optional<TituloDto> Deserialize(JsonNode? node)
    {
        if (node is null)
            return Optional<TituloDto>.Empty;

        var simbolo = _valueReader.GetRequiredValue<string>(node!, "symbol");
        var precioCompra = _valueReader.GetRequiredValue<double>(node!, "bidPrice");
        var precioVenta = _valueReader.GetRequiredValue<double>(node!, "offerPrice");
        var parking = _valueReader.GetRequiredValue<string>(node!, "settlementType");
        var moneda = _valueReader.GetRequiredValue<string>(node!, "denominationCcy");

        return Optional<TituloDto>.Sure(new TituloDto(simbolo, precioCompra, precioVenta, parking, moneda));
    }
}
