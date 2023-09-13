using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Converters
{
    internal interface IJsonValueReader
    {
        T GetValue<T>(JsonNode node, string key);
    }
}
