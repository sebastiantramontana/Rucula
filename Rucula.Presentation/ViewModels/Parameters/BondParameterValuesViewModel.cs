using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class BondParameterValuesViewModel
{
    internal double PurchasePercentage { get; private set; } = 0.0;
    internal double SalePercentage { get; private set; } = 0.0;
    internal double WithdrawalPercentage { get; private set; } = 0.0;

    public void Update(BondCommissions entity)
    {
        PurchasePercentage = entity.PurchasePercentage;
        SalePercentage = entity.SalePercentage;
        WithdrawalPercentage = entity.WithdrawalPercentage;
    }
}

