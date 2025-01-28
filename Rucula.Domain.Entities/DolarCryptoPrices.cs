namespace Rucula.Domain.Entities;

public sealed record class DolarCryptoPrices(string ExchangeName, Optional<double> GrossUsdc, Optional<double> GrossUsdt, Optional<double> GrossDai, IEnumerable<DolarCryptoNetPrices> DolarCryptoNetPrices);
