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
        var winnerTask = optionCallbacks.OnWinningOption.Invoke(_optionInfo.WinningOption);
        var bondsTask = optionCallbacks.OnBonds.Invoke(_optionInfo.RankingTitulos);
        var cryptoTask = optionCallbacks.OnCrypto.Invoke(_optionInfo.RankingCryptos);
        var blueTask = optionCallbacks.OnBlue.Invoke(_optionInfo.Blue);
        var wuTask = optionCallbacks.OnWesternUnion.Invoke(_optionInfo.DolarWesternUnion);

        return Task.WhenAll(winnerTask, bondsTask, cryptoTask, blueTask, wuTask);
    }
}