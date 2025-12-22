using Rucula.Domain.Entities;

namespace Rucula.Presentation.Presenters;

internal interface IWinningChoicePresenter
{
    Task ShowWinner(WinningChoice winningChoice);
}
