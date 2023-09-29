namespace Rucula.Domain.Entities
{
    public record class TituloIsin(string CodigoIsin,
                                   string Denominacion,
                                   Titulo? TituloCable,
                                   Titulo? TituloPeso,
                                   Titulo? TituloMep,
                                   DateOnly Vencimiento,
                                   double? CotizacionCcl,
                                   double? CotizacionCclMepBlue,
                                   double? PorcentajeArbitrajeCclMepBlue,
                                   double? PorcentajeArbitrajeCclMep);
}
