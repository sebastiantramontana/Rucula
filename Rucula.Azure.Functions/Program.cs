using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
        => new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(services => services.AddHttpClient())
            .Build()
            .Run();
}