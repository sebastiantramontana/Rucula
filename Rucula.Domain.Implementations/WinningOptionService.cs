using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal sealed class WinningOptionService : IWinningOptionService
{
    public void CalculateWinner(IEnumerable<TituloIsin> rankingTitulos, Optional<DolarWesternUnion> dolarWesternUnion, IEnumerable<DolarCryptoPrices> rankingCryptos, Optional<DolarApp> dolarApp, Action<WinningOption> OnWinningOption)
    {
        var bestTitulo = MaybeFirst(rankingTitulos);
        var bestCrypto = MaybeFirst(rankingCryptos);
        var winner = GetWinner(bestTitulo, dolarWesternUnion, bestCrypto, dolarApp);

        OnWinningOption.Invoke(winner);
    }

    private static WinningOption GetWinner(Optional<TituloIsin> titulo, Optional<DolarWesternUnion> dolarWesternUnion, Optional<DolarCryptoPrices> bestCrypto, Optional<DolarApp> dolarApp)
    {
        var competitors = new List<WinningOption>(4);

        AddOptionalCompetitor(competitors, titulo, CreateWinner);
        AddOptionalCompetitor(competitors, dolarWesternUnion, CreateWinner);
        AddOptionalCompetitor(competitors, bestCrypto, CreateWinner);
        AddOptionalCompetitor(competitors, dolarApp, CreateWinner);

        var winner = competitors
                    .OrderByDescending(w => w.DolarPrice)
                    .FirstOrDefault();

        return winner ?? WinningOption.NoWinners;
    }

    private static void AddOptionalCompetitor<T>(ICollection<WinningOption> competitors, Optional<T> competitor, Func<T, WinningOption> createWinnerFunc)
    {
        if (competitor.HasValue)
        {
            competitors.Add(createWinnerFunc(competitor.Value));
        }
    }

    private static DolarCryptoNetPrice GetBestCryptoNetPrice(DolarCryptoPrices topCryptoNetPrices)
        => topCryptoNetPrices.DolarCryptoNetPrices.First().TopNetPrice;

    private static WinningOption CreateWinner(TituloIsin titulo)
        => new(titulo.TituloPeso.Simbolo, "Incluye comisiones, pero no costos de transferencias", titulo.NetCcl);

    private static WinningOption CreateWinner(DolarWesternUnion dolarWesternUnion)
        => new("Western Union", "Incluye costos fijos", dolarWesternUnion.NetPrice);

    private static WinningOption CreateWinner(DolarCryptoPrices dolarCryptoPrices)
    {
        var bestNetPrice = GetBestCryptoNetPrice(dolarCryptoPrices);
        return new("Contado con Crypto", "Incluye comisiones fijas de exchange y de retiro", bestNetPrice.NetPrice);
    }

    private static WinningOption CreateWinner(DolarApp dolarApp)
        => new("DolarApp", "Incluye costos fijos", dolarApp.NetPrice);

    private static Optional<T> MaybeFirst<T>(IEnumerable<T> values)
        => Optional<T>.Maybe(values.FirstOrDefault());
}
