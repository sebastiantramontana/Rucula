using Microsoft.Extensions.DependencyInjection;

namespace Rucula.Application;

public static class ApplicationRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IBestOptionService, BestOptionService>()
            .AddSingleton<IRestartingPeriodicRunnerService, RestartingPeriodicRunnerService>()
            .AddSingleton<IPeriodicRunnerService, PeriodicRunnerService>();
}