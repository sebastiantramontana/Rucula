namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class BondParametersViewModel(BondParameterValuesViewModel Values) : ParametersViewModelBase<BondParameterValuesViewModel>("commisions-bond-settings", Values)
{
    protected override string GetStringifiedJsonValues()
        => $$"""
            {
                "purchasePercentage": {{Values.PurchasePercentage}},
                "salePercentage": {{Values.SalePercentage}},
                "withdrawalPercentage": {{Values.WithdrawalPercentage}}
            }
            """;

    public override string ToString()
        => StringifyJson();
}
