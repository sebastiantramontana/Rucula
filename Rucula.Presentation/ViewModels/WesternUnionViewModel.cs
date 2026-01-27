using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

internal sealed record class WesternUnionViewModel(double? GrossPrice, double? NetPrice, double? Fees)
{
    internal static WesternUnionViewModel FromEntity(Optional<DolarWesternUnion> dolarWesternUnion)
        => dolarWesternUnion.HasValue
            ? new(dolarWesternUnion.Value.GrossPrice, dolarWesternUnion.Value.NetPrice, dolarWesternUnion.Value.FixedFee)
            : new(null, null, null);
}
