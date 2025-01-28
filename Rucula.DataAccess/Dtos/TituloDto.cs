namespace Rucula.DataAccess.Dtos;

internal sealed record class TituloDto(string Simbolo,
                                     double PrecioCompra,
                                     double PrecioVenta,
                                     string Parking,
                                     string Moneda);
