using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class DolarAppPresenter(IViewUpdater<DolarAppViewModel> viewUpdater) : IDolarAppPresenter
{
    public Task ShowDolarApp(Optional<DolarApp> dolarApp)
        => viewUpdater.Update(DolarAppViewModel.FromEntity(dolarApp));
}