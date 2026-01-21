using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IWinningOptionService
{
    void CalculateWinner(IEnumerable<TituloIsin> rankingTitulos, Optional<DolarWesternUnion> dolarWesternUnion, IEnumerable<DolarCryptoPrices> rankingCryptos, Action<WinningOption> OnWinningOption);
}
