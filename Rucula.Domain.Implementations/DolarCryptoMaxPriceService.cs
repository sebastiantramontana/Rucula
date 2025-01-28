using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal sealed class DolarCryptoMaxPriceService : IDolarCryptoMaxPriceService
{
    public DolarCryptoNetPrice? MaxNetPrice(Optional<DolarCryptoNetPrice> netUsdc, Optional<DolarCryptoNetPrice> netUsdt, Optional<DolarCryptoNetPrice> netDai)
    {
        var prices = new List<DolarCryptoNetPrice>();

        AddNetPriceToListIfHasValue(prices, netUsdc);
        AddNetPriceToListIfHasValue(prices, netUsdt);
        AddNetPriceToListIfHasValue(prices, netDai);

        return prices.MaxBy(p => p.NetPrice);

        static void AddNetPriceToListIfHasValue(ICollection<DolarCryptoNetPrice> prices, Optional<DolarCryptoNetPrice> netPrice)
        {
            if (netPrice.HasValue)
            {
                prices.Add(netPrice.Value);
            }
        }
    }
}
