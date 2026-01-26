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
    public Task Start(OptionalOptionParameters initialParameters)
    {
        const bool ParametersAreDirty = false;

        var bondParameters = ResolveParameters(initialParameters.BondCommissions, BondCommissions.Default);
        var cryptoParameters = ResolveParameters(initialParameters.CryptoParameters, DolarCryptoParameters.Default);
        var wuParameters = ResolveParameters(initialParameters.WesternUnionParameters, WesternUnionParameters.Default);

        parametersRepository.StoreParameters(Result<OptionParameters>.Success(new(bondParameters, cryptoParameters, wuParameters)), ParametersAreDirty);

        var bondParamsTask = parametersPresenter.ShowBondParameters(bondParameters);
        var cryptoParamsTask = parametersPresenter.ShowCryptoParameters(cryptoParameters);
        var wuParamsTask = parametersPresenter.ShowWesternUnionParameters(wuParameters);
        var startOptionTask = screenPresenter.ShowOptions(viewModel);

        return Task.WhenAll(bondParamsTask, cryptoParamsTask, wuParamsTask, startOptionTask);
    }

    private static T ResolveParameters<T>(Optional<T> optionalParameters, T defaultParameters)
        => optionalParameters.HasValue ? optionalParameters.Value : defaultParameters;
}
