namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class WesternUnionParameterValuesViewModel(double AmountToSend, double Min, double Max) 
    : ParameterValuesViewModelBase(Min, Max);
