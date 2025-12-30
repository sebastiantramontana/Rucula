namespace Rucula.Presentation.Format;

internal sealed class SpanishPriceFormatProvider : ISpanishPriceFormatProvider
{
    private const int DefaultDigitCount = 3;

    public object? GetFormat(Type? formatType)
        => this;

    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        var number = arg switch
        {
            float or double or decimal => FormatFloatPoint(format, (IFormattable)arg),
            _ => FormatDefault(arg)
        };

        return ConvertToSpanish(number);
    }

    private static string FormatFloatPoint(string? format, IFormattable formattable)
    {
        var digitCount = GetDigitCount(format);
        return formattable.ToString($"F{digitCount}", null);
    }

    private static string FormatDefault(object? arg)
        => arg?.ToString() ?? string.Empty;

    private static int GetDigitCount(string? format)
        => string.IsNullOrEmpty(format) ? DefaultDigitCount : int.Parse(format);

    private static string ConvertToSpanish(string number)
        => number
            .Replace(",", "@")
            .Replace(".", ",")
            .Replace("@", ".");
}
