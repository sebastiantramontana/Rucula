namespace Rucula.Domain.Entities.Parameters;

public sealed record class WesternUnionParameters(double AmountToSend)
{
    private const double DefaultAmountToSend = 2000.0;
    private const double AmountToSendLowRange = 100.0;
    private const double AmountToSendHiRange = 10000.0;

    public static readonly WesternUnionParameters Default = new(DefaultAmountToSend);
    public static readonly ParameterRange Range = new(AmountToSendLowRange, AmountToSendHiRange);
}