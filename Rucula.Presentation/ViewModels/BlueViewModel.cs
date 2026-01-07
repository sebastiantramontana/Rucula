using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

internal sealed record class BlueViewModel
{
    internal double? PurchasePrice { get; private set; } = null;
    internal double? SalePrice { get; private set; } = null;

    internal void Update(Optional<Blue> blue)
    {
        if (blue.IsEmpty)
        {
            PurchasePrice = null;
            SalePrice = null;
        }
        else
        {
            PurchasePrice = blue.Value.PrecioCompra;
            SalePrice = blue.Value.PrecioVenta;
        }
    }
}
