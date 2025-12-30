using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

public interface IWinningChoiceService
{
    Task CalculateWinningChoice(IEnumerable<TituloIsin> rankingTitulos, Optional<DolarWesternUnion> dolarWesternUnion, IEnumerable<DolarCryptoPrices> rankingCryptos, Func<WinningChoice, Task> OnWinningChoice);
}
