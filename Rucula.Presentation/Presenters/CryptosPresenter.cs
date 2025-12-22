using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class CryptosPresenter(CryptosViewModel cryptosViewModel, IViewUpdater<CryptosViewModel> viewUpdater) : ICryptosPresenter
{
    public Task ShowCryptos(IEnumerable<DolarCryptoPrices> cryptos)
    {
        cryptosViewModel.Update(cryptos);
        return viewUpdater.Update(cryptosViewModel);
    }
}