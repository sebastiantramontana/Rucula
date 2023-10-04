using Microsoft.Extensions.DependencyInjection;
using Rucula.Domain.Abstractions;

namespace Rucula.Domain.Implementations.IoC
{
    public static class DomainRegistrar
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<ITitulosService, TitulosService>()
                .AddSingleton<IChoicesService, ChoicesService>();
        }
    }
}
