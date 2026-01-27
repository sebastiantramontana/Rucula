namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class DolarAppParametersViewModel(DolarAppParameterValuesViewModel Values) : ParametersViewModelBase<DolarAppParameterValuesViewModel>("parameters-dolarapp-settings", Values)
{
    protected override string GetStringifiedJsonValues()
        => $$"""
            {
                "volume": {{Values.Volume}}
            }
            """;

    public override string ToString()
        => StringifyJson();
}