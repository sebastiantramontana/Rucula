using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class WesternUnionPresenter(IViewUpdater<WesternUnionViewModel> viewUpdater) : IWesternUnionPresenter
{
    public Task ShowWesternUnion(Optional<DolarWesternUnion> dolarWesternUnion)
        => viewUpdater.Update(WesternUnionViewModel.FromEntity(dolarWesternUnion));
}