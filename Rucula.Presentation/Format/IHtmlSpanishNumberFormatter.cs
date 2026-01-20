namespace Rucula.Presentation.Format;

internal interface IHtmlSpanishNumberFormatter
{
    string Format(double? number);
    string Format(double? number, int digitCount);
}