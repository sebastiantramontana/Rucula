using Rucula.Domain.Entities;

namespace Rucula.Presentation.Presenters;

internal interface ICryptosPresenter
{
    Task ShowCryptos(IEnumerable<DolarCryptoPrices> cryptos);
}
