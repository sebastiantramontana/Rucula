﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Implementations.IoC;

namespace Rucula.WebAssembly
{
    public class Program
    {
        private static IJSRuntime _jsRuntime;
        private static ITitulosService _titulosService;

        public static async Task Main(string[] args)
        {
            try
            {
                var builder = CreateWebAssemblyBuilder(args);
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
            //return await Task.FromResult(new TituloIsin[] { new TituloIsin("pepepe", "qeqeqeq", null, null, null, new DateOnly(), new Blue(0, 0)) });
            return await _titulosService.GetCclRankingTitulosIsin();
        }

        private static WebAssemblyHostBuilder CreateWebAssemblyBuilder(string[] args)
            => WebAssemblyHostBuilder.CreateDefault(args);

        private static WebAssemblyHost CreateWebAssemblyHost(WebAssemblyHostBuilder builder)
            => builder.Build();

        private static void InstanceServices(IServiceProvider serviceProvider)
        {
            _jsRuntime = serviceProvider.GetRequiredService<IJSRuntime>();
            _titulosService = serviceProvider.GetRequiredService<ITitulosService>();
        }

        private static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
            DataAccessRegistrar.Register(serviceCollection);
            DomainRegistrar.Register(serviceCollection);
        }
    }
}