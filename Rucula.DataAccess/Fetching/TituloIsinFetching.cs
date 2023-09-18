using Rucula.DataAccess.Converters;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Fetching.Byma;
using Rucula.DataAccess.Mappers;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Fetching
{
    internal class TituloIsinFetching : IFetching<TituloIsin>
    {
        private readonly IBymaLetrasFetcher _letrasFetcher;
        private readonly IBymaBonosFetcher _bonosFetcher;
        private readonly IBymaTituloDetailsFetcher _tituloDetailsFetcher;
        private readonly IJsonConverter<TitulosContentDto> _jsonTituloConverter;
        private readonly IJsonConverter<TituloDetailsContentDto> _jsonTituloDetailsConverter;
        private readonly IMapper<TituloDto, Titulo> _tituloMapper;

        public TituloIsinFetching(IBymaLetrasFetcher letrasFetcher,
                                  IBymaBonosFetcher bonosFetcher,
                                  IBymaTituloDetailsFetcher tituloDetailsFetcher,
                                  IJsonConverter<TitulosContentDto> jsonTituloConverter,
                                  IJsonConverter<TituloDetailsContentDto> jsonTituloDetailsConverter,
                                  IMapper<TituloDto, Titulo> tituloMapper)
        {
            _letrasFetcher = letrasFetcher;
            _bonosFetcher = bonosFetcher;
            _tituloDetailsFetcher = tituloDetailsFetcher;
            _jsonTituloConverter = jsonTituloConverter;
            _jsonTituloDetailsConverter = jsonTituloDetailsConverter;
            _tituloMapper = tituloMapper;
        }

        public async Task<IEnumerable<TituloIsin>> Fetch()
        {
            var titulos = await GetAllTitulos();
            var titulosValidos = CleanUpTitulos(titulos);
            var details = await GetTitulosDetails(titulosValidos);

            return CreateTitulosIsin(details);
        }

        private IEnumerable<TituloIsin> CreateTitulosIsin(IEnumerable<(TituloDto Titulo, TituloDetailsDto? TituloDetails)> details)
        {
            return details
                .Where(d => d.TituloDetails is not null)
                .GroupBy(d => d.TituloDetails!)
                .Select(g => new TituloIsin(
                    g.Key.CodigoIsin,
                    g.Key.Denominacion,
                    GetTitulo(g, "EXT"),
                    GetTitulo(g, "ARS"),
                    GetTitulo(g, "USD"),
                    DateOnly.Parse(g.Key.FechaVencimiento),
                    new Blue(0.0, 0.0)));
        }

        private Titulo GetTitulo(IGrouping<TituloDetailsDto, (TituloDto Titulo, TituloDetailsDto? TituloDetails)> tuples, string moneda)
            => _tituloMapper.Map(tuples.Single(t => t.Titulo.Moneda == moneda).Titulo);

        private async Task<IEnumerable<(TituloDto Titulo, TituloDetailsDto? Details)>> GetTitulosDetails(IEnumerable<TituloDto> titulos)
        {
            var tasks = titulos.Select(t => FetchTituloDetailsContent(t));
            var detailsContentArray = await Task.WhenAll(tasks);

            return detailsContentArray
                .ToArray()
                .Select(d => (d.titulo, GetTituloDetailsDto(d.DetailsContent)));
        }

        private async Task<(TituloDto titulo, string DetailsContent)> FetchTituloDetailsContent(TituloDto titulo)
            => (titulo, await _tituloDetailsFetcher.Fetch(@$"{{ ""symbol"": ""{titulo.Simbolo}""}}"));

        private TituloDetailsDto? GetTituloDetailsDto(string jsonContent)
        {
            var detailsContent = ConvertToDetailsContent(jsonContent);
            return GetNationalTitulo(detailsContent.TitulosDetails);
        }

        private TituloDetailsContentDto ConvertToDetailsContent(string jsonContent)
            => _jsonTituloDetailsConverter.Convert(JsonNode.Parse(jsonContent)!);

        private TituloDetailsDto? GetNationalTitulo(IEnumerable<TituloDetailsDto> titulos)
            => titulos.FirstOrDefault(t => t.TipoObligacion == @"Valores Públicos Nacionales");

        private IEnumerable<TituloDto> CleanUpTitulos(IEnumerable<TituloDto> titulos)
            => titulos.Where(t => t.PrecioCompra > 0.0 && t.PrecioVenta > 0.0);

        private async Task<IEnumerable<TituloDto>> GetAllTitulos()
        {
            var letrasTask = GetTitulos(_letrasFetcher);
            var bonosTask = GetTitulos(_bonosFetcher);
            var titulosArray = await Task.WhenAll(letrasTask, bonosTask);

            return titulosArray.SelectMany(t => t).ToArray();
        }

        private async Task<IEnumerable<TituloDto>> GetTitulos(IFetcher fetcher)
        {
            string content = await fetcher.Fetch().ConfigureAwait(false);
            return ConvertContentToTitulos(content);
        }

        private IEnumerable<TituloDto> ConvertContentToTitulos(string content)
            => _jsonTituloConverter
                .Convert(JsonNode.Parse(content)!)
                .Titulos
                .ToArray();

    }
}
