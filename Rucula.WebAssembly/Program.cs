using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Implementations.IoC;

namespace Rucula.WebAssembly
{
    public class Program
    {
        private static IChoicesService? _choicesService;

        public static async Task Main(string[] args)
        {
            try
            {
                var builder = CreateWebAssemblyBuilder(args);

                builder.Logging.SetMinimumLevel(LogLevel.Warning);

                await using var host = CreateWebAssemblyHost(builder);

                Register(builder.Services);
                InstanceServices(builder.Services.BuildServiceProvider());

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [JSInvokable]
        public static async Task<ChoicesInfo> GetChoices()
        {
            var choices = ChoicesInfo.NoChoices;

            try
            {
                choices = await _choicesService!.GetChoices();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return choices;
        }

        private static WebAssemblyHostBuilder CreateWebAssemblyBuilder(string[] args)
            => WebAssemblyHostBuilder.CreateDefault(args);

        private static WebAssemblyHost CreateWebAssemblyHost(WebAssemblyHostBuilder builder)
            => builder.Build();

        private static void InstanceServices(IServiceProvider serviceProvider)
        {
            _choicesService = serviceProvider.GetRequiredService<IChoicesService>();
        }

        private static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
            DataAccessRegistrar.Register(serviceCollection);
            DomainRegistrar.Register(serviceCollection);
        }
    }
}