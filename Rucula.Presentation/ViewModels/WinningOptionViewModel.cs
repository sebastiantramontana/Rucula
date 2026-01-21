using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

internal sealed record class WinningOptionViewModel(string Name, string Info, double? DolarPrice)
{
    internal static WinningOptionViewModel FromEntity(WinningOption winningOption)
        => new(winningOption.Name, winningOption.Info, winningOption.DolarPrice);
}
