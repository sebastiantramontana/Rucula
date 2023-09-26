using Microsoft.Extensions.DependencyInjection;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Implementations.IoC;

namespace PruebaConsola
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var servicesCollection = new ServiceCollection();
            DataAccessRegistrar.Register(servicesCollection);
            DomainRegistrar.Register(servicesCollection);

            servicesCollection.AddHttpClient();
            var services = servicesCollection.BuildServiceProvider();

            var blueService = services.GetRequiredService<IDolarBlueProvider>();

            Console.WriteLine($"Blue: {await blueService.GetCurrentBlue()}");
            var service = services.GetRequiredService<ITitulosService>();

            var titulos = await service.GetCclRankingTitulosIsin();

            foreach (var titulo in titulos.OrderByDescending(t => t.CotizacionCcl))
            {
                Console.WriteLine($"{titulo.TituloCable?.Simbolo}/{titulo.TituloPeso?.Simbolo}: {titulo.CotizacionCcl}");
            }
        }
    }
}