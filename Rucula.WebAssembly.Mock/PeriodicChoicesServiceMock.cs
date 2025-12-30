using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities.Parameters;
using Rucula.WebAssembly.Mock;

namespace Rucula.WebAssembly;

public sealed class PeriodicChoicesServiceMock : IPeriodicChoicesService
{
    private ChoicesInfo _choicesInfo = default!;
    public void UpdateMokedChoices(ChoicesInfo choicesInfo) 
        => _choicesInfo = choicesInfo;

    public Task StartProcessChoices(ChoicesParameters parameters, TimeSpan interval, ChoicesCallbacks choicesCallbacks)
    {
        var winnerTask = choicesCallbacks.OnWinningChoice.Invoke(_choicesInfo.WinningChoice);
        var bondsTask = choicesCallbacks.OnBonds.Invoke(_choicesInfo.RankingTitulos);
        var cryptoTask = choicesCallbacks.OnCrypto.Invoke(_choicesInfo.RankingCryptos);
        var blueTask = choicesCallbacks.OnBlue.Invoke(_choicesInfo.Blue);
        var wuTask = choicesCallbacks.OnWesternUnion.Invoke(_choicesInfo.DolarWesternUnion);

        return Task.WhenAll(winnerTask, bondsTask, cryptoTask, blueTask, wuTask);
    }
}