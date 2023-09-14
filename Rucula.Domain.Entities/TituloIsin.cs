namespace Rucula.Domain.Entities
{
    public record class TituloIsin(string CodigoIsin,
                                   string Denominacion,
                                   Titulo TituloCable,
                                   Titulo TituloPeso,
                                   Titulo TituloMep,
                                   DateOnly Vencimiento,
                                   Blue Blue)
    {
        public double CotizacionCcl => TituloCable.PrecioCompra / TituloPeso.PrecioVenta;
        public double CotizacionCclMepBlue => Blue.PrecioCompra * PorcentajeArbitrajeCclMep;
        public double PorcentajeArbitrajeCclMepBlue => PasarAPorcentaje(CotizacionCclMepBlue / CotizacionCcl);
        public double PorcentajeArbitrajeCclMep => PasarAPorcentaje(CotizacionCcl / TituloMep.PrecioVenta);
        public double PorcentajeRuloMepBlue => PasarAPorcentaje(Blue.PrecioCompra / TituloMep.PrecioVenta);
        private double PasarAPorcentaje(double valor) => 100 * valor - 100;
    }
}
