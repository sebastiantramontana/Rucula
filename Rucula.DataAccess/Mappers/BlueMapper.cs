using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Globalization;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers;

internal sealed class BlueMapper(ISpanishNumberConverter spanishNumberConverter) : IMapper<BlueDto, Blue>
{
    public Optional<Blue> Map(Optional<BlueDto> from)
        => from.HasValue ? Parse(from.Value) : Optional<Blue>.Empty;

    private Optional<Blue> Parse(BlueDto blue)
        => Optional<Blue>.Sure(new(Parse(blue.Compra), Parse(blue.Venta)));

    private double Parse(string value)
        => double.Parse(ConvertToEnglishFormat(value));

    private string ConvertToEnglishFormat(string value)
        => spanishNumberConverter.ConvertToEnglish(value);
}
