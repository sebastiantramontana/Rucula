using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal sealed class JsonValueReader : IJsonValueReader
{
    public T GetRequiredValue<T>(JsonNode node, string key)
        => node[key]!.GetValue<T>();

    public Optional<T> GetValue<T>(JsonNode? node, string key)
        => node?[key] is null
        ? Optional<T>.Empty
        : Optional<T>.Sure(GetRequiredValue<T>(node, key));
}
