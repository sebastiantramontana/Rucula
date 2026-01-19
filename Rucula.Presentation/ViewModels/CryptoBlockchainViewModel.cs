using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

internal sealed record class CryptoBlockchainViewModel(string BlockchainName,
                                                     double? UsdcFee,
                                                     double? NetUsdc,
                                                     double? UsdtFee,
                                                     double? NetUsdt,
                                                     double? DaiFee,
                                                     double? NetDai)
{
    internal static CryptoBlockchainViewModel FromEntity(DolarCryptoNetPrices crypto) 
        => new(crypto.Blockchain.Name,
                GetFee(crypto.NetUsdc),
                GetNet(crypto.NetUsdc),
                GetFee(crypto.NetUsdt),
                GetNet(crypto.NetUsdt),
                GetFee(crypto.NetDai),
                GetNet(crypto.NetDai)); //VER EL TEMA DE TopNetPrice PARA EL RING

    private static double? GetFee(Optional<DolarCryptoNetPrice> price)
        => GetValue(price, p => p.Fee);

    private static double? GetNet(Optional<DolarCryptoNetPrice> price)
        => GetValue(price, p => p.NetPrice);

    private static double? GetValue(Optional<DolarCryptoNetPrice> price, Func<DolarCryptoNetPrice, double> getValueFunc)
        => price.HasValue ? getValueFunc.Invoke(price.Value) : null;
}
