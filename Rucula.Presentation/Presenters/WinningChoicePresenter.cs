using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.WebAssembly.ViewModels;

namespace Rucula.Presentation.Presenters;

internal interface IWinningChoicePresenter
{
    Task GetChoices(WinningChoiceViewModel viewmodel, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters);
    Task RecalculateChoices(WinningChoiceViewModel viewmodel, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters);
}

internal class WinningChoicePresenter(IChoicesService choiceService) : IWinningChoicePresenter
{
    private ChoicesInfo _currentChoices = ChoicesInfo.NoChoices;

    public async Task GetChoices(WinningChoiceViewModel viewmodel, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters)
    {
        _currentChoices = await choiceService.GetChoices(bondCommissions, westernUnionParameters, dolarCryptoParameters);
    }

    public Task RecalculateChoices(WinningChoiceViewModel viewmodel, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters) => throw new NotImplementedException();
}
