using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers
{
    internal class TituloDetailsMapper : IMapper<TituloDetailsDto, TituloDetails>
    {
        public TituloDetails Map(TituloDetailsDto from)
            => new(from.CodigoIsin, from.Denominacion, MapTipoObligacion(from.TipoObligacion), DateOnly.FromDateTime(DateTime.Parse(from.FechaVencimiento)));

        private TipoObligacion MapTipoObligacion(string value)
            => value switch
            {
                "Valores Públicos Nacionales" => TipoObligacion.Nacional,
                _ => TipoObligacion.Otro
            };
    }
}
