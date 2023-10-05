using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Globalization;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers
{
    internal class BlueMapper : IMapper<BlueDto, Blue>
    {
        private readonly ISpanishNumberConverter _spanishNumberConverter;

        public BlueMapper(ISpanishNumberConverter spanishNumberConverter)
        {
            _spanishNumberConverter = spanishNumberConverter;
        }

        public Blue Map(BlueDto from)
            => new(Parse(from.Compra), Parse(from.Venta));

        private double Parse(string value)
            => double.Parse(ConvertToEnglishFormat(value));

        private string ConvertToEnglishFormat(string value)
            => _spanishNumberConverter.ConvertToEnglish(value);
    }
}
