using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

public sealed class BondsViewModel
{
    internal IEnumerable<BondViewModel> Bonds { get; private set; } = [];

    internal void Update(IEnumerable<TituloIsin> bonds) 
        => Bonds = bonds.Select(BondViewModel.FromEntity);
}
