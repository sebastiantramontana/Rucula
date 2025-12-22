using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

public sealed class BlueViewModel
{
    internal double? PurchasePrice { get; private set; } = null;
    internal double? SellingPrice { get; private set; } = null;

    internal void Update(Optional<Blue> blue)
    {
        if (blue.IsEmpty)
        {
            PurchasePrice = null;
            SellingPrice = null;
        }
        else
        {
            PurchasePrice = blue.Value.PrecioCompra;
            SellingPrice = blue.Value.PrecioVenta;
        }
    }
}
