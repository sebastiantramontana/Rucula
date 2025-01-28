namespace Rucula.DataAccess.Globalization;

internal sealed class SpanishNumberConverter : ISpanishNumberConverter
{
    public string ConvertToEnglish(string number)
        => number
            .Replace(",", "@")
            .Replace(".", ",")
            .Replace("@", ".");
}
