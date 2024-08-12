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
            servicesCollection
                .AddHttpClient()
                .AddDataAccess()
                .AddDomain()
                .AddSingleton<INotifier, ConsoleNotifier>();

            var services = servicesCollection.BuildServiceProvider();

            var notifier = services.GetRequiredService<INotifier>();

            var blueProvider = services.GetRequiredService<IDolarBlueProvider>();
            await notifier.NotifyProgress($"Blue: {await blueProvider.GetCurrentBlue()}{Environment.NewLine}");

            var cryptoProvider = services.GetRequiredService<IDolarCryptoProvider>();
            await notifier.NotifyProgress($"Crypto: {await cryptoProvider.GetCurrentDolarCrypto()}{Environment.NewLine}");

            var wuProvider = services.GetRequiredService<IWesternUnionProvider>();
            await notifier.NotifyProgress($"WU: {await wuProvider.GetCurrentDolarWesternUnion()}{Environment.NewLine}");

            var service = services.GetRequiredService<IChoicesService>();
            var choices = await service.GetChoices();

            await notifier.NotifyProgress($"Mejor opción: {choices.WinningChoice}{Environment.NewLine}");

            foreach (var titulo in choices.RankingTitulos)
            {
                await notifier.NotifyProgress($"{titulo.TituloCable?.Simbolo}/{titulo.TituloPeso?.Simbolo}: {titulo.CotizacionCcl}{Environment.NewLine}");
            }
        }
    }

    public class ConsoleNotifier : INotifier
    {
        public Task NotifyProgress(string message)
        {
            CleanConsoleLine();
            Console.Write(message);

            return Task.CompletedTask;
        }

        private static void CleanConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("\u001b[2K");
        }
    }
}