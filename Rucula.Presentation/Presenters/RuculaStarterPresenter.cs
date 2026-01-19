using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;
using Rucula.Presentation.Repositories;
using Rucula.Presentation.ViewModels;

namespace Rucula.Presentation.Presenters;

internal sealed class RuculaStarterPresenter(
    RuculaScreenViewModel viewModel,
    IRuculaScreenPresenter screenPresenter,
    IParametersPresenter parametersPresenter,
    IParametersRepository parametersRepository) : IRuculaStarterPresenter
{
    public Task Start(ChoicesParameters initialParameters)
    {
        const bool ParametersAreDirty = false;
        parametersRepository.StoreParameters(Result<ChoicesParameters>.Success(initialParameters), ParametersAreDirty);

        var bondParamsTask = parametersPresenter.ShowBondParameters(initialParameters.BondCommissions);
        var cryptoParamsTask = parametersPresenter.ShowCryptoParameters(initialParameters.CryptoParameters);
        var wuParamsTask = parametersPresenter.ShowWesternUnionParameters(initialParameters.WesternUnionParameters);
        var startChoicesTask = screenPresenter.StartShowChoices(viewModel);

        return Task.WhenAll(bondParamsTask, cryptoParamsTask, wuParamsTask, startChoicesTask);
    }
}
