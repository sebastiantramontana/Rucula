using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace PruebaConsola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        static IProvider<TituloIsin> CreateTituloIsinFetcher()
        {
            var fetcher = new TituloIsinFetching();
        }
    }
}