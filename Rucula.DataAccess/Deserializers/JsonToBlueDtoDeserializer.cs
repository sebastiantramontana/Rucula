using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal sealed class JsonToBlueDtoDeserializer(IJsonValueReader valueReader) : IJsonDeserializer<BlueDto>
{
    public Optional<BlueDto> Deserialize(JsonNode? node)
    {
        if (node is null)
        {
            return Optional<BlueDto>.Empty;
        }

        var compra = valueReader.GetRequiredValue<string>(node!, "compra");
        var venta = valueReader.GetRequiredValue<string>(node!, "venta");

        return Optional<BlueDto>.Sure(new(compra, venta));
    }
}
