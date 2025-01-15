using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Implementations.IoC;
using WasmMock = Rucula.WebAssembly.Mock.Program;

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
            .AddSingleton<INotifier, ConsoleNotifier>()
            .AddSingleton<NavigationManager, PruebaNavigationManager>();

        var services = servicesCollection.BuildServiceProvider();

        var notifier = services.GetRequiredService<INotifier>();

        bool getFromService = true;

        var choices = getFromService
            ? await GetChoicesFromService(services, new(1, 1, 1), new(20200), new(10000))
            : await GetChoicesFromWasmMock(servicesCollection, new(1, 1, 1), new(20200), new(10000));

        await notifier.Notify($"Mejor opción: {choices.WinningChoice}{Environment.NewLine}");

        await notifier.Notify($"{Environment.NewLine}");
        await notifier.Notify($"Titulos {Environment.NewLine}");

        foreach (var titulo in choices.RankingTitulos)
        {
            await notifier.Notify($"{titulo.TituloCable?.Simbolo}/{titulo.TituloPeso?.Simbolo}: {titulo.GrossCcl} -> Neto: {titulo.NetCcl} {Environment.NewLine}");
        }

        await notifier.Notify($"{Environment.NewLine}");
        await notifier.Notify($"Crypto {Environment.NewLine}");

        await notifier.Notify($"{Columnize("Exchange")} {Columnize("Blockchain")} {Columnize("USDC")} {Columnize("USDT")} {Columnize("DAI")}{Environment.NewLine}");

        foreach (var crypto in choices.RankingCryptos)
        {
            await notifier.Notify($"{Columnize(crypto.ExchangeName)} {Columnize(string.Empty)} {Columnize(crypto.GrossUsdc)} {Columnize(crypto.GrossUsdt)} {Columnize(crypto.GrossDai)}{Environment.NewLine}");

            foreach (var netPrices in crypto.DolarCryptoNetPrices)
            {
                await notifier.Notify($"{Columnize(string.Empty)} {Columnize(netPrices.Blockchain.Name)} {Columnize(netPrices.NetUsdc)} {Columnize(netPrices.NetUsdt)} {Columnize(netPrices.NetDai)}{Environment.NewLine}");
            }
        }

        await notifier.Notify($"{Environment.NewLine}");

        const string emptyString = "--";

        await notifier.Notify($"Blue: {GetStringFromOptional(choices.Blue, emptyString)}{Environment.NewLine}");
        await notifier.Notify($"WU: {GetStringFromOptional(choices.DolarWesternUnion, emptyString)}{Environment.NewLine}");
    }

    private static Task<ChoicesInfo> GetChoicesFromWasmMock(IServiceCollection servicesCollection, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters)
    {
        WasmMock.InstanceServices(servicesCollection);
        return WasmMock.GetChoices(bondCommissions, westernUnionParameters, dolarCryptoParameters);
    }

    private static Task<ChoicesInfo> GetChoicesFromService(IServiceProvider serviceProvider, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters)
    {
        var service = serviceProvider.GetRequiredService<IChoicesService>();
        return service.GetChoices(bondCommissions, westernUnionParameters, dolarCryptoParameters);
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
