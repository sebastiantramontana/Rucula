using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

internal sealed record class BondsViewModel(IEnumerable<BondViewModel> Bonds)
{
    internal static BondsViewModel FromEntity(IEnumerable<TituloIsin> bonds)
        => new(bonds.Select(BondViewModel.FromEntity));
}
