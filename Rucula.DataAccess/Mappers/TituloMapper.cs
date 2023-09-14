using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers
{
    internal class TituloMapper : IMapper<TituloDto, Titulo>
    {
        public Titulo Map(TituloDto from)
            => new Titulo(from.Simbolo, from.PrecioCompra, from.PrecioVenta, MapToParking(from.Parking), MapToMoneda(from.Moneda));

        private Parking MapToParking(string settlementType)
            => settlementType switch
            {
                "1" => Parking.CI,
                "2" => Parking.T24,
                "3" => Parking.T48,
                _ => throw new NotImplementedException($"Valor inválido para parking: {settlementType}")
            };

        private Moneda MapToMoneda(string denominationCcy)
            => denominationCcy switch
            {
                "ARS" => Moneda.Peso,
                "EXT" => Moneda.DolarCable,
                "USD" => Moneda.DolarMep,
                _ => throw new NotImplementedException($"Valor inválido para moneda: {denominationCcy}")
            };
    }
}
