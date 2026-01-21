using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class WinningOptionPresenter(IViewUpdater<WinningOptionViewModel> viewUpdater) : IWinningOptionPresenter
{
    public Task ShowWinner(WinningOption winningOption)
        => viewUpdater.Update(WinningOptionViewModel.FromEntity(winningOption));
}