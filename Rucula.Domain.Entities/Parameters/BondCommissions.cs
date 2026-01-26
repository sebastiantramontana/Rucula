namespace Rucula.Domain.Entities.Parameters;

public sealed record class BondCommissions(double PurchasePercentage, double SalePercentage, double WithdrawalPercentage)
{
    private const double DefaultPurchasePercentage = 0.5;
    private const double DefaultSalePercentage = 0.5;
    private const double DefaultWithdrawalPercentage = 0.5;
    private const double LowRange = 0.0;
    private const double HiRange = 5.0;

    public static readonly BondCommissions Default = new(DefaultPurchasePercentage, DefaultSalePercentage, DefaultWithdrawalPercentage);
    public static readonly ParameterRange Range = new(LowRange, HiRange);
}
