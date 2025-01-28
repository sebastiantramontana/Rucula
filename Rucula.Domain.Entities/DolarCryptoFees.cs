namespace Rucula.Domain.Entities;

public sealed record class DolarCryptoFees(string ExchangeName, IEnumerable<CryptoCurrencyFees> CryptoCurrencyFees);
