using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

public sealed record class WinningChoiceViewModel
{
    public string Name { get; private set; } = string.Empty;
    public string Info { get; private set; } = string.Empty;
    public double? DolarPrice { get; private set; } = null;

    public void Update(WinningChoice winningChoice)
    {
        Name = winningChoice.Name;
        Info = winningChoice.Info;
        DolarPrice = winningChoice.DolarPrice;
    }
}
