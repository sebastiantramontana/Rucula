using Microsoft.Extensions.DependencyInjection;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace PruebaConsola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var servicesCollection = new ServiceCollection();
            Rucula.DataAccess.IoC.Registrar.Register(servicesCollection);
            var services = servicesCollection.BuildServiceProvider();

            var provider = services.GetRequiredService<IProvider<TituloIsin>>();

            var titulos = provider.Get().Result;

            foreach(var titulo in titulos)
            {
                Console.WriteLine(titulo.CodigoIsin);
            }
        }
    }
}