using Rucula.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Rucula.WebAssembly;

public class OptionalJsonConverter<T>(JsonTypeInfo<T> jsonTypeInfo) : JsonConverter<Optional<T>>
{
    public override Optional<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Asegurar de que el token actual es el inicio de un objeto
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token");
        }

        var hasValue = true;
        var isEmpty = false;
        T? value = default;

        // Leer las propiedades manualmente
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break; // Fin del objeto
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException("Expected PropertyName token");
            }

            var propertyName = reader.GetString();
            reader.Read(); // Mover al valor de la propiedad

            switch (propertyName)
            {

                case "hasValue":
                    hasValue = reader.GetBoolean();
                    break;
                case "isEmpty":
                    isEmpty = reader.GetBoolean();
                    break;
                case "value":
                    value = JsonSerializer.Deserialize(ref reader, jsonTypeInfo);
                    break;
                default:
                    throw new JsonException($"Unexpected property: {propertyName}");
            }
        }

        return hasValue == isEmpty
            ? throw new JsonException($"HasValue y IsEmpty no pueden tener el mismo valor para un Optional")
            : hasValue ? Optional<T>.Sure(value!) : Optional<T>.Empty;
    }

    public override void Write(Utf8JsonWriter writer, Optional<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("hasValue", value.HasValue);
        writer.WriteBoolean("isEmpty", value.IsEmpty);
        writer.WritePropertyName("value");
        writer.WriteRawValue(JsonSerializer.Serialize(value.Value, jsonTypeInfo));
        writer.WriteEndObject();
    }
}
