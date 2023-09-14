namespace Rucula.Domain.Entities
{
    public sealed record class Titulo(string Simbolo,
                                      double PrecioCompra,
                                      double PrecioVenta,
                                      Parking Parking,
                                      Moneda Moneda);
}
