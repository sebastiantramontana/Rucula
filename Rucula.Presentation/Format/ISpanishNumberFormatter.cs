namespace Rucula.Presentation.Format;

internal interface ISpanishNumberFormatter
{
    string? Format(double? number);
    string? Format(double? number, int digitCount);
}
