using Microsoft.Extensions.DependencyInjection;
using Rucula.Domain.Abstractions;

namespace Rucula.Domain.Implementations.IoC
{
    public static class DomainRegistrar
    {
        public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<ITitulosService, TitulosService>()
                .AddSingleton<IChoicesService, ChoicesService>();
    }
}
