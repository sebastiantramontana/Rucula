using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Infrastructure;
using Rucula.Infrastructure.IoC;
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
    private static NavigationManager _navigationManager = default!;
    private static IHttpClientFactory _httpClientFactory = default!;
    private static INotifier _notifier = default!;
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);
    private static ChoicesInfo _currentChoices = ChoicesInfo.NoChoices;

    public static async Task Main(string[] args)
    {
        try
        {
            var builder = CreateWebAssemblyBuilder(args);

            _ = builder.Logging.SetMinimumLevel(LogLevel.Trace);

            Register(builder.Services);
            InstanceServices(builder.Services.BuildServiceProvider());

            Console.WriteLine("CORRIENDO WASM MOCKEADO...!!!");

            await using var host = CreateWebAssemblyHost(builder);
            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    [JSExport]
    [return: JSMarshalAs<Promise<JSType.String>>]
    public static async Task<string> GetChoices(JSObject bondCommissions, JSObject westernUnionParameters, JSObject dolarCryptoParameters)
    {
        ShowParameters(bondCommissions, westernUnionParameters, dolarCryptoParameters);

        await NullDependencyAwaiter.AwaitToNotNull(() => _navigationManager);
        await NullDependencyAwaiter.AwaitToNotNull(() => _httpClientFactory);
        await NullDependencyAwaiter.AwaitToNotNull(() => _notifier);

        var mockParam = await GetMockParam();

        _currentChoices = mockParam switch
        {
            null or "" => await FetchDefaultMock(),
            "forever" => await RunForever(),
            _ => await FetchMock(mockParam!)
        };

        return JsonSerializer.Serialize(_currentChoices, SourceGenerationContext.Default.ChoicesInfo);
    }

    [JSExport]
    [return: JSMarshalAs<Promise<JSType.String>>]
    public static Task<string> RecalculateChoices(JSObject bondCommissions, JSObject westernUnionParameters, JSObject dolarCryptoParameters)
    {
        ShowParameters(bondCommissions, westernUnionParameters, dolarCryptoParameters);

        var json = JsonSerializer.Serialize(_currentChoices, SourceGenerationContext.Default.ChoicesInfo);
        return Task.FromResult(json);
    }

    private static void ShowParameters(JSObject bondCommissions, JSObject westernUnionParameters, JSObject dolarCryptoParameters)
    {
        Console.WriteLine($"Comisiones: {bondCommissions.GetPropertyAsDouble("purchasePercentage")}% - {bondCommissions.GetPropertyAsDouble("salePercentage")}% - {bondCommissions.GetPropertyAsDouble("withdrawalPercentage")}%");
        Console.WriteLine($"Parámetros WU: {westernUnionParameters.GetPropertyAsDouble("amountToSend")}");
        Console.WriteLine($"Parámetros Crypto: {dolarCryptoParameters.GetPropertyAsDouble("volume")}");
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

    private static Task<ChoicesInfo> FetchDefaultMock()
        => FetchMock("default.json");

    private static async Task<ChoicesInfo> FetchMock(string mockName)
    {
        await _notifier.Notify($"Fetching mock: {mockName}");
        await Task.Delay(1000);

        var uri = GetMockUri(mockName);
        var json = await RequestMock(uri);

        return JsonSerializer.Deserialize<ChoicesInfo>(json, SourceGenerationContext.RuculaOptions)!;
    }

    private static async Task<string> RequestMock(string uri)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        using var client = _httpClientFactory.CreateClient();
        using var msg = await client.SendAsync(request).ConfigureAwait(false);
        return await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
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

    private static async Task<ChoicesInfo> RunForever()
    {
        await _notifier.Notify("Running forever...");

        await Task.Delay(Timeout.InfiniteTimeSpan);
        return ChoicesInfo.NoChoices;
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
    }

    private static void Register(IServiceCollection serviceCollection)
        => serviceCollection
            .AddHttpClient()
            .AddInfrastructure();
}