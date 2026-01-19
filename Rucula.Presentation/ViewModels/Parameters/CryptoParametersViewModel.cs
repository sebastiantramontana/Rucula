namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class CryptoParametersViewModel(CryptoParameterValuesViewModel Values) : ParametersViewModelBase<CryptoParameterValuesViewModel>("parameters-crypto-settings", Values)
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