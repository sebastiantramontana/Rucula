using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Providers.Byma;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers
{
    internal class TituloIsinProvider : IProvider<TituloIsin>
    {
        private readonly IBymaTituloDetailsFetcher _tituloDetailsFetcher;
        private readonly IJsonDeserializer<TituloDetailsContentDto> _jsonTituloDetailsDeserializer;
        private readonly IProvider<Titulo> _titulosProvider;

        public TituloIsinProvider(IBymaTituloDetailsFetcher tituloDetailsFetcher,
                                  IJsonDeserializer<TituloDetailsContentDto> jsonTituloDetailsDeserializer,
                                  IProvider<Titulo> titulosProvider)
        {
            _tituloDetailsFetcher = tituloDetailsFetcher;
            _jsonTituloDetailsDeserializer = jsonTituloDetailsDeserializer;
            _titulosProvider = titulosProvider;
        }

        public async Task<IEnumerable<TituloIsin>> Get()
        {
            var titulos = await _titulosProvider.Get();
            var details = await GetTitulosDetails(titulos);

            return CreateTitulosIsin(details);
        }

        private IEnumerable<TituloIsin> CreateTitulosIsin(IEnumerable<(Titulo Titulo, TituloDetailsDto? TituloDetails)> details)
        {
            return details
                .Where(d => d.TituloDetails is not null && (d.Titulo.PrecioCompra > 0.0 || d.Titulo.PrecioVenta > 0.0))
                .GroupBy(d => d.TituloDetails!)
                .Select(g => new TituloIsin(
                    g.Key.CodigoIsin,
                    g.Key.Denominacion,
                    GetTitulo(g, Moneda.DolarCable),
                    GetTitulo(g, Moneda.Peso),
                    GetTitulo(g, Moneda.DolarMep),
                    DateOnly.FromDateTime(DateTime.Parse(g.Key.FechaVencimiento)),
                    new Blue(0.0, 0.0)));
        }

        private Titulo? GetTitulo(IGrouping<TituloDetailsDto, (Titulo Titulo, TituloDetailsDto? TituloDetails)> tuples, Moneda moneda)
        {
            return tuples
                .Select(t => t.Titulo)
                .SingleOrDefault(t => t.Moneda == moneda && t.Parking == Parking.CI);
        }

        private async Task<IEnumerable<(Titulo Titulo, TituloDetailsDto? Details)>> GetTitulosDetails(IEnumerable<Titulo> titulos)
        {
            var detailsContentList = new List<(Titulo Titulo, string DetailsContent)>();

            foreach (var t in titulos)
                detailsContentList.Add(await FetchTituloDetailsContent(t));

            return detailsContentList
                    .Select(d => (d.Titulo, GetTituloDetailsDto(d.DetailsContent)));

            //var tasks = titulos.Select(async t => await FetchTituloDetailsContent(t)).ToArray();
            //var detailsContentArray = await Task.WhenAll(tasks).ConfigureAwait(false);

            //return detailsContentArray
            //    .ToArray()
            //    .Select(d => (d.titulo, GetTituloDetailsDto(d.DetailsContent)));
        }

        private async Task<(Titulo Titulo, string DetailsContent)> FetchTituloDetailsContent(Titulo titulo)
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
    }
}
