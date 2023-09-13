namespace Rucula.DataAccess.Dtos
{
    public sealed record class TituloDto(string Simbolo,
                                         double PrecioCompra,
                                         double PrecioVenta,
                                         Parking Parking,
                                         Moneda Moneda);
}
