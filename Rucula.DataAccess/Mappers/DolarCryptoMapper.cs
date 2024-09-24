using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Globalization;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers;

internal class DolarCryptoMapper : IMapper<DolarCryptoDto, DolarCrypto>
{
    private readonly ISpanishNumberConverter _spanishNumberConverter;

    public DolarCryptoMapper(ISpanishNumberConverter spanishNumberConverter) 
        => _spanishNumberConverter = spanishNumberConverter;

    public Optional<DolarCrypto> Map(Optional<DolarCryptoDto> from)
        => from.HasValue ? Parse(from) : Optional<DolarCrypto>.Empty;

    private Optional<DolarCrypto> Parse(Optional<DolarCryptoDto> cryptoDto)
        => Optional<DolarCrypto>.Sure(new(Parse(cryptoDto.Value.Compra), Parse(cryptoDto.Value.Venta)));

    private double Parse(string value)
        => double.Parse(ConvertToEnglishFormat(value));

    private string ConvertToEnglishFormat(string value)
        => _spanishNumberConverter.ConvertToEnglish(value);
}
