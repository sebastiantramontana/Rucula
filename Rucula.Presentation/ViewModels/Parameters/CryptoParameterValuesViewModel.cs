namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class CryptoParameterValuesViewModel(double Volume, double Min, double Max) 
    : ParameterValuesViewModelBase(Min, Max);