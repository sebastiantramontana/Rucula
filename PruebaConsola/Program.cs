using Microsoft.Extensions.DependencyInjection;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace PruebaConsola
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var servicesCollection = new ServiceCollection();
            Rucula.DataAccess.IoC.Registrar.Register(servicesCollection);
            var services = servicesCollection.BuildServiceProvider();

            var provider = services.GetRequiredService<IProvider<TituloIsin>>();

            var titulos = await provider.Get();

            foreach(var titulo in titulos.OrderByDescending(t=>t.CotizacionCcl))
            {
                Console.WriteLine(titulo.CodigoIsin);
            }
        }
    }
}