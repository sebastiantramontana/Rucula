using Microsoft.Extensions.DependencyInjection;
using Rucula.Domain.Abstractions;

namespace Rucula.Domain.Implementations.IoC;

public static class DomainRegistrar
{
    public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IWinningOptionService, WinningOptionService>()
            .AddSingleton<ITitulosService, TitulosService>()
            .AddSingleton<IWesternUnionService, WesternUnionService>()
            .AddSingleton<IDolarCryptoService, DolarCryptoService>()
            .AddSingleton<IDolarCryptoMaxPriceService, DolarCryptoMaxPriceService>();
}
