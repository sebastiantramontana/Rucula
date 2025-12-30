using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal sealed class WinningChoiceService : IWinningChoiceService
{
    public Task CalculateWinningChoice(IEnumerable<TituloIsin> rankingTitulos, Optional<DolarWesternUnion> dolarWesternUnion, IEnumerable<DolarCryptoPrices> rankingCryptos, Func<WinningChoice, Task> OnWinningChoice)
    {
        var bestTitulo = MaybeFirst(rankingTitulos);
        var bestCrypto = MaybeFirst(rankingCryptos);
        var winner= GetWinningChoice(bestTitulo, dolarWesternUnion, bestCrypto);

        return OnWinningChoice.Invoke(winner);
    }

    private static WinningChoice GetWinningChoice(Optional<TituloIsin> titulo, Optional<DolarWesternUnion> dolarWesternUnion, Optional<DolarCryptoPrices> bestCrypto)
    {
        var competitors = new List<WinningChoice>(3);

        if (titulo.HasValue)
        {
            competitors.Add(CreateWinner(titulo.Value));
        }

        if (dolarWesternUnion.HasValue)
        {
            competitors.Add(CreateWinner(dolarWesternUnion.Value));
        }

        if (bestCrypto.HasValue)
        {
            var bestNetPrice = GetBestCryptoNetPrice(bestCrypto.Value);
            competitors.Add(CreateWinner(bestNetPrice));
        }

        var winner = competitors
                    .OrderByDescending(w => w.DolarPrice)
                    .FirstOrDefault();

        return winner ?? WinningChoice.NoWinners;
    }

    private static DolarCryptoNetPrice GetBestCryptoNetPrice(DolarCryptoPrices topCryptoNetPrices)
        => topCryptoNetPrices.DolarCryptoNetPrices.First().TopNetPrice;

    private static WinningChoice CreateWinner(TituloIsin titulo)
        => new(titulo.TituloPeso.Simbolo, "Incluye comisiones, pero no costos de transferencias", titulo.NetCcl);

    private static WinningChoice CreateWinner(DolarWesternUnion dolarWesternUnion)
        => new("Western Union", "Incluye costos", dolarWesternUnion.NetPrice);

    private static WinningChoice CreateWinner(DolarCryptoNetPrice dolarCryptoNetPrice)
        => new("Contado con Crypto", "Incluye comisiones de exchange y de retiro", dolarCryptoNetPrice.NetPrice);

    private static Optional<T> MaybeFirst<T>(IEnumerable<T> values)
        => Optional<T>.Maybe(values.FirstOrDefault());
}
