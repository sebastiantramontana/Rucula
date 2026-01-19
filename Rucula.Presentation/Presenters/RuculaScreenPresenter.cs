using Rucula.Domain.Abstractions;
using Rucula.Presentation.Repositories;
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
    IParametersProvider parametersProvider,
    IViewUpdater<RuculaScreenViewModel> viewUpdater) : IRuculaScreenPresenter
{
    public Task StartShowChoices(RuculaScreenViewModel viewmodel)
        => viewmodel.IsRunning
            ? Task.CompletedTask
            : restartingPeriodicRunnerService.Restart(RunChoices, TimeSpan.FromMinutes(5));

    private async Task RunChoices()
    {
        var parameters = parametersProvider.GetParameters();

        if (parameters.IsSuccess)
        {
            await ShowStartRunning(parametersProvider.AreDirty);

            var callbacks = CreateCallbacks();
            await choicesService.ProcessChoices(parameters.Value, callbacks);

            await ShowStopRunning(parametersProvider.AreDirty);
        }
        else
        {
            await ShowByInvalidParameters(parametersProvider.AreDirty);
        }
    }

    private ChoicesCallbacks CreateCallbacks()
        => new(winningChoicePresenter.ShowWinner,
            bondsPresenter.ShowBonds,
            bluePresenter.ShowBlue,
            westernUnionPresenter.ShowWesternUnion,
            cryptosPresenter.ShowCryptos);

    private Task ShowStartRunning(bool areParametersDirty)
        => ShowRunning(true, areParametersDirty);

    private Task ShowStopRunning(bool areParametersDirty)
        => ShowRunning(false, areParametersDirty);

    private Task ShowRunning(bool isRunning, bool areParametersDirty)
        => UpdateView(isRunning, false, areParametersDirty);

    private Task ShowByInvalidParameters(bool areParametersDirty)
        => UpdateView(false, true, areParametersDirty);

    private Task UpdateView(bool isRunning, bool areParametersInvalid, bool areParametersDirty)
    {
        var viewmodel = new RuculaScreenViewModel(isRunning, areParametersInvalid, areParametersDirty);
        return viewUpdater.Update(viewmodel);
    }
}
