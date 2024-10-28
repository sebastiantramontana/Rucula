using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal class ChoicesService : IChoicesService
{
    private readonly ITitulosService _titulosService;
    private readonly IDolarBlueProvider _dolarBlueProvider;
    private readonly IWesternUnionService _westernUnionService;
    private readonly IDolarDiarcoProvider _dolarDiarcoProvider;

    public ChoicesService(ITitulosService titulosService,
                          IDolarBlueProvider dolarBlueProvider,
                          IWesternUnionService westernUnionService,
                          IDolarDiarcoProvider dolarDiarcoProvider)
    {
        _titulosService = titulosService;
        _dolarBlueProvider = dolarBlueProvider;
        _westernUnionService = westernUnionService;
        _dolarDiarcoProvider = dolarDiarcoProvider;
    }

    public async Task<ChoicesInfo> GetChoices(BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters)
    {
        var dolarBlueTask = _dolarBlueProvider.GetCurrentBlue();
        var dolarWesternUnionTask = _westernUnionService.GetDolarWesternUnion(westernUnionParameters);
        var dolarDiarcoTask = _dolarDiarcoProvider.GetCurrentDolarDiarco();

        await Task.WhenAll(dolarBlueTask, dolarWesternUnionTask, dolarDiarcoTask).ConfigureAwait(false);

        var rankingTitulos = await _titulosService.GetNetCclRanking(dolarBlueTask.Result, bondCommissions).ConfigureAwait(false);

        return CreateWinningChoice(rankingTitulos, dolarBlueTask.Result, dolarWesternUnionTask.Result, dolarDiarcoTask.Result);
    }

    public async Task<ChoicesInfo> RecalculateChoices(ChoicesInfo choices, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters)
    {
        var rankingTitulos = _titulosService.RecalculateNetCclRanking(choices.RankingTitulos, bondCommissions);
        var westernUnionDolar = await _westernUnionService.GetDolarWesternUnion(westernUnionParameters);

        return CreateWinningChoice(rankingTitulos, choices.Blue, westernUnionDolar, choices.DolarDiarco);
    }

    private static ChoicesInfo CreateWinningChoice(IEnumerable<TituloIsin> rankingTitulos, Optional<Blue> dolarBlue, Optional<DolarWesternUnion> dolarWesternUnion, Optional<DolarDiarco> dolarDiarco)
    {
        var bestTitulo = rankingTitulos.FirstOrDefault();
        var winner = GetWinningChoice(Optional<TituloIsin>.Maybe(bestTitulo), dolarWesternUnion);

        return new ChoicesInfo(winner ?? WinningChoice.NoWinners, rankingTitulos, dolarBlue, dolarWesternUnion, dolarDiarco);
    }

    private static WinningChoice? GetWinningChoice(Optional<TituloIsin> titulo, Optional<DolarWesternUnion> dolarWesternUnion)
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

        var winner = competitors
                    .OrderByDescending(w => w.DolarPrice)
                    .FirstOrDefault();

        return winner;
    }

    private static WinningChoice CreateWinner(TituloIsin titulo)
        => new(titulo.TituloPeso!.Simbolo, "Incluye comisiones, pero no costos de transferencias", titulo.NetCcl!.Value);

    private static WinningChoice CreateWinner(DolarWesternUnion dolarWesternUnion)
        => new("Western Union", "Incluye costos", dolarWesternUnion.NetPrice);
}
