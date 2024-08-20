namespace Rucula.Domain.Entities
{
    public record class ChoicesInfo(WinningChoice WinningChoice,
                                IEnumerable<TituloIsin> RankingTitulos,
                                Blue Blue,
                                DolarCrypto DolarCrypto,
                                DolarWesternUnion DolarWesternUnion,
                                DolarDiarco DolarDiarco)
    {
        public static readonly ChoicesInfo NoChoices = new ChoicesInfo(WinningChoice.NoWinners,
                                                                       Enumerable.Empty<TituloIsin>(),
                                                                       new Blue(double.NaN, double.NaN),
                                                                       new DolarCrypto(double.NaN, double.NaN),
                                                                       new DolarWesternUnion(null),
                                                                       new DolarDiarco(null));
    }
}
