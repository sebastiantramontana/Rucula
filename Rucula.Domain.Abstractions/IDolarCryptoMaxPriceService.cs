using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IDolarCryptoMaxPriceService
{
    DolarCryptoNetPrice? MaxNetPrice(Optional<DolarCryptoNetPrice> netUsdc, Optional<DolarCryptoNetPrice> netUsdt, Optional<DolarCryptoNetPrice> netDai);
}
