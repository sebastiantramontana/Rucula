using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed class CryptoParameterValuesViewModel
{
    internal double Volume { get; private set; } = 1000.0;

    public void Update(DolarCryptoParameters cryptoParameters)
        => Volume = cryptoParameters.TradingVolume;
}