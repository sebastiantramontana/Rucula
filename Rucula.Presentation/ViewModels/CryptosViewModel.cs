using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

public sealed class CryptosViewModel
{
    public IEnumerable<CryptoExchangeViewModel> Exchanges { get; private set; } = [];

    internal void Update(IEnumerable<DolarCryptoPrices> cryptos) 
        => Exchanges = cryptos.Select(CryptoExchangeViewModel.FromEntity);
}