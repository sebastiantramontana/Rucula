using Microsoft.Extensions.DependencyInjection;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Implementations.IoC;

namespace PruebaConsola;

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
        var service = services.GetRequiredService<IChoicesService>();

        var choices = await service.GetChoices(new BondCommissions(1, 1, 1), new(100));

        await notifier.NotifyProgress($"Mejor opción: {choices.WinningChoice}{Environment.NewLine}");

        foreach (var titulo in choices.RankingTitulos)
        {
            await notifier.NotifyProgress($"{titulo.TituloCable?.Simbolo}/{titulo.TituloPeso?.Simbolo}: {titulo.GrossCcl} -> Neto: {titulo.NetCcl} {Environment.NewLine}");
        }

        await notifier.NotifyProgress($"Blue: {choices.Blue.Value.PrecioCompra}{Environment.NewLine}");
        await notifier.NotifyProgress($"Crypto: {choices.DolarCrypto.Value}{Environment.NewLine}");
        await notifier.NotifyProgress($"WU: {choices.DolarWesternUnion.Value}{Environment.NewLine}");
        await notifier.NotifyProgress($"Diarco: {choices.DolarDiarco.Value}{Environment.NewLine}");
    }
}
