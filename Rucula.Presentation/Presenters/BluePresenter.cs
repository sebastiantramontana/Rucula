using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class BluePresenter(IViewUpdater<BlueViewModel> viewUpdater) : IBluePresenter
{
    public Task ShowBlue(Optional<Blue> blue)
        => viewUpdater.Update(BlueViewModel.FromEntity(blue));
}