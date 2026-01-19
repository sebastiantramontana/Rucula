using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class BondsPresenter(IViewUpdater<BondsViewModel> viewUpdater) : IBondsPresenter
{
    public Task ShowBonds(IEnumerable<TituloIsin> bonds)
        => viewUpdater.Update(BondsViewModel.FromEntity(bonds));
}