namespace Rucula.Domain.Entities;

public record class DolarCryptoNetPrices(Blockchain Blockchain, Optional<DolarCryptoNetPrice> NetUsdc, Optional<DolarCryptoNetPrice> NetUsdt, Optional<DolarCryptoNetPrice> NetDai, DolarCryptoNetPrice TopNetPrice);