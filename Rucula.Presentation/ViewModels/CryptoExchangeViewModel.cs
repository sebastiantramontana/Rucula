using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

public sealed record class CryptoExchangeViewModel(string ExchangeName,
                                                   double? GrossUsdc,
                                                   double? GrossUsdt,
                                                   double? GrossDai,
                                                   IEnumerable<CryptoBlockchainViewModel> Blockchains)
{
    internal static CryptoExchangeViewModel FromEntity(DolarCryptoPrices crypto)
        => new(crypto.ExchangeName,
                GetValue(crypto.GrossUsdc),
                GetValue(crypto.GrossUsdt),
                GetValue(crypto.GrossDai),
                crypto.DolarCryptoNetPrices.Select(CryptoBlockchainViewModel.FromEntity));

    private static double? GetValue(Optional<double> optionalValue)
        => optionalValue.HasValue ? optionalValue.Value : null;
}
