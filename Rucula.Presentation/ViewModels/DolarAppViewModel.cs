using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

internal sealed record class DolarAppViewModel(double? GrossPrice, double? NetPrice, double? FixedFee)
{
    internal static DolarAppViewModel FromEntity(Optional<DolarApp> dolarApp)
        => dolarApp.HasValue
            ? new(dolarApp.Value.GrossPrice, dolarApp.Value.NetPrice, dolarApp.Value.FixedFee)
            : new(null, null, null);
}
