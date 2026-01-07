namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class WesternUnionParametersViewModel() : ParametersViewModelBase<WesternUnionParameterValuesViewModel>("parameters-wu-settings")
{
    protected override string GetStringifiedJsonValues() 
        => $$"""
            {
                "amountToSend": {{Values.AmountToSend}}
            }
            """;

    public override string ToString()
        => StringifydJson();
}