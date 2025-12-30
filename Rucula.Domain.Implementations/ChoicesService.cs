using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Implementations;

internal sealed class ChoicesService(ITitulosService titulosService,
                      IDolarBlueProvider dolarBlueProvider,
                      IWesternUnionService westernUnionService,
                      IDolarCryptoService dolarCryptoService,
                      IWinningChoiceService winningChoiceService) : IChoicesService
{
    public async Task ProcessChoices(ChoicesParameters parameters, ChoicesCallbacks choicesCallbacks)
    {
        var rankingCryptoTask = dolarCryptoService.GetPriceRanking(parameters.CryptoParameters, choicesCallbacks.OnCrypto);
        var dolarBlueTask = dolarBlueProvider.GetCurrentBlue(choicesCallbacks.OnBlue);
        var dolarWesternUnionTask = westernUnionService.GetDolarWesternUnion(parameters.WesternUnionParameters, choicesCallbacks.OnWesternUnion);

        var nonRankingTitulosTasks = Task.WhenAll(dolarBlueTask, dolarWesternUnionTask, rankingCryptoTask);
        var rankingTitulosTask = titulosService.GetNetCclRanking(await dolarBlueTask, parameters.BondCommissions, choicesCallbacks.OnBonds);

        await Task.WhenAll(nonRankingTitulosTasks, rankingTitulosTask);

        await winningChoiceService.CalculateWinningChoice(await rankingTitulosTask, await dolarWesternUnionTask, await rankingCryptoTask, choicesCallbacks.OnWinningChoice);
    }
}
