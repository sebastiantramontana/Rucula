using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal class ChoicesService : IChoicesService
{
    private readonly ITitulosService _titulosService;
    private readonly IDolarBlueProvider _dolarBlueProvider;
    private readonly IDolarCryptoProvider _dolarCryptoProvider;
    private readonly IWesternUnionService _westernUnionService;

    public ChoicesService(ITitulosService titulosService,
                          IDolarBlueProvider dolarBlueProvider,
                          IDolarCryptoProvider dolarCryptoProvider,
                          IWesternUnionService westernUnionService)
    {
        _titulosService = titulosService;
        _dolarBlueProvider = dolarBlueProvider;
        _dolarCryptoProvider = dolarCryptoProvider;
        _westernUnionService = westernUnionService;
    }

    public async Task<ChoicesInfo> GetChoices(BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters)
    {
        var dolarBlueTask = _dolarBlueProvider.GetCurrentBlue();
        var dolarCryptoTask = _dolarCryptoProvider.GetCurrentDolarCrypto();
        var dolarWesternUnionTask = _westernUnionService.GetDolarWesternUnion(westernUnionParameters);

        await Task.WhenAll(dolarBlueTask, dolarCryptoTask, dolarWesternUnionTask).ConfigureAwait(false);

        var rankingTitulos = await _titulosService.GetNetCclRanking(dolarBlueTask.Result, bondCommissions).ConfigureAwait(false);

        return CreateWinningChoice(rankingTitulos, dolarBlueTask.Result, dolarCryptoTask.Result, dolarWesternUnionTask.Result);
    }

    public async Task<ChoicesInfo> RecalculateChoices(ChoicesInfo choices, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters)
    {
        var rankingTitulos = _titulosService.RecalculateNetCclRanking(choices.RankingTitulos, bondCommissions);
        var westernUnionDolar = await _westernUnionService.GetDolarWesternUnion(westernUnionParameters);

        return CreateWinningChoice(rankingTitulos, choices.Blue, choices.DolarCrypto, westernUnionDolar);
    }

    private static ChoicesInfo CreateWinningChoice(IEnumerable<TituloIsin> rankingTitulos, Optional<Blue> dolarBlue, Optional<DolarCrypto> dolarCrypto, Optional<DolarWesternUnion> dolarWesternUnion)
    {
        var bestTitulo = rankingTitulos.FirstOrDefault();
        var winner = GetWinningChoice(Optional<TituloIsin>.Maybe(bestTitulo), dolarCrypto, dolarWesternUnion);

        return new ChoicesInfo(winner ?? WinningChoice.NoWinners, rankingTitulos, dolarBlue, dolarCrypto, dolarWesternUnion);
    }

    private static WinningChoice? GetWinningChoice(Optional<TituloIsin> titulo, Optional<DolarCrypto> dolarCrypto, Optional<DolarWesternUnion> dolarWesternUnion)
    {
        var competitors = new List<WinningChoice>(3);

        if (titulo.HasValue)
            competitors.Add(CreateWinner(titulo.Value));

        if (dolarCrypto.HasValue)
            competitors.Add(CreateWinner(dolarCrypto.Value));

        if (dolarWesternUnion.HasValue)
            competitors.Add(CreateWinner(dolarWesternUnion.Value));

        var winner = competitors
                    .OrderByDescending(w => w.DolarPrice)
                    .FirstOrDefault();

        return winner;
    }

    private static WinningChoice CreateWinner(TituloIsin titulo)
        => new(titulo.TituloPeso!.Simbolo, "Incluye comisiones, pero no costos de transferencias", titulo.NetCcl!.Value);

    private static WinningChoice CreateWinner(DolarCrypto dolarCrypto)
        => new("Dolar Crypto", string.Empty, dolarCrypto.PrecioCompra);

    private static WinningChoice CreateWinner(DolarWesternUnion dolarWesternUnion)
        => new("Western Union", "Incluye costos", dolarWesternUnion.NetPrice);
}
