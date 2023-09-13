using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Converters
{
    internal interface IJsonConverter<out T>
    {
        T Convert(JsonNode node);
    }
}
