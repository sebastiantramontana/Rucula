namespace Rucula.Domain.Entities;

public sealed record class CryptoCurrencyFees(string CryptoCurrencyKey, IEnumerable<CurrencyBlockchainFee> BlockchainFees);
