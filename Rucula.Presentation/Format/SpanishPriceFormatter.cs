namespace Rucula.Presentation.Format;

internal sealed class SpanishPriceFormatter(ISpanishPriceFormatProvider provider) : ISpanishPriceFormatter
{
    public string Format(double? dolarPrice)
        => dolarPrice?.ToString(provider) ?? string.Empty;
}
