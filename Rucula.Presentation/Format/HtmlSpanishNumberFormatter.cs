namespace Rucula.Presentation.Format;

internal sealed class HtmlSpanishNumberFormatter(ISpanishNumberFormatter spanishNumberFormatter) : IHtmlSpanishNumberFormatter
{
    private const string HtmlNonBreakSpace = "\u00A0";

    public string Format(double? number)
        => spanishNumberFormatter.Format(number) ?? HtmlNonBreakSpace;

    public string Format(double? number, int digitCount)
        => spanishNumberFormatter.Format(number, digitCount) ?? HtmlNonBreakSpace;
}
