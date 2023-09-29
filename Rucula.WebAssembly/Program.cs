using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Implementations.IoC;
using System.Collections.Generic;

namespace Rucula.WebAssembly
{
    public class Program
    {
        private static ITitulosService _titulosService;
        private static IDolarBlueProvider _dolarBlueProvider;

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
        public static async Task<IEnumerable<TituloIsin>> GetTitulosRanking()
        {
            try
            {
                return await _titulosService.GetCclRankingTitulosIsin();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Enumerable.Empty<TituloIsin>();
            }
        }

        [JSInvokable]
        public static async Task<Blue> GetDolarBlue()
        {
            return await _dolarBlueProvider.GetCurrentBlue();
        }

        private static WebAssemblyHostBuilder CreateWebAssemblyBuilder(string[] args)
            => WebAssemblyHostBuilder.CreateDefault(args);

        private static WebAssemblyHost CreateWebAssemblyHost(WebAssemblyHostBuilder builder)
            => builder.Build();

        private static void InstanceServices(IServiceProvider serviceProvider)
        {
            _titulosService = serviceProvider.GetRequiredService<ITitulosService>();
            _dolarBlueProvider = serviceProvider.GetRequiredService<IDolarBlueProvider>();
        }

        private static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
            DataAccessRegistrar.Register(serviceCollection);
            DomainRegistrar.Register(serviceCollection);
        }
    }
}