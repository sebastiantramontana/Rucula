using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Globalization;

namespace Rucula.DataAccess.Mappers
{
    internal class BlueMapper : IMapper<BlueDto, Blue>
    {
        public Blue Map(BlueDto from)
            => new(Parse(from.Compra), Parse(from.Venta));

        private double Parse(string value)
            => double.Parse(value, new CultureInfo("es-AR"));
    }
}
