using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Implementations;

internal sealed class WesternUnionService(IWesternUnionProvider westernUnionProvider) : IWesternUnionService
{
    public async Task<Optional<DolarWesternUnion>> GetDolarWesternUnion(WesternUnionParameters parameters, Func<Optional<DolarWesternUnion>, Task> notifyFunc)
    {
        var wu = Optional<DolarWesternUnion>.Empty;
        var info = await westernUnionProvider.GetCurrentDolarWesternUnion(parameters);

        if (info.HasValue)
        {
            var netPrice = CalculateNetPrice(info.Value.GrossPrice, info.Value.Fees, parameters);
            wu = Optional<DolarWesternUnion>.Sure(new(info.Value.GrossPrice, netPrice, info.Value.Fees));
        }

        await notifyFunc.Invoke(wu);

        return wu;
    }

    private static double CalculateNetPrice(double grossPrice, double fees, WesternUnionParameters parameters)
        => (parameters.AmountToSend * grossPrice) / (parameters.AmountToSend + fees);
}
