namespace Rucula.Domain.Entities;

public sealed record class DolarCryptoGrossPrices(string ExchangeName, IEnumerable<DolarCryptoCurrencyGrossPrice> GrossPrices);
