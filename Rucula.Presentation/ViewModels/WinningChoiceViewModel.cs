using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

internal sealed record class WinningChoiceViewModel(string Name, string Info, double? DolarPrice)
{
    internal static WinningChoiceViewModel FromEntity(WinningChoice winningChoice)
        => new(winningChoice.Name, winningChoice.Info, winningChoice.DolarPrice);
}
