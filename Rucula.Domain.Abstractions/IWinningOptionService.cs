using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IWinningOptionService
{
    Task CalculateWinner(IEnumerable<TituloIsin> rankingTitulos, Optional<DolarWesternUnion> dolarWesternUnion, IEnumerable<DolarCryptoPrices> rankingCryptos, Func<WinningOption, Task> OnWinningOption);
}
