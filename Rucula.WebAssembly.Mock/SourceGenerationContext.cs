using Rucula.Domain.Entities;
using Rucula.WebAssembly.Mock;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rucula.WebAssembly;

[JsonSourceGenerationOptions(
    WriteIndented = false,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default)]
[JsonSerializable(typeof(OptionsInfo))]
[JsonSerializable(typeof(WinningOption))]
[JsonSerializable(typeof(IEnumerable<TituloIsin>))]
[JsonSerializable(typeof(TituloIsin))]
[JsonSerializable(typeof(Optional<Blue>))]
[JsonSerializable(typeof(Blue))]
[JsonSerializable(typeof(Optional<DolarWesternUnion>))]
[JsonSerializable(typeof(DolarWesternUnion))]
[JsonSerializable(typeof(IEnumerable<DolarCryptoPrices>))]
[JsonSerializable(typeof(DolarCryptoPrices))]
[JsonSerializable(typeof(Titulo))]
[JsonSerializable(typeof(Parking))]
[JsonSerializable(typeof(Moneda))]
[JsonSerializable(typeof(DateOnly))]
[JsonSerializable(typeof(IEnumerable<DolarCryptoNetPrices>))]
[JsonSerializable(typeof(DolarCryptoNetPrices))]
[JsonSerializable(typeof(Optional<DolarCryptoNetPrice>))]
[JsonSerializable(typeof(DolarCryptoNetPrice))]
[JsonSerializable(typeof(Blockchain))]
[JsonSerializable(typeof(DolarApp))]
public partial class SourceGenerationContext : JsonSerializerContext
{
    private static JsonSerializerOptions? _ruculaOptions = null;

    private static JsonSerializerOptions CreateOptions()
    {
        var options = new JsonSerializerOptions(Default.Options);

        options.Converters.Add(new OptionalJsonConverter<Blue>(Default.Blue));
        options.Converters.Add(new OptionalJsonConverter<double>(Default.Double));
        options.Converters.Add(new OptionalJsonConverter<DolarWesternUnion>(Default.DolarWesternUnion));
        options.Converters.Add(new OptionalJsonConverter<DolarCryptoNetPrice>(Default.DolarCryptoNetPrice));
        options.Converters.Add(new OptionalJsonConverter<DolarApp>(Default.DolarApp));

        return options;
    }

    public static JsonSerializerOptions RuculaOptions
        => _ruculaOptions ??= CreateOptions();
}