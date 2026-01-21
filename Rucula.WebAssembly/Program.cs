using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Rucula.Application;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Implementations.IoC;
using Rucula.Infrastructure;
using Rucula.Presentation.IoC;
using Rucula.Presentation.Presenters;
using Rucula.WebAssembly.IoC;
using Rucula.WebAssembly.Parameters;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace Rucula.WebAssembly;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
[SupportedOSPlatform("browser")]
public partial class Program
{
    private static IServiceProvider _serviceProvider = default!;
    private static bool _isInitializationFinished = false;

    public static async Task Main(string[] args)
    {
        try
        {
            var builder = CreateWebAssemblyBuilder(args);

            _ = builder.Logging.SetMinimumLevel(LogLevel.Warning);

            Register(builder.Services);

            await using var host = CreateWebAssemblyHost(builder);

            _serviceProvider = host.Services;

            await _serviceProvider.BuildPresentation();

            _isInitializationFinished = true;

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    [JSExport]
    public static async Task StartShowOptions(JSObject bondCommissionsJSObject, JSObject westernUnionParametersJSObject, JSObject dolarCryptoParametersJSObject)
    {
        try
        {
            await AwaitToFinishInitialization();
            await AwaitToDependencies();

            var _parametersConverter = _serviceProvider.GetRequiredService<IParametersJSObjectConverter>();
            var parameters = _parametersConverter.GetParameters(bondCommissionsJSObject, westernUnionParametersJSObject, dolarCryptoParametersJSObject);
            var starter = _serviceProvider.GetRequiredService<IRuculaStarterPresenter>();

            await starter.Start(parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private static Task AwaitToDependencies()
        => Awaiter.AwaitToDependencyNotNull(() => _serviceProvider);

    private static Task AwaitToFinishInitialization()
        => Awaiter.KeepAwaiting(() => _isInitializationFinished);

    private static WebAssemblyHostBuilder CreateWebAssemblyBuilder(string[] args)
        => WebAssemblyHostBuilder.CreateDefault(args);

    private static WebAssemblyHost CreateWebAssemblyHost(WebAssemblyHostBuilder builder)
        => builder.Build();

    private static void Register(IServiceCollection serviceCollection)
        => serviceCollection
            .AddHttpClient()
            .AddDataAccess()
            .AddDomain()
            .AddApplication()
            .AddPresentation()
            .AddRuculaWebAssembly();
}