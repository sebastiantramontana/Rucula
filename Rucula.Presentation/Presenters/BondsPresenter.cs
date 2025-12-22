using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class BondsPresenter(BondsViewModel bondsViewModel, IViewUpdater<BondsViewModel> viewUpdater) : IBondsPresenter
{
    public Task ShowBonds(IEnumerable<TituloIsin> bonds)
    {
        bondsViewModel.Update(bonds);
        return viewUpdater.Update(bondsViewModel);
    }
}