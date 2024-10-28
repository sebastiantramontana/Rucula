using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IDolarCryptoGrossPricesProvider
{
    Task<IEnumerable<DolarCryptoGrossPrices>> GetGrossPrices(double volume, IEnumerable<string> currencyKeys);
}
