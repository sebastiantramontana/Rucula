using Rucula.Domain.Entities;

namespace Rucula.WebAssembly.Mock;

public sealed record class OptionsInfo(WinningOption WinningOption,
                            IEnumerable<TituloIsin> RankingTitulos,
                            Optional<Blue> Blue,
                            Optional<DolarWesternUnion> DolarWesternUnion,
                            IEnumerable<DolarCryptoPrices> RankingCryptos)
{
    public static readonly OptionsInfo NoOptions = new(WinningOption.NoWinners,
                                                        [],
                                                        Optional<Blue>.Empty,
                                                        Optional<DolarWesternUnion>.Empty,
                                                        []);
}
