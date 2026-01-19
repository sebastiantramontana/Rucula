using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

internal sealed record class BlueViewModel(double? PurchasePrice, double? SalePrice)
{
    internal static BlueViewModel FromEntity(Optional<Blue> blue)
        => blue.IsEmpty
            ? new BlueViewModel(null, null)
            : new BlueViewModel(blue.Value.PrecioCompra, blue.Value.PrecioVenta);
}
