using Rucula.Domain.Entities;

namespace Rucula.WebAssembly.ViewModels;

public sealed class WinningChoiceViewModel
{
    public string Name { get; private set; } = string.Empty;
    public string Info { get; private set; } = string.Empty;
    public double? DolarPrice { get; private set; }

    public void Update(WinningChoice winningChoice)
    {
        Name = winningChoice.Name;
        Info = winningChoice.Info;
        DolarPrice = winningChoice.DolarPrice;
    }

    public async Task GetChoices(BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters )
    {

    }

    public async Task RecalculateChoices(BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters)
    {

    }
}
