using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal class WesternUnionService : IWesternUnionService
{
    private readonly IWesternUnionProvider _westernUnionProvider;

    public WesternUnionService(IWesternUnionProvider westernUnionProvider)
        => _westernUnionProvider = westernUnionProvider;

    public async Task<Optional<DolarWesternUnion>> GetDolarWesternUnion(WesternUnionParameters parameters)
    {
        var info = await _westernUnionProvider.GetCurrentDolarWesternUnion(parameters).ConfigureAwait(false);

        if (!info.HasValue)
            return Optional<DolarWesternUnion>.Empty;

        var netPrice = info.Value.Fees.HasValue 
            ? CalculateNetPrice(info.Value.GrossPrice, info.Value.Fees.Value, parameters)
            : info.Value.GrossPrice;

        return Optional<DolarWesternUnion>.Sure(new(info.Value.GrossPrice, netPrice, info.Value.Fees));
    }

    private static double CalculateNetPrice(double grossPrice, double fees, WesternUnionParameters parameters)
        => (parameters.AmountToSend * grossPrice) / (parameters.AmountToSend + fees);
}
