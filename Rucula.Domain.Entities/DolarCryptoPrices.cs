namespace Rucula.Domain.Entities;

public record DolarCryptoPrices(string ExchangeName, Optional<double> GrossUsdc, Optional<double> GrossUsdt, Optional<double> GrossDai, IEnumerable<DolarCryptoNetPrices> DolarCryptoNetPrices);
