using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers
{
    internal interface IJsonDeserializer<out T>
    {
        T Deserialize(JsonNode node);
    }
}
