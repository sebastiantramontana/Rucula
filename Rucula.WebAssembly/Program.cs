using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Implementations.IoC;
using Rucula.Infrastructure.IoC;

namespace Rucula.WebAssembly;

public class Program
{
    private static IChoicesService _choicesService = default!;
    private ChoicesInfo _currentChoices = ChoicesInfo.NoChoices;

    public static async Task Main(string[] args)
    {
        try
        {
            var builder = CreateWebAssemblyBuilder(args);

            _ = builder.Logging.SetMinimumLevel(LogLevel.Warning);

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
    public static async Task<ChoicesInfo> GetChoices(BondCommissions bondCommissions)
    {
        var choices = ChoicesInfo.NoChoices;

        try
        {
            choices = await _choicesService.GetChoices(bondCommissions).ConfigureAwait(false);
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
        => _choicesService = serviceProvider.GetRequiredService<IChoicesService>();

    private static void Register(IServiceCollection serviceCollection)
        => serviceCollection
            .AddHttpClient()
            .AddDataAccess()
            .AddDomain()
            .AddInfrastructure();
}