using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class RuculaScreenPresenter(
    IChoicesService choicesService,
    IWinningChoicePresenter winningChoicePresenter,
    IBondsPresenter bondsPresenter,
    IWesternUnionPresenter westernUnionPresenter,
    ICryptosPresenter cryptosPresenter,
    IBluePresenter bluePresenter,
    IViewUpdater<RuculaScreenViewModel> viewUpdater) : IRuculaScreenPresenter
{
    private ChoicesInfo _currentChoices = ChoicesInfo.NoChoices;

    public async Task ShowChoices(RuculaScreenViewModel viewmodel, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters)
    {
        if (viewmodel.IsRunning)
            return;

        await StartRunning(viewmodel).ConfigureAwait(false);

        _currentChoices = await choicesService.GetChoices(bondCommissions, westernUnionParameters, dolarCryptoParameters).ConfigureAwait(false);

        var winnerTask = winningChoicePresenter.ShowWinner(_currentChoices.WinningChoice);
        var bondsTask = bondsPresenter.ShowBonds(_currentChoices.RankingTitulos);
        var wuTask = westernUnionPresenter.ShowWesternUnion(_currentChoices.DolarWesternUnion);
        var cryptosTask = cryptosPresenter.ShowCryptos(_currentChoices.RankingCryptos);
        var blueTask = bluePresenter.ShowBlue(_currentChoices.Blue);

        await Task.WhenAll(winnerTask, bondsTask, wuTask, cryptosTask, blueTask).ConfigureAwait(false);

        await StopRunning(viewmodel).ConfigureAwait(false);
    }

    public async Task ShowRecalculatedChoices(RuculaScreenViewModel viewmodel, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters)
    {
        if (viewmodel.IsRunning)
            return;

        await StartRunning(viewmodel).ConfigureAwait(false);

        _currentChoices = await choicesService.RecalculateChoices(_currentChoices, bondCommissions, westernUnionParameters, dolarCryptoParameters).ConfigureAwait(false);

        await StopRunning(viewmodel).ConfigureAwait(false);
    }

    private Task StartRunning(RuculaScreenViewModel viewmodel)
        => UpdateRunning(viewmodel, true);

    private Task StopRunning(RuculaScreenViewModel viewmodel)
        => UpdateRunning(viewmodel, false);

    private Task UpdateRunning(RuculaScreenViewModel viewmodel, bool isRunning)
    {
        viewmodel.Update(isRunning);
        return viewUpdater.Update(viewmodel);
    }
}
