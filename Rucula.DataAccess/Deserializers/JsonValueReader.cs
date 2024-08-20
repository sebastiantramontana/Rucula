using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers
{
    internal class JsonValueReader : IJsonValueReader
    {
        public T? GetValue<T>(JsonNode node, string key)
        {
            var nodeValue = node[key];

            return nodeValue is not null 
                ? nodeValue.GetValue<T>() 
                : default;
        }
    }
}
