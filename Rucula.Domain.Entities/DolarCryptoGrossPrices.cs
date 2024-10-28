namespace Rucula.Domain.Entities;

public record DolarCryptoGrossPrices(string ExchangeName, IEnumerable<DolarCryptoCurrencyGrossPrice> GrossPrices);
