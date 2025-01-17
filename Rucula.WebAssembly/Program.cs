using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Implementations.IoC;
using Rucula.Infrastructure;
using Rucula.Infrastructure.IoC;
using Rucula.WebAssembly.IoC;
using Rucula.WebAssembly.Parameters;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rucula.WebAssembly;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
[SupportedOSPlatform("browser")]
public partial class Program
{
    private static IChoicesService _choicesService = default!;
    private static ChoicesInfo _currentChoices = ChoicesInfo.NoChoices;
    private static IParametersJSObjectConverter _parametersJSObjectConverter = default!;

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

    [JSExport]
    [return: JSMarshalAs<Promise<JSType.String>>]
    public static async Task<string> GetChoices(JSObject bondCommissionsJSObject, JSObject westernUnionParametersJSObject, JSObject dolarCryptoParametersJSObject)
    {
        try
        {
            await WaitAllDependenciesAreReady();

            var parameters = _parametersJSObjectConverter.GetParameters(bondCommissionsJSObject, westernUnionParametersJSObject, dolarCryptoParametersJSObject);

            _currentChoices = await _choicesService.GetChoices(parameters.BondCommissions, parameters.WesternUnionParameters, parameters.DolarCryptoParameters).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return JsonSerializer.Serialize(_currentChoices, SourceGenerationContext.Default.ChoicesInfo);
    }

    [JSExport]
    [return: JSMarshalAs<Promise<JSType.String>>]
    public static async Task<string> RecalculateChoices(JSObject bondCommissionsJSObject, JSObject westernUnionParametersJSObject, JSObject dolarCryptoParametersJSObject)
    {
        try
        {
            await WaitAllDependenciesAreReady();

            var parameters = _parametersJSObjectConverter.GetParameters(bondCommissionsJSObject, westernUnionParametersJSObject, dolarCryptoParametersJSObject);

            _currentChoices = await _choicesService.RecalculateChoices(_currentChoices, parameters.BondCommissions, parameters.WesternUnionParameters, parameters.DolarCryptoParameters).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return JsonSerializer.Serialize(_currentChoices, SourceGenerationContext.Default.ChoicesInfo);
    }

    private static async Task WaitAllDependenciesAreReady()
    {
        await NullDependencyAwaiter.AwaitToNotNull(() => _choicesService);
        await NullDependencyAwaiter.AwaitToNotNull(() => _parametersJSObjectConverter);
    }

    private static WebAssemblyHostBuilder CreateWebAssemblyBuilder(string[] args)
        => WebAssemblyHostBuilder.CreateDefault(args);

    private static WebAssemblyHost CreateWebAssemblyHost(WebAssemblyHostBuilder builder)
        => builder.Build();

    private static void InstanceServices(IServiceProvider serviceProvider)
    {
        _choicesService = serviceProvider.GetRequiredService<IChoicesService>();
        _parametersJSObjectConverter = serviceProvider.GetRequiredService<IParametersJSObjectConverter>();
    }

    private static void Register(IServiceCollection serviceCollection)
        => serviceCollection
            .AddHttpClient()
            .AddDataAccess()
            .AddDomain()
            .AddInfrastructure()
            .AddRuculaWebAssembly();
}