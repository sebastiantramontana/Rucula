using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Fetching.Byma;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Fetching
{
    internal class TituloIsinProvider : IProvider<TituloIsin>
    {
        private readonly IBymaLetrasFetcher _letrasFetcher;
        private readonly IBymaBonosFetcher _bonosFetcher;
        private readonly IBymaTituloDetailsFetcher _tituloDetailsFetcher;
        private readonly IJsonDeserializer<TitulosContentDto> _jsonTituloDeserializer;
        private readonly IJsonDeserializer<TituloDetailsContentDto> _jsonTituloDetailsDeserializer;
        private readonly IMapper<TituloDto, Titulo> _tituloMapper;

        public TituloIsinProvider(IBymaLetrasFetcher letrasFetcher,
                                  IBymaBonosFetcher bonosFetcher,
                                  IBymaTituloDetailsFetcher tituloDetailsFetcher,
                                  IJsonDeserializer<TitulosContentDto> jsonTituloDeserializer,
                                  IJsonDeserializer<TituloDetailsContentDto> jsonTituloDetailsDeserializer,
                                  IMapper<TituloDto, Titulo> tituloMapper)
        {
            _letrasFetcher = letrasFetcher;
            _bonosFetcher = bonosFetcher;
            _tituloDetailsFetcher = tituloDetailsFetcher;
            _jsonTituloDeserializer = jsonTituloDeserializer;
            _jsonTituloDetailsDeserializer = jsonTituloDetailsDeserializer;
            _tituloMapper = tituloMapper;
        }

        public async Task<IEnumerable<TituloIsin>> Get()
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
                .Where(g =>
                        g.Any(t => t.Titulo.Moneda == "EXT") &&
                        g.Any(t => t.Titulo.Moneda == "ARS"))
                .Select(g => new TituloIsin(
                    g.Key.CodigoIsin,
                    g.Key.Denominacion,
                    GetTitulo(g, "EXT")!,
                    GetTitulo(g, "ARS")!,
                    GetTitulo(g, "USD"),
                    DateOnly.FromDateTime(DateTime.Parse(g.Key.FechaVencimiento)),
                    new Blue(0.0, 0.0)));
        }

        private Titulo? GetTitulo(IGrouping<TituloDetailsDto, (TituloDto Titulo, TituloDetailsDto? TituloDetails)> tuples, string moneda)
        {
            var tituloDto = tuples
                .Select(t => t.Titulo)
                .SingleOrDefault(t => t.Moneda == moneda && t.Parking == "1");

            return tituloDto is not null
                ? _tituloMapper.Map(tituloDto)
                : null;
        }


        private async Task<IEnumerable<(TituloDto Titulo, TituloDetailsDto? Details)>> GetTitulosDetails(IEnumerable<TituloDto> titulos)
        {
            var tasks = titulos.Select(t => FetchTituloDetailsContent(t));
            var detailsContentArray = await Task.WhenAll(tasks).ConfigureAwait(false);

            return detailsContentArray
                .ToArray()
                .Select(d => (d.titulo, GetTituloDetailsDto(d.DetailsContent)));
        }

        private async Task<(TituloDto titulo, string DetailsContent)> FetchTituloDetailsContent(TituloDto titulo)
            => (titulo, await _tituloDetailsFetcher.Fetch(@$"{{ ""symbol"": ""{titulo.Simbolo}""}}").ConfigureAwait(false));

        private TituloDetailsDto? GetTituloDetailsDto(string jsonContent)
        {
            var detailsContent = ConvertToDetailsContent(jsonContent);
            return GetNationalTitulo(detailsContent.TitulosDetails);
        }

        private TituloDetailsContentDto ConvertToDetailsContent(string jsonContent)
            => _jsonTituloDetailsDeserializer.Deserialize(JsonNode.Parse(jsonContent)!);

        private TituloDetailsDto? GetNationalTitulo(IEnumerable<TituloDetailsDto> titulos)
            => titulos.FirstOrDefault(t => t.TipoObligacion == @"Valores Públicos Nacionales");

        private IEnumerable<TituloDto> CleanUpTitulos(IEnumerable<TituloDto> titulos)
            => titulos.Where(t => t.PrecioCompra > 0.0 && t.PrecioVenta > 0.0);

        private async Task<IEnumerable<TituloDto>> GetAllTitulos()
        {
            var letrasTask = GetTitulos(_letrasFetcher);
            var bonosTask = GetTitulos(_bonosFetcher);
            var titulosArray = await Task.WhenAll(letrasTask, bonosTask).ConfigureAwait(false);

            return titulosArray.SelectMany(t => t).ToArray();
        }

        private async Task<IEnumerable<TituloDto>> GetTitulos(IFetcher fetcher)
        {
            string content = await fetcher.Fetch().ConfigureAwait(false);
            return ConvertContentToTitulos(content);
        }

        private IEnumerable<TituloDto> ConvertContentToTitulos(string content)
            => _jsonTituloDeserializer
                .Deserialize(JsonNode.Parse(content)!)
                .Titulos
                .ToArray();
    }
}
