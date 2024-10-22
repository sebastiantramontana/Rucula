namespace Rucula.Domain.Entities;

public record class DolarCryptoFees(string ExchangeName, IEnumerable<CryptoCurrencyFees> CryptoCurrencyFees);
