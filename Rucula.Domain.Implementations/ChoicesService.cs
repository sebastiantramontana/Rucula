using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal class ChoicesService : IChoicesService
{
    private readonly ITitulosService _titulosService;
    private readonly IDolarBlueProvider _dolarBlueProvider;
    private readonly IWesternUnionService _westernUnionService;
    private readonly IDolarCryptoService _dolarCryptoService;

    public ChoicesService(ITitulosService titulosService,
                          IDolarBlueProvider dolarBlueProvider,
                          IWesternUnionService westernUnionService,
                          IDolarCryptoService dolarCryptoService)
    {
        _titulosService = titulosService;
        _dolarBlueProvider = dolarBlueProvider;
        _westernUnionService = westernUnionService;
        _dolarCryptoService = dolarCryptoService;
    }

    public async Task<ChoicesInfo> GetChoices(BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters cryptoParameters)
    {
        var dolarBlueTask = _dolarBlueProvider.GetCurrentBlue();
        var dolarWesternUnionTask = _westernUnionService.GetDolarWesternUnion(westernUnionParameters);
        var rankingCryptoTask = _dolarCryptoService.GetPriceRanking(cryptoParameters);

        await Task.WhenAll(dolarBlueTask, dolarWesternUnionTask, rankingCryptoTask).ConfigureAwait(false);

        var rankingTitulos = await _titulosService.GetNetCclRanking(await dolarBlueTask, bondCommissions).ConfigureAwait(false);

        return CreateWinningChoice(rankingTitulos, await dolarBlueTask, await dolarWesternUnionTask, await rankingCryptoTask);
    }

    public async Task<ChoicesInfo> RecalculateChoices(ChoicesInfo choices, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters cryptoParameters)
    {
        var rankingTitulos = _titulosService.RecalculateNetCclRanking(choices.RankingTitulos, bondCommissions);
        var westernUnionDolarTask = _westernUnionService.GetDolarWesternUnion(westernUnionParameters);
        var rankingCryptoTask = _dolarCryptoService.GetPriceRanking(cryptoParameters);

        await Task.WhenAll(westernUnionDolarTask, rankingCryptoTask).ConfigureAwait(false);

        return CreateWinningChoice(rankingTitulos, choices.Blue, await westernUnionDolarTask, await rankingCryptoTask);
    }

    private static ChoicesInfo CreateWinningChoice(IEnumerable<TituloIsin> rankingTitulos, Optional<Blue> dolarBlue, Optional<DolarWesternUnion> dolarWesternUnion, IEnumerable<DolarCryptoPrices> rankingCryptos)
    {
        var bestTitulo = MaybeFirst(rankingTitulos);
        var bestCrypto = MaybeFirst(rankingCryptos);

        var winner = GetWinningChoice(bestTitulo, dolarWesternUnion, bestCrypto);

        return new ChoicesInfo(winner ?? WinningChoice.NoWinners, rankingTitulos, dolarBlue, dolarWesternUnion, rankingCryptos);
    }

    private static WinningChoice? GetWinningChoice(Optional<TituloIsin> titulo, Optional<DolarWesternUnion> dolarWesternUnion, Optional<DolarCryptoPrices> bestCrypto)
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

        return winner;
    }

    private static DolarCryptoNetPrice GetBestCryptoNetPrice(DolarCryptoPrices topCryptoNetPrices)
        => topCryptoNetPrices.DolarCryptoNetPrices.First().TopNetPrice;

    private static WinningChoice CreateWinner(TituloIsin titulo)
        => new(titulo.TituloPeso!.Simbolo, "Incluye comisiones, pero no costos de transferencias", titulo.NetCcl!.Value);

    private static WinningChoice CreateWinner(DolarWesternUnion dolarWesternUnion)
        => new("Western Union", "Incluye costos", dolarWesternUnion.NetPrice);

    private static WinningChoice CreateWinner(DolarCryptoNetPrice dolarCryptoNetPrice)
        => new("Contado con Crypto", "Incluye comisiones de exchange y de retiro", dolarCryptoNetPrice.NetPrice);

    private static Optional<T> MaybeFirst<T>(IEnumerable<T> values)
        => Optional<T>.Maybe(values.FirstOrDefault());

}
