using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Application;

internal sealed class BestOptionService(ITitulosService titulosService,
                      IDolarBlueProvider dolarBlueProvider,
                      IWesternUnionService westernUnionService,
                      IDolarCryptoService dolarCryptoService,
                      IWinningOptionService winningOptionService) : IBestOptionService
{
    public async Task ProcessOptions(OptionParameters parameters, OptionCallbacks optionCallbacks)
    {
        var rankingCryptoTask = dolarCryptoService.GetPriceRanking(parameters.CryptoParameters, optionCallbacks.OnCrypto);
        var dolarBlueTask = dolarBlueProvider.GetCurrentBlue(optionCallbacks.OnBlue);
        var dolarWesternUnionTask = westernUnionService.GetDolarWesternUnion(parameters.WesternUnionParameters, optionCallbacks.OnWesternUnion);

        var nonRankingTitulosTasks = Task.WhenAll(dolarBlueTask, dolarWesternUnionTask, rankingCryptoTask);
        var rankingTitulosTask = titulosService.GetNetCclRanking(await dolarBlueTask, parameters.BondCommissions, optionCallbacks.OnBonds);

        await Task.WhenAll(nonRankingTitulosTasks, rankingTitulosTask);

        await winningOptionService.CalculateWinner(await rankingTitulosTask, await dolarWesternUnionTask, await rankingCryptoTask, optionCallbacks.OnWinningOption);
    }
}
