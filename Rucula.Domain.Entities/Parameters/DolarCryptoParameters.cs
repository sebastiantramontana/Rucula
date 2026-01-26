namespace Rucula.Domain.Entities.Parameters;

public sealed record class DolarCryptoParameters(double TradingVolume)
{
    private const double DefaultTradingVolume = 1000.0;
    private const double VolumeLowRange = 100.0;
    private const double VolumeHiRange = 10000.0;

    public static readonly DolarCryptoParameters Default = new(DefaultTradingVolume);
    public static readonly ParameterRange Range = new(VolumeLowRange, VolumeHiRange);
}