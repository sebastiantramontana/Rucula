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

        private async Task<IEnumerable<(Titulo Titulo, TituloDetails Details)>> GetUsefulTitulosDetails(IEnumerable<Titulo> titulos)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var detailsContentList = new List<(Titulo Titulo, TituloDetails Details)>();

            foreach (var t in titulos)
            {
                var allDetails = await GetTituloDetails(t);
                var usefulDetails = allDetails.First(d => IsDetailsUseful(d, today));
                detailsContentList.Add((t, usefulDetails));
            }

            return detailsContentList;
        }

        private Task<IEnumerable<TituloDetails>> GetTituloDetails(Titulo titulo)
            => _tituloDetailsProvider.GetTituloDetails(titulo.Simbolo);

        private IEnumerable<TituloIsin> CreateTitulosIsin(IEnumerable<(Titulo Titulo, TituloDetails TituloDetails)> details, Blue blue)
        {
            return details
                .GroupBy(d => d.TituloDetails!)
                .Select(g => CreateTituloIsin(g, blue))
                .Where(t => t.CotizacionCcl is not null)
                .OrderByDescending(t => t.CotizacionCcl);
        }

        private TituloIsin CreateTituloIsin(IGrouping<TituloDetails, (Titulo Titulo, TituloDetails TituloDetails)> values, Blue blue)
        {
            var isin = values.Key.CodigoIsin;
            var denominacion = values.Key.Denominacion;
            var tituloCable = GetTitulo(values, Moneda.DolarCable);
            var tituloPeso = GetTitulo(values, Moneda.Peso);
            var tituloMep = GetTitulo(values, Moneda.DolarMep);
            var fechaVencimiento = values.Key.FechaVencimiento;
            var ccl = CalculateCcl(tituloPeso, tituloCable);
            var cclMepBlue = CalculateCclMepBlue(blue, tituloPeso, tituloCable, tituloMep);
            var percCclMepBlue = CalculatePercentageCclMepBlue(blue, tituloPeso, tituloCable, tituloMep);
            var percCclMep = CalculatePercentageCclMep(tituloPeso, tituloCable, tituloMep);
            var ruloMepBlue = CalculatePercentageRuloMepBlue(blue, tituloMep);

            return new(isin,
                       denominacion,
                       tituloCable,
                       tituloPeso,
                       tituloMep,
                       fechaVencimiento,
                       ccl,
                       cclMepBlue,
                       percCclMepBlue,
                       percCclMep,
                       ruloMepBlue);
        }

        private Titulo? GetTitulo(IGrouping<TituloDetails, (Titulo Titulo, TituloDetails TituloDetails)> tuples, Moneda moneda)
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
            => tituloPeso?.PrecioCompra / tituloCable?.PrecioVenta;

        private double? CalculateCclMepBlue(Blue blue, Titulo? tituloPeso, Titulo? tituloCable, Titulo? tituloMep)
            => blue.PrecioCompra * CalculatePercentageCclMep(tituloPeso, tituloCable, tituloMep);

        private double? CalculatePercentageCclMepBlue(Blue blue, Titulo? tituloPeso, Titulo? tituloCable, Titulo? tituloMep)
            => PasarAPorcentaje(CalculateCclMepBlue(blue, tituloPeso, tituloCable, tituloMep) / CalculateCcl(tituloPeso, tituloCable));

        private double? CalculatePercentageCclMep(Titulo? tituloPeso, Titulo? tituloCable, Titulo? tituloMep)
            => PasarAPorcentaje(CalculateCcl(tituloPeso, tituloCable) / tituloMep?.PrecioCompra);

        private double? CalculatePercentageRuloMepBlue(Blue blue, Titulo? tituloMep)
            => PasarAPorcentaje(blue.PrecioCompra / tituloMep?.PrecioCompra);

        private double? PasarAPorcentaje(double? valor)
            => 100 * valor - 100;
    }
}
