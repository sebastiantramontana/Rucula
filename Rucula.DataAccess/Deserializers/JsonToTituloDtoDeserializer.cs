using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToTituloDtoDeserializer(IJsonValueReader valueReader) : IJsonDeserializer<TituloDto>
{
    public Optional<TituloDto> Deserialize(JsonNode? node)
    {
        if (node is null)
        {
            return Optional<TituloDto>.Empty;
        }

        var simbolo = valueReader.GetRequiredValue<string>(node!, "symbol");
        var precioCompra = valueReader.GetRequiredValue<double>(node!, "bidPrice");
        var precioVenta = valueReader.GetRequiredValue<double>(node!, "offerPrice");
        var parking = valueReader.GetRequiredValue<string>(node!, "settlementType");
        var moneda = valueReader.GetRequiredValue<string>(node!, "denominationCcy");

        return Optional<TituloDto>.Sure(new TituloDto(simbolo, precioCompra, precioVenta, parking, moneda));
    }
}
