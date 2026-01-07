using Microsoft.Extensions.DependencyInjection;
using Rucula.Domain.Abstractions;

namespace Rucula.Domain.Implementations.IoC;

public static class DomainRegistrar
{
    public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IWinningChoiceService, WinningChoiceService>()
            .AddSingleton<ITitulosService, TitulosService>()
            .AddSingleton<IChoicesService, ChoicesService>()
            .AddSingleton<IRestartingPeriodicRunnerService, RestartingPeriodicRunnerService>()
            .AddSingleton<IWesternUnionService, WesternUnionService>()
            .AddSingleton<IDolarCryptoService, DolarCryptoService>()
            .AddSingleton<IDolarCryptoMaxPriceService, DolarCryptoMaxPriceService>()
            .AddSingleton<IPeriodicRunnerService, PeriodicRunnerService>();
}
