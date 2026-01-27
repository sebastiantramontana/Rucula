using Rucula.Application;
using Rucula.Domain.Entities;
using Rucula.Presentation.Repositories;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class RuculaScreenPresenter(
    IRestartingPeriodicRunnerService restartingPeriodicRunnerService,
    IBestOptionService bestOptionService,
    IWinningOptionPresenter winningOptionPresenter,
    IBondsPresenter bondsPresenter,
    IWesternUnionPresenter westernUnionPresenter,
    ICryptosPresenter cryptosPresenter,
    IBluePresenter bluePresenter,
    IDolarAppPresenter dolarAppPresenter,
    IParametersProvider parametersProvider,
    IViewUpdater<RuculaScreenViewModel> viewUpdater) : IRuculaScreenPresenter
{
    public Task ShowOptions(RuculaScreenViewModel viewmodel)
        => viewmodel.IsRunning
            ? Task.CompletedTask
            : restartingPeriodicRunnerService.Restart(RunOptions, TimeSpan.FromMinutes(5));

    private async Task RunOptions()
    {
        var parameters = parametersProvider.GetParameters();

        if (parameters.IsSuccess)
        {
            await ShowStartRunning(parametersProvider.AreDirty);

            var callbacks = CreateCallbacks();
            await bestOptionService.ProcessOptions(parameters.Value, callbacks);

            await ShowStopRunning(parametersProvider.AreDirty);
        }
        else
        {
            await ShowByInvalidParameters(parametersProvider.AreDirty);
        }
    }

    private OptionCallbacks CreateCallbacks()
        => new(
            CreateFireForgetCallback<WinningOption>(winningOptionPresenter.ShowWinner),
            CreateFireForgetCallback<IEnumerable<TituloIsin>>(bondsPresenter.ShowBonds),
            CreateFireForgetCallback<Optional<Blue>>(bluePresenter.ShowBlue),
            CreateFireForgetCallback<Optional<DolarWesternUnion>>(westernUnionPresenter.ShowWesternUnion),
            CreateFireForgetCallback<IEnumerable<DolarCryptoPrices>>(cryptosPresenter.ShowCryptos),
            CreateFireForgetCallback<Optional<DolarApp>>(dolarAppPresenter.ShowDolarApp));

    private static Action<T> CreateFireForgetCallback<T>(Func<T, Task> forgottenAsyncFunc)
        => (obj) => _ = forgottenAsyncFunc(obj);

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
