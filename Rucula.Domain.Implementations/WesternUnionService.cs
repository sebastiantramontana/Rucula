using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Implementations;

internal sealed class WesternUnionService(IWesternUnionProvider westernUnionProvider, IDolarNetCalculator dolarNetCalculator) : IWesternUnionService
{
    public async Task<Optional<DolarWesternUnion>> GetDolarWesternUnion(WesternUnionParameters parameters, Action<Optional<DolarWesternUnion>> notifyFunc)
    {
        var wu = Optional<DolarWesternUnion>.Empty;
        var info = await westernUnionProvider.GetCurrentDolarWesternUnion(parameters);

        if (info.HasValue)
        {
            var netPrice = CalculateNetPrice(info.Value.GrossPrice, info.Value.FixedFee, parameters);
            wu = Optional<DolarWesternUnion>.Sure(new(info.Value.GrossPrice, netPrice, info.Value.FixedFee));
        }

        notifyFunc.Invoke(wu);
        return wu;
    }

    private double CalculateNetPrice(double grossPrice, double fixedFee, WesternUnionParameters parameters)
        => dolarNetCalculator.CalculateByFixedFee(grossPrice, parameters.AmountToSend, fixedFee);
}
