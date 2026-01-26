namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class BondParameterValuesViewModel(double PurchasePercentage, double SalePercentage, double WithdrawalPercentage, double Min, double Max)
    : ParameterValuesViewModelBase(Min, Max);
