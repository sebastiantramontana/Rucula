namespace Rucula.Domain.Entities;

public record DolarCryptoPrices(string ExchangeName, double GrossUsdc, double GrossUsdt, double GrossDai, IEnumerable<DolarCryptoNetPrices> DolarCryptoNetPrices);
