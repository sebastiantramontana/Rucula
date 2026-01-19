using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

internal sealed record class CryptosViewModel(IEnumerable<CryptoExchangeViewModel> Exchanges)
{
    internal static CryptosViewModel FromEntity(IEnumerable<DolarCryptoPrices> cryptos)
        => new(cryptos.Select(CryptoExchangeViewModel.FromEntity));
}