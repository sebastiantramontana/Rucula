namespace Rucula.Domain.Entities;

public record class TituloDetails(string CodigoIsin, string Denominacion, TipoObligacion TipoObligacion, DateOnly FechaVencimiento);
