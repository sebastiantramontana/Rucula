namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class DolarAppParameterValuesViewModel(double Volume, double Min, double Max)
    : ParameterValuesViewModelBase(Min, Max);