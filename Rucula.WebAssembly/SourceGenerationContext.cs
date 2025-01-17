using Rucula.Domain.Entities;

namespace Rucula.WebAssembly;

[JsonSourceGenerationOptions(
    WriteIndented = false,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Serialization)]
[JsonSerializable(typeof(ChoicesInfo))]
[JsonSerializable(typeof(WinningChoice))]
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
public partial class SourceGenerationContext : JsonSerializerContext
{
}
