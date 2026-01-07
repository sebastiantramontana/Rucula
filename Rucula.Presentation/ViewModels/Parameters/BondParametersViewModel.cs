namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class BondParametersViewModel() : ParametersViewModelBase<BondParameterValuesViewModel>("commisions-bond-settings")
{
    protected override string GetStringifiedJsonValues()
        => $$"""
            {
                "purchasePercentage": {{Values.PurchasePercentage}},
                "salePercentage": {{Values.SalePercentage}},
                "WithdrawalPercentage": {{Values.WithdrawalPercentage}}
            }
            """;

    public override string ToString()
        => StringifydJson();
}
