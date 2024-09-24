using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Globalization;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers;

internal class BlueMapper : IMapper<BlueDto, Blue>
{
    private readonly ISpanishNumberConverter _spanishNumberConverter;

    public BlueMapper(ISpanishNumberConverter spanishNumberConverter)
    {
        _spanishNumberConverter = spanishNumberConverter;
    }

    public Optional<Blue> Map(Optional<BlueDto> from)
        => from.HasValue ? Parse(from.Value) : Optional<Blue>.Empty;

    private Optional<Blue> Parse(BlueDto blue)
        => Optional<Blue>.Sure(new(Parse(blue.Compra), Parse(blue.Venta)));

    private double Parse(string value)
        => double.Parse(ConvertToEnglishFormat(value));

    private string ConvertToEnglishFormat(string value)
        => _spanishNumberConverter.ConvertToEnglish(value);
}
