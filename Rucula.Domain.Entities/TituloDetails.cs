namespace Rucula.Domain.Entities;

public sealed record class TituloDetails(string CodigoIsin, string Denominacion, TipoObligacion TipoObligacion, DateOnly FechaVencimiento);
