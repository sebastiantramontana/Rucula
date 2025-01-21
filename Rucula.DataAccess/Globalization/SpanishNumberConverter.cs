namespace Rucula.DataAccess.Globalization;

internal class SpanishNumberConverter : ISpanishNumberConverter
{
    public string ConvertToEnglish(string number)
        => number
            .Replace(",", "@")
            .Replace(".", ",")
            .Replace("@", ".");
}
