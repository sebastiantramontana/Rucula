using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities.Parameters;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class RuculaScreenPresenter(
    IRestartingPeriodicRunnerService restartingPeriodicRunnerService,
    IChoicesService choicesService,
    IWinningChoicePresenter winningChoicePresenter,
    IBondsPresenter bondsPresenter,
    IWesternUnionPresenter westernUnionPresenter,
    ICryptosPresenter cryptosPresenter,
    IBluePresenter bluePresenter,
    IViewUpdater<RuculaScreenViewModel> viewUpdater) : IRuculaScreenPresenter
{
    public Task StartShowChoicesFromScratch(ChoicesParameters parameters)
        => StartShowChoices(new RuculaScreenViewModel(), parameters);

    public Task StartShowChoices(RuculaScreenViewModel viewmodel, ChoicesParameters parameters)
        => viewmodel.IsRunning
            ? Task.CompletedTask
            : restartingPeriodicRunnerService.Restart(() => RunChoices(viewmodel, parameters), TimeSpan.FromMinutes(1));

    private async Task RunChoices(RuculaScreenViewModel viewmodel, ChoicesParameters parameters)
    {
        await ShowStartRunning(viewmodel);

        var callbacks = new ChoicesCallbacks(
            winningChoicePresenter.ShowWinner,
            bondsPresenter.ShowBonds,
            bluePresenter.ShowBlue,
            westernUnionPresenter.ShowWesternUnion,
            cryptosPresenter.ShowCryptos);

        await choicesService.ProcessChoices(parameters, callbacks);

        await ShowStopRunning(viewmodel);
    }

    private Task ShowStartRunning(RuculaScreenViewModel viewmodel)
        => ShowRunning(viewmodel, true);

    private Task ShowStopRunning(RuculaScreenViewModel viewmodel)
        => ShowRunning(viewmodel, false);

    private Task ShowRunning(RuculaScreenViewModel viewmodel, bool isRunning)
    {
        viewmodel.Update(isRunning);
        return viewUpdater.Update(viewmodel);
    }
}
