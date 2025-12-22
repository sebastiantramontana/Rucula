using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class BluePresenter(BlueViewModel blueViewModel, IViewUpdater<BlueViewModel> viewUpdater) : IBluePresenter
{
    public Task ShowBlue(Optional<Blue> blue)
    {
        blueViewModel.Update(blue);
        return viewUpdater.Update(blueViewModel);
    }
}