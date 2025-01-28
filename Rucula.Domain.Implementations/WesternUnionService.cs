using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal sealed class WesternUnionService(IWesternUnionProvider westernUnionProvider) : IWesternUnionService
{
    public async Task<Optional<DolarWesternUnion>> GetDolarWesternUnion(WesternUnionParameters parameters)
    {
        var info = await westernUnionProvider.GetCurrentDolarWesternUnion(parameters).ConfigureAwait(false);

        if (!info.HasValue)
        {
            return Optional<DolarWesternUnion>.Empty;
        }

        var netPrice = CalculateNetPrice(info.Value.GrossPrice, info.Value.Fees, parameters);

        return Optional<DolarWesternUnion>.Sure(new(info.Value.GrossPrice, netPrice, info.Value.Fees));
    }

    private static double CalculateNetPrice(double grossPrice, double fees, WesternUnionParameters parameters)
        => (parameters.AmountToSend * grossPrice) / (parameters.AmountToSend + fees);
}
