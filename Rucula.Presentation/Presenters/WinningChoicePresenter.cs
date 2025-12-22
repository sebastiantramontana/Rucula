using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class WinningChoicePresenter(
    WinningChoiceViewModel winningChoiceViewModel,
    IViewUpdater<WinningChoiceViewModel> viewUpdater) : IWinningChoicePresenter
{
    public Task ShowWinner(WinningChoice winningChoice)
    {
        winningChoiceViewModel.Update(winningChoice);
        return viewUpdater.Update(winningChoiceViewModel);
    }
}