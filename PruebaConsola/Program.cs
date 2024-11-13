﻿using Microsoft.Extensions.DependencyInjection;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Implementations.IoC;

namespace PruebaConsola;

internal class Program
{
    static async Task Main()
    {
        var servicesCollection = new ServiceCollection();
        _ = servicesCollection
            .AddHttpClient()
            .AddDataAccess()
            .AddDomain()
            .AddSingleton<INotifier, ConsoleNotifier>();

        var services = servicesCollection.BuildServiceProvider();

        var notifier = services.GetRequiredService<INotifier>();
        var service = services.GetRequiredService<IChoicesService>();

        var choices = await service.GetChoices(new BondCommissions(1, 1, 1), new(20200), new(10000));

        await notifier.NotifyProgress($"Mejor opción: {choices.WinningChoice}{Environment.NewLine}");

        await notifier.NotifyProgress($"{Environment.NewLine}");
        await notifier.NotifyProgress($"Titulos {Environment.NewLine}");

        foreach (var titulo in choices.RankingTitulos)
        {
            await notifier.NotifyProgress($"{titulo.TituloCable?.Simbolo}/{titulo.TituloPeso?.Simbolo}: {titulo.GrossCcl} -> Neto: {titulo.NetCcl} {Environment.NewLine}");
        }

        await notifier.NotifyProgress($"{Environment.NewLine}");
        await notifier.NotifyProgress($"Crypto {Environment.NewLine}");

        await notifier.NotifyProgress($"{Columnize("Exchange")} {Columnize("Blockchain")} {Columnize("USDC")} {Columnize("USDT")} {Columnize("DAI")}{Environment.NewLine}");

        foreach (var crypto in choices.RankingCryptos)
        {
            await notifier.NotifyProgress($"{Columnize(crypto.ExchangeName)} {Columnize(string.Empty)} {Columnize(crypto.GrossUsdc)} {Columnize(crypto.GrossUsdt)} {Columnize(crypto.GrossDai)}{Environment.NewLine}");

            foreach (var netPrices in crypto.DolarCryptoNetPrices)
            {
                await notifier.NotifyProgress($"{Columnize(string.Empty)} {Columnize(netPrices.Blockchain.Name)} {Columnize(netPrices.NetUsdc)} {Columnize(netPrices.NetUsdt)} {Columnize(netPrices.NetDai)}{Environment.NewLine}");
            }
        }

        await notifier.NotifyProgress($"{Environment.NewLine}");

        const string emptyString = "--";

        await notifier.NotifyProgress($"Blue: {GetStringFromOptional(choices.Blue, emptyString)}{Environment.NewLine}");
        await notifier.NotifyProgress($"WU: {GetStringFromOptional(choices.DolarWesternUnion, emptyString)}{Environment.NewLine}");
        await notifier.NotifyProgress($"Diarco: {GetStringFromOptional(choices.DolarDiarco, emptyString)}{Environment.NewLine}");
    }

    private static string Columnize(Optional<DolarCryptoNetPrice> optionalValue)
        => Columnize(GetStringFromOptional(optionalValue, value => FormatPrice(value.NetPrice), string.Empty));

    private static string Columnize(Optional<double> optionalValue)
        => Columnize(GetStringFromOptional(optionalValue, FormatPrice, string.Empty));

    private static string Columnize(string value)
        => value.PadRight(12).Remove(12);

    private static string GetStringFromOptional<T>(Optional<T> optionalValue, string emptyValue)
            => GetStringFromOptional(optionalValue, value => value!.ToString()!, emptyValue);

    private static string GetStringFromOptional<T>(Optional<T> optionalValue, Func<T, string> getValueFunc, string emptyValue)
            => optionalValue.HasValue
                ? getValueFunc.Invoke(optionalValue.Value ?? throw new Exception("HasValue, pero value nulo!"))
                : emptyValue;

    private static string FormatPrice(double value)
        => Math.Round(value, 2).ToString();
}
