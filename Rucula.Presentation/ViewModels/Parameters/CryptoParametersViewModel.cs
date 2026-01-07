namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class CryptoParametersViewModel() : ParametersViewModelBase<CryptoParameterValuesViewModel>("parameters-crypto-settings")
{
    protected override string GetStringifiedJsonValues() 
        => $$"""
            {
                "volume": {{Values.Volume}}
            }
            """;

    public override string ToString()
        => StringifydJson();
}