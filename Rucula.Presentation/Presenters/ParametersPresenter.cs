using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;
using Rucula.Presentation.ViewModels;
using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class ParametersPresenter(
    IViewUpdater<SaveParametersViewModel> saveViewUpdater,
    IViewUpdater<RuculaScreenViewModel> validateViewUpdater,
    IViewUpdater<BondParameterValuesViewModel> bondParameterViewUpdater,
    IViewUpdater<CryptoParameterValuesViewModel> cryptoParameterViewUpdater,
    IViewUpdater<WesternUnionParameterValuesViewModel> WesternUnionParameterViewUpdater) : IParametersPresenter
{
    public Task SaveParameters(Result<OptionParameters> parameters)
    {
        var viewModel = UpdateParametersViewModel(parameters.Value);
        return saveViewUpdater.Update(viewModel);
    }

    public Task UpdateUIStateByParameters(Result<OptionParameters> parameters, bool areParametersDirty)
    {
        var viewModel = new RuculaScreenViewModel(false, !parameters.IsSuccess, areParametersDirty);
        return validateViewUpdater.Update(viewModel);
    }

    public Task ShowBondParameters(BondCommissions bondCommissions)
    {
        var bondViewModel = UpdateBondParametersViewModel(bondCommissions);
        return bondParameterViewUpdater.Update(bondViewModel.Values);
    }

    public Task ShowCryptoParameters(DolarCryptoParameters cryptoParameters)
    {
        var cryptoViewModel = UpdateCryptoParametersViewModel(cryptoParameters);
        return cryptoParameterViewUpdater.Update(cryptoViewModel.Values);
    }

    public Task ShowWesternUnionParameters(WesternUnionParameters wuParameters)
    {
        var wuViewModel = UpdateWesternUnionParametersViewModel(wuParameters);
        return WesternUnionParameterViewUpdater.Update(wuViewModel.Values);
    }

    private static SaveParametersViewModel UpdateParametersViewModel(OptionParameters parameters)
    {
        var bondParameters = UpdateBondParametersViewModel(parameters.BondCommissions);
        var cryptoParameters = UpdateCryptoParametersViewModel(parameters.CryptoParameters);
        var wuParameters = UpdateWesternUnionParametersViewModel(parameters.WesternUnionParameters);

        return new SaveParametersViewModel(bondParameters, cryptoParameters, wuParameters);
    }

    private static BondParametersViewModel UpdateBondParametersViewModel(BondCommissions bondCommissions)
    {
        var parameterValues = new BondParameterValuesViewModel(bondCommissions.PurchasePercentage, bondCommissions.SalePercentage, bondCommissions.WithdrawalPercentage);
        return new(parameterValues);
    }

    private static CryptoParametersViewModel UpdateCryptoParametersViewModel(DolarCryptoParameters cryptoParameters)
    {
        var parameterValues = new CryptoParameterValuesViewModel(cryptoParameters.TradingVolume);
        return new(parameterValues);
    }

    private static WesternUnionParametersViewModel UpdateWesternUnionParametersViewModel(WesternUnionParameters westernUnionParameters)
    {
        var parameterValues = new WesternUnionParameterValuesViewModel(westernUnionParameters.AmountToSend);
        return new(parameterValues);
    }
}