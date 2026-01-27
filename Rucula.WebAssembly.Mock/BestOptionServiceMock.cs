using Rucula.Application;
using Rucula.Domain.Entities.Parameters;
using Rucula.WebAssembly.Mock;

namespace Rucula.WebAssembly;

public sealed class BestOptionServiceMock : IBestOptionService
{
    private OptionsInfo _optionInfo = default!;
    public void UpdateMockedOptions(OptionsInfo optionInfo)
        => _optionInfo = optionInfo;

    public Task ProcessOptions(OptionParameters parameters, OptionCallbacks optionCallbacks)
    {
        optionCallbacks.OnWinningOption.Invoke(_optionInfo.WinningOption);
        optionCallbacks.OnBonds.Invoke(_optionInfo.RankingTitulos);
        optionCallbacks.OnCrypto.Invoke(_optionInfo.RankingCryptos);
        optionCallbacks.OnBlue.Invoke(_optionInfo.Blue);
        optionCallbacks.OnWesternUnion.Invoke(_optionInfo.DolarWesternUnion);
        optionCallbacks.OnDolarApp.Invoke(_optionInfo.DolarApp);

        return Task.CompletedTask;
    }
}