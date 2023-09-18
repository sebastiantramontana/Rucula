using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers
{
    internal interface IJsonValueReader
    {
        T GetValue<T>(JsonNode node, string key);
    }
}
