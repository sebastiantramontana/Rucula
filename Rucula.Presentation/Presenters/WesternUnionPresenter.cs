using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class WesternUnionPresenter(
    WesternUnionViewModel westernUnionViewModel,
    IViewUpdater<WesternUnionViewModel> viewUpdater) : IWesternUnionPresenter
{
    public Task ShowWesternUnion(Optional<DolarWesternUnion> dolarWesternUnion)
    {
        westernUnionViewModel.Update(dolarWesternUnion);
        return viewUpdater.Update(westernUnionViewModel);
    }
}