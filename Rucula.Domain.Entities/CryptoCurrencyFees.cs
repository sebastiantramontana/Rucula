﻿namespace Rucula.Domain.Entities;

public record class CryptoCurrencyFees(string CryptoCurrencyKey, IEnumerable<CurrencyBlockchainFee> BlockchainFees);
