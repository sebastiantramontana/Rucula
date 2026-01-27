namespace Rucula.Domain.Entities.Parameters;

public sealed record class DolarAppParameters(double Volume)
{
    private const double DefaultVolume = 1000.0;
    private const double VolumeLowRange = 100.0;
    private const double VolumeHiRange = 10000.0;

    public static readonly DolarCryptoParameters Default = new(DefaultVolume);
    public static readonly ParameterRange Range = new(VolumeLowRange, VolumeHiRange);
}