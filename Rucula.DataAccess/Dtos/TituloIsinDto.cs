namespace Rucula.DataAccess.Dtos
{
    public record class TituloIsinDto(string CodigoIsin,
                                      string Denominacion,
                                      TituloDto TituloCable,
                                      TituloDto TituloPeso,
                                      TituloDto TituloMep,
                                      DateOnly Vencimiento)
    {
        public double Ccl => TituloCable.PrecioCompra / TituloPeso.PrecioVenta;
        public double ArbitrajeCclMep => Ccl / TituloMep.PrecioVenta;
    }
}
