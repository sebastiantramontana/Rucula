namespace Rucula.Domain.Entities;

public record class DolarCryptoNetPrices(Blockchain Blockchain, DolarCryptoNetPrice NetUsdc, DolarCryptoNetPrice NetUsdt, DolarCryptoNetPrice NetDai);