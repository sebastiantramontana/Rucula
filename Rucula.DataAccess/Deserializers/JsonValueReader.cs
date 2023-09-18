using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers
{
    internal class JsonValueReader : IJsonValueReader
    {
        public T GetValue<T>(JsonNode node, string key) => node[key]!.GetValue<T>();
    }
}
