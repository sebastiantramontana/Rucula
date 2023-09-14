using Rucula.DataAccess.Converters;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Fetching.Byma;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Fetching
{
    internal class TituloIsinFetching : IFetching<TituloIsin>
    {
        private readonly ILetrasFetcher _letrasFetcher;
        private readonly IBonosFetcher _bonosFetcher;
        private readonly IJsonConverter<TitulosContentDto> _jsonConverter;

        public TituloIsinFetching(ILetrasFetcher letrasFetcher, IBonosFetcher bonosFetcher, IJsonConverter<TitulosContentDto> jsonConverter)
        {
            _letrasFetcher = letrasFetcher;
            _bonosFetcher = bonosFetcher;
            _jsonConverter = jsonConverter;
        }

        public async Task<IEnumerable<TituloIsin>> Fetch()
        {
            var letras = await GetTitulos(_letrasFetcher);
            var bonos = await GetTitulos(_bonosFetcher);
            var titulosValidos = CleanUpTitulos(letras, bonos);

            //Traer los ISIN de cada titulo y agruparlos
        }

        private IEnumerable<TituloDto> CleanUpTitulos(IEnumerable<TituloDto> letras, IEnumerable<TituloDto> bonos)
            => letras
                .Concat(bonos)
                .Where(t => t.PrecioCompra > 0.0 && t.PrecioVenta > 0.0);

        private async Task<IEnumerable<TituloDto>> GetTitulos(IFetcher fetcher)
        {
            string content = await fetcher.Fetch().ConfigureAwait(false);
            return ConvertContentToTitulos(content);
        }

        private IEnumerable<TituloDto> ConvertContentToTitulos(string content)
            => _jsonConverter
                .Convert(JsonNode.Parse(content)!)
                .Titulos
                .ToArray();

    }
}
