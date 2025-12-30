using Rucula.Domain.Entities.Parameters;
using Rucula.WebAssembly.Parameters;
using System.Runtime.Versioning;

namespace Rucula.WebAssembly.IoC;

[SupportedOSPlatform("browser")]
public static class WebAssemblyRegistrar
{
    public static IServiceCollection AddRuculaWebAssembly(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJSObjectConverter<BondCommissions>, BondCommissionsJSObjectConverter>()
            .AddSingleton<IJSObjectConverter<WesternUnionParameters>, WesternUnionParametersJSObjectConverter>()
            .AddSingleton<IJSObjectConverter<DolarCryptoParameters>, DolarCryptoParametersJSObjectConverter>()
            .AddSingleton<IParametersJSObjectConverter, ParametersJSObjectConverter>();
}
