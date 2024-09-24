using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal interface IJsonDeserializer<T>
{
    Optional<T> Deserialize(JsonNode? node);
}
