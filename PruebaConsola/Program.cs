using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace PruebaConsola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        static IFetching<TituloIsin> CreateTituloIsinFetcher()
        {
            var fetcher = new TituloIsinFetching();
        }
    }
}