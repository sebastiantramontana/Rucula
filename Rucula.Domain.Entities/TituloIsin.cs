namespace Rucula.Domain.Entities;

public sealed record class TituloIsin(string CodigoIsin,
                               string Denominacion,
                               Titulo? TituloCable,
                               Titulo? TituloPeso,
                               Titulo? TituloMep,
                               DateOnly Vencimiento,
                               double? GrossCcl,
                               double? NetCcl,
                               double? MepOverCable,
                               double? BlueOverCcl);
