using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Globalization;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers;

internal class DolarDiarcoMapper : IMapper<DolarDiarcoDto, DolarDiarco>
{
    private readonly ISpanishNumberConverter _spanishNumberConverter;

    public DolarDiarcoMapper(ISpanishNumberConverter spanishNumberConverter)
    {
        _spanishNumberConverter = spanishNumberConverter;
    }

    public DolarDiarco Map(DolarDiarcoDto from)
    {
        var description = from.DescriptionContainingPrice;
        var priceString = ExtractPriceFromDescription(description);
        var priceStringEnglish = ConvertToEnglishFormat(priceString);
        var price = ParsePrice(priceStringEnglish);

        return new(price);
    }

    private static double? ParsePrice(string? priceString)
        => double.TryParse(priceString, out var price) ? price : null;

    private static string? ExtractPriceFromDescription(string? description)
    {
        if (description is null)
            return null;

        const string StartToken = "COTIZACIÓN: $";
        const string EndToken = "Tomamos tus dólares";

        var startIndex = description.IndexOf(StartToken) + StartToken.Length;
        var endIndex = description.IndexOf(EndToken);

        return description?[startIndex..endIndex]?.Trim();
    }

    private string? ConvertToEnglishFormat(string? value)
        => value is not null ? _spanishNumberConverter.ConvertToEnglish(value) : null;
}
