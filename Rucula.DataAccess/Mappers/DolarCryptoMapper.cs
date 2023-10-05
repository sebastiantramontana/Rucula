using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Globalization;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers
{
    internal class DolarCryptoMapper : IMapper<DolarCryptoDto, DolarCrypto>
    {
        private readonly ISpanishNumberConverter _spanishNumberConverter;

        public DolarCryptoMapper(ISpanishNumberConverter spanishNumberConverter)
        {
            _spanishNumberConverter = spanishNumberConverter;
        }

        public DolarCrypto Map(DolarCryptoDto from)
            => new(Parse(from.Compra), Parse(from.Venta));

        private double Parse(string value)
            => double.Parse(ConvertToEnglishFormat(value));

        private string ConvertToEnglishFormat(string value)
            => _spanishNumberConverter.ConvertToEnglish(value);
    }
}
