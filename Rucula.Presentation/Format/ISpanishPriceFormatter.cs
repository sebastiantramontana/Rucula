namespace Rucula.Presentation.Format;

internal interface ISpanishPriceFormatter
{
    string Format(double? dolarPrice);
    string Format(double? dolarPrice, int digitCount);
}