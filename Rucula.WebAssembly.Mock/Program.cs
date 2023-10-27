using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.JSInterop;
using Rucula.Domain.Entities;
using System.Text.Json;

namespace Rucula.WebAssembly.Mock
{
    public class Program
    {
        private static NavigationManager _navigationManager = default!;
        private static IHttpClientFactory _httpClientFactory = default!;

        public static async Task Main(string[] args)
        {
            try
            {
                var builder = CreateWebAssemblyBuilder(args);

                builder.Logging.SetMinimumLevel(LogLevel.Trace);

                await using var host = CreateWebAssemblyHost(builder);

                Register(builder.Services);
                InstanceServices(builder.Services.BuildServiceProvider());

                Console.WriteLine("CORRIENDO WASM MOCKEADO...!!!");

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
            var mockParam = GetMockParam();

            return mockParam switch
            {
                null or "" => await FetchDefaultMock(),
                "forever" => await RunForever(),
                _ => await FetchMock(mockParam!)
            };
        }

        private static string? GetMockParam()
        {
            var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

            if (string.IsNullOrEmpty(uri.Query))
                return null;

            var queryStrings = QueryHelpers.ParseNullableQuery(uri.Query);

            if (!(queryStrings?.TryGetValue("mock", out StringValues values) ?? false))
                return null;

            Console.WriteLine($"params: {values}");

            return values[0];
        }

        private static Task<ChoicesInfo> FetchDefaultMock()
            => FetchMock("default.json");

        private static async Task<ChoicesInfo> FetchMock(string mockName)
        {
            Console.WriteLine($"Fetching mock: {mockName}");

            var uri = GetMockUri(mockName);
            var json = await RequestMock(uri);
            return JsonSerializer.Deserialize<ChoicesInfo>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
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
            Console.WriteLine("Running forever...");

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
        }

        private static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
        }
    }
}