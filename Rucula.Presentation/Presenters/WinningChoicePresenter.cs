using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class WinningChoicePresenter(IViewUpdater<WinningChoiceViewModel> viewUpdater) : IWinningChoicePresenter
{
    public Task ShowWinner(WinningChoice winningChoice)
        => viewUpdater.Update(WinningChoiceViewModel.FromEntity(winningChoice));
}