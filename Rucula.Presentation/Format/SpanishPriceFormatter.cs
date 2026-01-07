namespace Rucula.Presentation.Format;

internal sealed class SpanishPriceFormatter : ISpanishPriceFormatter
{
    private const int DefaultDigitCount = 2;

    public string Format(double? dolarPrice, int digitCount)
        => FormatToSpanish(dolarPrice, digitCount);

    public string Format(double? dolarPrice)
        => FormatToSpanish(dolarPrice, null);

    private static string FormatToSpanish(double? dolarPrice, int? digitCount)
    {
        var formattedNumber = dolarPrice is not null
            ? FormatFloatPoint(dolarPrice, null)
            : string.Empty;

        return ConvertToSpanish(formattedNumber);
    }

    private static string FormatFloatPoint(IFormattable formattable, int? digitCount)
    {
        var finalDigitCount = GetFinalDigitCount(digitCount);
        return formattable.ToString($"F{finalDigitCount}", null);
    }

    private static int GetFinalDigitCount(int? digitCount)
        => digitCount ?? DefaultDigitCount;

    private static string ConvertToSpanish(string formattedNumber)
        => formattedNumber
            .Replace(",", "@")
            .Replace(".", ",")
            .Replace("@", ".");
}
