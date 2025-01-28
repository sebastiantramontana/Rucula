namespace Rucula.Domain.Entities;

public sealed record class ChoicesInfo(WinningChoice WinningChoice,
                            IEnumerable<TituloIsin> RankingTitulos,
                            Optional<Blue> Blue,
                            Optional<DolarWesternUnion> DolarWesternUnion,
                            IEnumerable<DolarCryptoPrices> RankingCryptos)
{
    public static readonly ChoicesInfo NoChoices = new(WinningChoice.NoWinners,
                                                        [],
                                                        Optional<Blue>.Empty,
                                                        Optional<DolarWesternUnion>.Empty,
                                                        []);
}
