using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers;

internal class TituloDetailsMapper : IMapper<TituloDetailsDto, TituloDetails>
{
    private const string TextoObligacionNacional = "Valores Públicos Nacionales";

    public Optional<TituloDetails> Map(Optional<TituloDetailsDto> from)
        => from.HasValue ? Create(from.Value) : Optional<TituloDetails>.Empty;

    private static Optional<TituloDetails> Create(TituloDetailsDto from)
        => Optional<TituloDetails>.Sure(new(from.CodigoIsin, from.Denominacion, MapTipoObligacion(from.TipoObligacion), DateOnly.FromDateTime(DateTime.Parse(from.FechaVencimiento))));

    private static TipoObligacion MapTipoObligacion(string value)
        => value == TextoObligacionNacional ? TipoObligacion.Nacional : TipoObligacion.Otro;
}
