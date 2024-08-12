using Microsoft.Extensions.DependencyInjection;
using Rucula.Domain.Abstractions;
using Rucula.Infrastructure.JsInterop;

namespace Rucula.Infrastructure.IoC;

public static class InfrastructureRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJsModuleImporter, JsModuleImporter>()
            .AddSingleton<IJsModulesProvider, JsModulesProvider>()
            .AddSingleton<INotifier, JsNotifier>();
}
