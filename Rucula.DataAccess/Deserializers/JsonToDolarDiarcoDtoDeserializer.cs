using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToDolarDiarcoDtoDeserializer : IJsonDeserializer<DolarDiarcoDto>
{
    private readonly IJsonValueReader _valueReader;

    public JsonToDolarDiarcoDtoDeserializer(IJsonValueReader valueReader)
        => _valueReader = valueReader;

    public DolarDiarcoDto Deserialize(JsonNode node)
        => new(_valueReader.GetValue<string>(node, "description"));
}
