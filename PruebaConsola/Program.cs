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

        var choices = await service.GetChoices(new BondCommissions(1, 1, 1), new(20200));

        await notifier.NotifyProgress($"Mejor opción: {choices.WinningChoice}{Environment.NewLine}");

        foreach (var titulo in choices.RankingTitulos)
        {
            await notifier.NotifyProgress($"{titulo.TituloCable?.Simbolo}/{titulo.TituloPeso?.Simbolo}: {titulo.GrossCcl} -> Neto: {titulo.NetCcl} {Environment.NewLine}");
        }

        await notifier.NotifyProgress($"Blue: {GetValueFromOptional(choices.Blue)}{Environment.NewLine}");
        await notifier.NotifyProgress($"Crypto: {GetValueFromOptional(choices.DolarCrypto)}{Environment.NewLine}");
        await notifier.NotifyProgress($"WU: {GetValueFromOptional(choices.DolarWesternUnion)}{Environment.NewLine}");
        await notifier.NotifyProgress($"Diarco: {GetValueFromOptional(choices.DolarDiarco)}{Environment.NewLine}");
    }

    private static string GetValueFromOptional<T>(Optional<T> optionalValue)
        => optionalValue.HasValue
            ? optionalValue.Value?.ToString() ?? "HasValue, pero value nulo!"
            : "----";
}
