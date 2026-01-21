using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using Rucula.Application;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities.Parameters;
using Rucula.Infrastructure;
using Rucula.Presentation.IoC;
using Rucula.Presentation.Presenters;
using Rucula.WebAssembly.Mock;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;

namespace Rucula.WebAssembly;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
[SupportedOSPlatform("browser")]
public partial class Program
{
    private static NavigationManager _navigationManager = default!;
    private static IHttpClientFactory _httpClientFactory = default!;
    private static INotifier _notifier = default!;
    private static IRuculaStarterPresenter _starter = default!;
    private static BestOptionServiceMock _bestOptionService = default!;
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);
    private static bool _isInitializationFinished = false;

    public static async Task Main(string[] args)
    {
        try
        {
            var builder = CreateWebAssemblyBuilder(args);

            _ = builder.Logging.SetMinimumLevel(LogLevel.Trace);

            Register(builder.Services);

            Console.WriteLine("CORRIENDO WASM MOCKEADO...!!!");

            await using var host = CreateWebAssemblyHost(builder);

            InstanceServices(host.Services);
            await host.Services.BuildPresentation();

            _isInitializationFinished = true;

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    [JSExport]
    public static async Task StartShowOptions(JSObject bondCommissions, JSObject westernUnionParameters, JSObject dolarCryptoParameters)
    {
        await Awaiter.KeepAwaiting(() => _isInitializationFinished);

        var initialParameters = ConvertParameters(bondCommissions, westernUnionParameters, dolarCryptoParameters);
        ShowParameters(initialParameters);

        await Awaiter.AwaitToDependencyNotNull(() => _navigationManager);
        await Awaiter.AwaitToDependencyNotNull(() => _httpClientFactory);
        await Awaiter.AwaitToDependencyNotNull(() => _notifier);
        await Awaiter.AwaitToDependencyNotNull(() => _starter);
        await Awaiter.AwaitToDependencyNotNull(() => _bestOptionService);

        var mockParam = await GetMockParam();

        var currentOptions = mockParam switch
        {
            null or "" => await FetchDefaultMock(),
            "forever" => await RunForever(),
            _ => await FetchMock(mockParam!)
        };

        _bestOptionService.UpdateMockedOptions(currentOptions);

        await _starter.Start(initialParameters);
    }

    private static OptionParameters ConvertParameters(JSObject bondCommissions, JSObject westernUnionParameters, JSObject dolarCryptoParameters)
        => new(
            new(bondCommissions.GetPropertyAsDouble("purchasePercentage"),
                bondCommissions.GetPropertyAsDouble("salePercentage"),
                bondCommissions.GetPropertyAsDouble("withdrawalPercentage")),
            new(dolarCryptoParameters.GetPropertyAsDouble("volume")),
            new(westernUnionParameters.GetPropertyAsDouble("amountToSend"))
            );

    private static void ShowParameters(OptionParameters parameters)
    {
        Console.WriteLine($"Comisiones: {parameters.BondCommissions.PurchasePercentage}% - {parameters.BondCommissions.SalePercentage}% - {parameters.BondCommissions.WithdrawalPercentage}%");
        Console.WriteLine($"Parámetros WU: {parameters.WesternUnionParameters.AmountToSend}");
        Console.WriteLine($"Parámetros Crypto: {parameters.CryptoParameters.TradingVolume}");
    }

    private static async Task<string?> GetMockParam()
    {
        var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

        if (string.IsNullOrEmpty(uri.Query))
        {
            return null;
        }

        var queryStrings = QueryHelpers.ParseNullableQuery(uri.Query);

        if (!(queryStrings?.TryGetValue("mock", out var values) ?? false))
        {
            return null;
        }

        await _notifier.Notify($"params: {values}");
        await Task.Delay(1000);

        return values[0];
    }

    private static Task<OptionsInfo> FetchDefaultMock()
        => FetchMock("default.json");

    private static async Task<OptionsInfo> FetchMock(string mockName)
    {
        await _notifier.Notify($"Fetching mock: {mockName}");
        await Task.Delay(1000);

        var uri = GetMockUri(mockName);
        var json = await RequestMock(uri);

        return JsonSerializer.Deserialize<OptionsInfo>(json, SourceGenerationContext.RuculaOptions)!;
    }

    private static async Task<string> RequestMock(string uri)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        using var client = _httpClientFactory.CreateClient();
        using var msg = await client.SendAsync(request);
        return await msg.Content.ReadAsStringAsync();
    }

    private static string GetMockUri(string mock)
    {
        var uriPath = GeUriPath();
        var uri = $"{uriPath}mocks/{mock}";
        return uri;
    }

    private static string GeUriPath()
        => _navigationManager
            .ToAbsoluteUri(_navigationManager.Uri)
            .GetLeftPart(UriPartial.Path);

    private static async Task<OptionsInfo> RunForever()
    {
        await _notifier.Notify("Running forever...");

        await Task.Delay(Timeout.InfiniteTimeSpan);
        return OptionsInfo.NoOptions;
    }

    private static WebAssemblyHostBuilder CreateWebAssemblyBuilder(string[] args)
        => WebAssemblyHostBuilder.CreateDefault(args);

    private static WebAssemblyHost CreateWebAssemblyHost(WebAssemblyHostBuilder builder)
        => builder.Build();

    private static void InstanceServices(IServiceProvider serviceProvider)
    {
        _navigationManager = serviceProvider.GetRequiredService<NavigationManager>();
        _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        _notifier = serviceProvider.GetRequiredService<INotifier>();
        _starter = serviceProvider.GetRequiredService<IRuculaStarterPresenter>();
        _bestOptionService = serviceProvider.GetRequiredService<BestOptionServiceMock>();
    }

    private static void Register(IServiceCollection serviceCollection)
        => serviceCollection
            .AddHttpClient()
            .AddSingleton<BestOptionServiceMock>()
            .AddSingleton<IBestOptionService>(sp => sp.GetRequiredService<BestOptionServiceMock>())
            .AddSingleton<IRestartingPeriodicRunnerService, RestartingPeriodicRunnerServiceMock>()
            .AddPresentation();
}
