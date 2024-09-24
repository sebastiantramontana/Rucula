using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToDolarDiarcoDtoDeserializer : IJsonDeserializer<DolarDiarcoDto>
{
    private readonly IJsonValueReader _valueReader;

    public JsonToDolarDiarcoDtoDeserializer(IJsonValueReader valueReader)
        => _valueReader = valueReader;

    public Optional<DolarDiarcoDto> Deserialize(JsonNode? node)
        => node is not null
            ? Optional<DolarDiarcoDto>.Sure(new(_valueReader.GetRequiredValue<string>(node!, "description")))
            : Optional<DolarDiarcoDto>.Empty;
}
