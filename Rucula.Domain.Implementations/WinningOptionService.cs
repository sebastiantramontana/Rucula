using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal sealed class WinningOptionService : IWinningOptionService
{
    public void CalculateWinner(IEnumerable<TituloIsin> rankingTitulos, Optional<DolarWesternUnion> dolarWesternUnion, IEnumerable<DolarCryptoPrices> rankingCryptos, Action<WinningOption> OnWinningOption)
    {
        var bestTitulo = MaybeFirst(rankingTitulos);
        var bestCrypto = MaybeFirst(rankingCryptos);
        var winner = GetWinner(bestTitulo, dolarWesternUnion, bestCrypto);

        OnWinningOption.Invoke(winner);
    }

    private static WinningOption GetWinner(Optional<TituloIsin> titulo, Optional<DolarWesternUnion> dolarWesternUnion, Optional<DolarCryptoPrices> bestCrypto)
    {
        var competitors = new List<WinningOption>(3);

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

        return winner ?? WinningOption.NoWinners;
    }

    private static DolarCryptoNetPrice GetBestCryptoNetPrice(DolarCryptoPrices topCryptoNetPrices)
        => topCryptoNetPrices.DolarCryptoNetPrices.First().TopNetPrice;

    private static WinningOption CreateWinner(TituloIsin titulo)
        => new(titulo.TituloPeso.Simbolo, "Incluye comisiones, pero no costos de transferencias", titulo.NetCcl);

    private static WinningOption CreateWinner(DolarWesternUnion dolarWesternUnion)
        => new("Western Union", "Incluye costos", dolarWesternUnion.NetPrice);

    private static WinningOption CreateWinner(DolarCryptoNetPrice dolarCryptoNetPrice)
        => new("Contado con Crypto", "Incluye comisiones de exchange y de retiro", dolarCryptoNetPrice.NetPrice);

    private static Optional<T> MaybeFirst<T>(IEnumerable<T> values)
        => Optional<T>.Maybe(values.FirstOrDefault());
}
