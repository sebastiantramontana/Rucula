using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Application;

internal sealed class BestOptionService(ITitulosService titulosService,
                      IDolarBlueProvider dolarBlueProvider,
                      IWesternUnionService westernUnionService,
                      IDolarCryptoService dolarCryptoService,
                      IDolarAppService dolarAppService,
                      IWinningOptionService winningOptionService) : IBestOptionService
{
    public async Task ProcessOptions(OptionParameters parameters, OptionCallbacks optionCallbacks)
    {
        var rankingCryptoTask = dolarCryptoService.GetPriceRanking(parameters.CryptoParameters, optionCallbacks.OnCrypto);
        var dolarBlueTask = dolarBlueProvider.GetCurrentBlue(optionCallbacks.OnBlue);
        var dolarWesternUnionTask = westernUnionService.GetDolarWesternUnion(parameters.WesternUnionParameters, optionCallbacks.OnWesternUnion);
        var dolarAppTask = dolarAppService.GetDolarApp(parameters.DolarAppParameters, optionCallbacks.OnDolarApp);

        var nonRankingTitulosTasks = Task.WhenAll(dolarBlueTask, dolarWesternUnionTask, rankingCryptoTask, dolarAppTask);
        var rankingTitulosTask = titulosService.GetNetCclRanking(await dolarBlueTask, parameters.BondCommissions, optionCallbacks.OnBonds);

        await Task.WhenAll(nonRankingTitulosTasks, rankingTitulosTask);

        winningOptionService.CalculateWinner(rankingTitulosTask.Result, dolarWesternUnionTask.Result, rankingCryptoTask.Result, dolarAppTask.Result, optionCallbacks.OnWinningOption);
    }
}
