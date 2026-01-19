using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class CryptosPresenter(IViewUpdater<CryptosViewModel> viewUpdater) : ICryptosPresenter
{
    public Task ShowCryptos(IEnumerable<DolarCryptoPrices> cryptos)
        => viewUpdater.Update(CryptosViewModel.FromEntity(cryptos));
}