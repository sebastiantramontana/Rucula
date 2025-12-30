using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

internal sealed class CryptosViewModel
{
    internal IEnumerable<CryptoExchangeViewModel> Exchanges { get; private set; } = [];

    internal void Update(IEnumerable<DolarCryptoPrices> cryptos) 
        => Exchanges = cryptos.Select(CryptoExchangeViewModel.FromEntity);
}