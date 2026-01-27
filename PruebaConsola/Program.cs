using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Rucula.Application;
using Rucula.DataAccess.IoC;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;
using Rucula.Domain.Implementations.IoC;

namespace PruebaConsola;

internal class Program
{
    private const string emptyString = "--";
    private static INotifier _notifier = default!;

    static Task Main()
    {
        var servicesCollection = new ServiceCollection();
        _ = servicesCollection
            .AddHttpClient()
            .AddDataAccess()
            .AddDomain()
            .AddApplication()
            .AddSingleton<INotifier, ConsoleNotifier>()
            .AddSingleton<NavigationManager, PruebaNavigationManager>();

        var services = servicesCollection.BuildServiceProvider();

        _notifier = services.GetRequiredService<INotifier>();

        return GetOptionsFromService(services, new(1, 1, 1), new(20200), new(10000), new(15000));
    }

    private static Task GetOptionsFromService(IServiceProvider serviceProvider, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters, DolarAppParameters dolarAppParameters)
    {
        var service = serviceProvider.GetRequiredService<IBestOptionService>();
        var parameters = new OptionParameters(bondCommissions, dolarCryptoParameters, westernUnionParameters, dolarAppParameters);
        var callbacks = new OptionCallbacks(OnWinningOption, OnBonds, OnBlue, OnWesternUnion, OnCryptos, OnDolarApp);

        return service.ProcessOptions(parameters, callbacks);
    }

    private static void OnWinningOption(WinningOption winningOption)
    {
        _ = _notifier.Notify($"Mejor opción: {winningOption}{Environment.NewLine}");
        _ = _notifier.Notify($"{Environment.NewLine}");
    }

    private static void OnBonds(IEnumerable<TituloIsin> bonds)
    {
        _ = _notifier.Notify($"Titulos {Environment.NewLine}");

        foreach (var bond in bonds)
        {
            _ = _notifier.Notify($"{bond.TituloCable?.Simbolo}/{bond.TituloPeso?.Simbolo}: {bond.GrossCcl} -> Neto: {bond.NetCcl} {Environment.NewLine}");
        }

        _ = _notifier.Notify($"{Environment.NewLine}");
    }

    private static void OnCryptos(IEnumerable<DolarCryptoPrices> cryptos)
    {
        _ = _notifier.Notify($"Crypto {Environment.NewLine}");

        _ = _notifier.Notify($"{Columnize("Exchange")} {Columnize("Blockchain")} {Columnize("USDC")} {Columnize("USDT")} {Columnize("DAI")}{Environment.NewLine}");

        foreach (var crypto in cryptos)
        {
            _ = _notifier.Notify($"{Columnize(crypto.ExchangeName)} {Columnize(string.Empty)} {Columnize(crypto.GrossUsdc)} {Columnize(crypto.GrossUsdt)} {Columnize(crypto.GrossDai)}{Environment.NewLine}");

            foreach (var netPrices in crypto.DolarCryptoNetPrices)
            {
                _ = _notifier.Notify($"{Columnize(string.Empty)} {Columnize(netPrices.Blockchain.Name)} {Columnize(netPrices.NetUsdc)} {Columnize(netPrices.NetUsdt)} {Columnize(netPrices.NetDai)}{Environment.NewLine}");
            }
        }

        _ = _notifier.Notify($"{Environment.NewLine}");
    }

    private static void OnBlue(Optional<Blue> blue)
        => _ = _notifier.Notify($"Blue: {GetStringFromOptional(blue, emptyString)}{Environment.NewLine}");

    private static void OnWesternUnion(Optional<DolarWesternUnion> wu)
        => _ = _notifier.Notify($"WU: {GetStringFromOptional(wu, emptyString)}{Environment.NewLine}");

    private static void OnDolarApp(Optional<DolarApp> dolarApp)
        => _ = _notifier.Notify($"DolarApp: {GetStringFromOptional(dolarApp, emptyString)}{Environment.NewLine}");


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
