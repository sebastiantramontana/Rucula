namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class WesternUnionParametersViewModel(WesternUnionParameterValuesViewModel Values) : ParametersViewModelBase<WesternUnionParameterValuesViewModel>("parameters-wu-settings", Values)
{
    protected override string GetStringifiedJsonValues() 
        => $$"""
            {
                "amountToSend": {{Values.AmountToSend}}
            }
            """;

    public override string ToString()
        => StringifyJson();
}