using Rucula.Domain.Entities;

namespace Rucula.Presentation.Presenters;

internal interface IWinningOptionPresenter
{
    Task ShowWinner(WinningOption winningOption);
}
