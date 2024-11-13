namespace Rucula.Domain.Entities;

public record class ChoicesInfo(WinningChoice WinningChoice,
                            IEnumerable<TituloIsin> RankingTitulos,
                            Optional<Blue> Blue,
                            Optional<DolarWesternUnion> DolarWesternUnion,
                            Optional<DolarDiarco> DolarDiarco,
                            IEnumerable<DolarCryptoPrices> RankingCryptos)
{
    public static readonly ChoicesInfo NoChoices = new(WinningChoice.NoWinners,
                                                        [],
                                                        Optional<Blue>.Empty,
                                                        Optional<DolarWesternUnion>.Empty,
                                                        Optional<DolarDiarco>.Empty,
                                                        []);
}
