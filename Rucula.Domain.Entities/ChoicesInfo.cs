namespace Rucula.Domain.Entities;

public record class ChoicesInfo(WinningChoice WinningChoice,
                            IEnumerable<TituloIsin> RankingTitulos,
                            Optional<Blue> Blue,
                            Optional<DolarCrypto> DolarCrypto,
                            Optional<DolarWesternUnion> DolarWesternUnion,
                            Optional<DolarDiarco> DolarDiarco)
{
    public static readonly ChoicesInfo NoChoices = new (WinningChoice.NoWinners,
                                                        [],
                                                        Optional<Blue>.Empty,
                                                        Optional<DolarCrypto>.Empty,
                                                        Optional<DolarWesternUnion>.Empty,
                                                        Optional<DolarDiarco>.Empty);
}
