namespace Rucula.Presentation.Format;

internal sealed class SpanishNumberFormatter : ISpanishNumberFormatter
{
    private const int DefaultDigitCount = 2;

    public string? Format(double? number, int digitCount)
        => FormatToSpanish(number, digitCount);

    public string? Format(double? number)
        => FormatToSpanish(number, DefaultDigitCount);

    private static string? FormatToSpanish(double? number, int digitCount)
    {
        if (number is null)
            return null;

        var formattedNumber = FormatFloatPoint(number, digitCount);
        return ConvertToSpanish(formattedNumber);
    }

    private static string FormatFloatPoint(IFormattable formattable, int digitCount)
        => formattable.ToString($"F{digitCount}", null);

    private static string ConvertToSpanish(string formattedNumber)
        => formattedNumber
            .Replace(",", "@")
            .Replace(".", ",")
            .Replace("@", ".");
}
