using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IDolarCryptoService
{
    Task<IEnumerable<DolarCryptoPrices>> GetPriceRanking(DolarCryptoParameters cryptoParameters);
}