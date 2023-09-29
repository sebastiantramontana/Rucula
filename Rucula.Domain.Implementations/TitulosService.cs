using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Linq;

namespace Rucula.Domain.Implementations
{
    internal class TitulosService : ITitulosService
    {
        private readonly ITitulosProvider _titulosProvider;
        private readonly ITituloDetailsProvider _tituloDetailsProvider;
        private readonly IDolarBlueProvider _dolarBlueProvider;

        public TitulosService(ITitulosProvider titulosProvider, ITituloDetailsProvider tituloDetailsProvider, IDolarBlueProvider dolarBlueProvider)
        {
            _titulosProvider = titulosProvider;
            _tituloDetailsProvider = tituloDetailsProvider;
            _dolarBlueProvider = dolarBlueProvider;
        }

        public Task<IEnumerable<Titulo>> GetAllTitulos()
        {
            return _titulosProvider.Get();
        }

        public async Task<IEnumerable<TituloIsin>> GetCclRankingTitulosIsin()
        {
            var titulos = await _titulosProvider.Get();
            var details = await GetUsefulTitulosDetails(titulos);
            var blue = await _dolarBlueProvider.GetCurrentBlue();
            return CreateTitulosIsin(details, blue);
        }

        private async Task<IEnumerable<(Titulo Titulo, TituloDetails? Details)>> GetUsefulTitulosDetails(IEnumerable<Titulo> titulos)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var detailsContentList = new List<(Titulo Titulo, TituloDetails? Details)>();

            foreach (var t in titulos)
            {
                var allDetails = await GetTituloDetails(t);
                var usefulDetails = allDetails.FirstOrDefault(d => IsDetailsUseful(d, today));
                detailsContentList.Add((t, usefulDetails));
            }

            return detailsContentList;
        }

        private Task<IEnumerable<TituloDetails>> GetTituloDetails(Titulo titulo)
            => _tituloDetailsProvider.GetTituloDetails(titulo.Simbolo);

        private IEnumerable<TituloIsin> CreateTitulosIsin(IEnumerable<(Titulo Titulo, TituloDetails? TituloDetails)> details, Blue blue)
        {
            return details
                .Where(d => d.TituloDetails is not null)
                .GroupBy(d => d.TituloDetails!)
                .Select(g => CreateTituloIsin(g, blue))
                .Where(t => t.CotizacionCcl is not null)
                .OrderByDescending(t => t.CotizacionCcl);
        }

        private TituloIsin CreateTituloIsin(IGrouping<TituloDetails, (Titulo Titulo, TituloDetails? TituloDetails)> values, Blue blue)
        {
            var isin = values.Key.CodigoIsin;
            var denominacion = values.Key.Denominacion;
            var tituloCable = GetTitulo(values, Moneda.DolarCable);
            var tituloPeso = GetTitulo(values, Moneda.Peso);
            var tituloMep = GetTitulo(values, Moneda.DolarMep);
            var fechaVencimiento = values.Key.FechaVencimiento;
            var ccl = CalculateCcl(tituloPeso, tituloCable);
            var mep = CalculateMep(tituloPeso, tituloMep);
            var cclMepBlue = CalculateCclMepBlue(blue, ccl, mep);
            var percCclMepBlue = CalculatePercentageCclMepBlue(cclMepBlue, ccl);
            var percCclMep = CalculatePercentageCclMep(ccl, mep);

            return new(isin,
                       denominacion,
                       tituloCable,
                       tituloPeso,
                       tituloMep,
                       fechaVencimiento,
                       ccl,
                       cclMepBlue,
                       percCclMepBlue,
                       percCclMep);
        }

        private Titulo? GetTitulo(IGrouping<TituloDetails, (Titulo Titulo, TituloDetails? TituloDetails)> tuples, Moneda moneda)
        {
            return tuples
                .Select(t => t.Titulo)
                .SingleOrDefault(t => IsTituloUseful(t, moneda));
        }

        private bool IsDetailsUseful(TituloDetails tituloDetails, DateOnly today)
            => tituloDetails.FechaVencimiento > today
            && tituloDetails.TipoObligacion == TipoObligacion.Nacional;

        private bool IsTituloUseful(Titulo titulo, Moneda moneda)
            => (titulo.PrecioCompra > 0.0 || titulo.PrecioVenta > 0.0)
                && titulo.Moneda == moneda
                && titulo.Parking == Parking.CI;

        private double? CalculateCcl(Titulo? tituloPeso, Titulo? tituloCable)
            => Divide(tituloPeso?.PrecioCompra, tituloCable?.PrecioVenta);

        private double? CalculateCclMepBlue(Blue blue, double? ccl, double? mep)
            => blue.PrecioCompra * Divide(ccl, mep);

        private double? CalculatePercentageCclMepBlue(double? cclMepBlue, double? ccl)
            => ConvertToPercentaje(Divide(cclMepBlue, ccl));

        private double? CalculatePercentageCclMep(double? ccl, double? mep)
            => ConvertToPercentaje(Divide(ccl, mep));

        private double? CalculateMep(Titulo? tituloPesos, Titulo? tituloMep)
            => Divide(tituloPesos?.PrecioVenta, tituloMep?.PrecioCompra);

        private double? ConvertToPercentaje(double? valor)
            => 100 * valor - 100;

        private double? Divide(double? value1, double? value2)
            => value1.HasValue && value2.HasValue && value2.Value != 0
                ? value1.Value / value2.Value
                : null;
    }
}
