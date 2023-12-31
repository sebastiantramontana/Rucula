﻿using Microsoft.Extensions.DependencyInjection;
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

            var blueProvider = services.GetRequiredService<IDolarBlueProvider>();
            Console.WriteLine($"Blue: {await blueProvider.GetCurrentBlue()}");

            var cryptoProvider = services.GetRequiredService<IDolarCryptoProvider>();
            Console.WriteLine($"Crypto: {await cryptoProvider.GetCurrentDolarCrypto()}");

            var wuProvider = services.GetRequiredService<IWesternUnionProvider>();
            Console.WriteLine($"WU: {await wuProvider.GetCurrentDolarWesternUnion()}");

            var service = services.GetRequiredService<IChoicesService>();
            var choices = await service.GetChoices();

            Console.WriteLine($"Mejor opción: {choices.WinningChoice}");

            foreach (var titulo in choices.RankingTitulos)
            {
                Console.WriteLine($"{titulo.TituloCable?.Simbolo}/{titulo.TituloPeso?.Simbolo}: {titulo.CotizacionCcl}");
            }
        }
    }
}