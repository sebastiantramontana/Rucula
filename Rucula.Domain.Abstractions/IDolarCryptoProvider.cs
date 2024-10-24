﻿using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IDolarCryptoProvider
{
    Task<Optional<DolarCrypto>> GetCurrentDolarCrypto();
}
