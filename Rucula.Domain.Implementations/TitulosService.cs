using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations
{
    internal class TitulosService : ITitulosService
    {
        private readonly IProvider<Titulo> _titulosProvider;
        private readonly IProvider<TituloIsin> _tituloIsinProvider;

        public TitulosService(IProvider<Titulo> titulosProvider, IProvider<TituloIsin> tituloIsinProvider)
        {
            _titulosProvider = titulosProvider;
            _tituloIsinProvider = tituloIsinProvider;
        }

        public Task<IEnumerable<Titulo>> GetAllTitulos()
        {
            return _titulosProvider.Get();
        }

        public Task<IEnumerable<TituloIsin>> GetAllTitulosIsin()
        {
            return _tituloIsinProvider.Get();
        }

        public async Task<IEnumerable<TituloIsin>> GetCclRankingTitulosIsin()
        {
            var allTitulosIsin = await _tituloIsinProvider.Get();

            return allTitulosIsin
                    .Where(t =>
                        IsTituloUseful(t.TituloCable) &&
                        IsTituloUseful(t.TituloPeso) &&
                        t.Vencimiento > DateOnly.FromDateTime(DateTime.Today))
                    .OrderByDescending(t => t.CotizacionCcl);
        }

        private bool IsTituloUseful(Titulo? titulo)
            => titulo is not null && (titulo.PrecioCompra > 0.0 || titulo.PrecioVenta > 0.0);
    }
}