using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Abstractions;

public interface IDolarCryptoService
{
    Task<IEnumerable<DolarCryptoPrices>> GetPriceRanking(DolarCryptoParameters cryptoParameters, Func<IEnumerable<DolarCryptoPrices>, Task> notifyFunc);
}