using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Providers.Byma;
using Rucula.DataAccess.Mappers;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers
{
    internal class TitulosProvider : ITitulosProvider
    {
        private readonly IBymaLetrasFetcher _letrasFetcher;
        private readonly IBymaBonosFetcher _bonosFetcher;
        private readonly IBymaIsMarketOpenFetcher _isMarketOpenFetcher;
        private readonly IJsonDeserializer<TitulosContentDto> _jsonTituloDeserializer;
        private readonly IMapper<TituloDto, Titulo> _tituloMapper;

        public TitulosProvider(IBymaLetrasFetcher letrasFetcher,
                               IBymaBonosFetcher bonosFetcher,
                               IBymaIsMarketOpenFetcher isMarketOpenFetcher,
                               IJsonDeserializer<TitulosContentDto> jsonTituloDeserializer,
                               IMapper<TituloDto, Titulo> tituloMapper)
        {
            _letrasFetcher = letrasFetcher;
            _bonosFetcher = bonosFetcher;
            _isMarketOpenFetcher = isMarketOpenFetcher;
            _jsonTituloDeserializer = jsonTituloDeserializer;
            _tituloMapper = tituloMapper;
        }

        public async Task<IEnumerable<Titulo>> Get()
        {
            if (!await IsMarketOpen())
                return [];

            var letrasTask = await GetLetras();
            var bonosTask = await GetBonos();

            return letrasTask.Concat(bonosTask);
        }

        public Task<IEnumerable<Titulo>> GetBonos()
        {
            return GetTitulos(_bonosFetcher);
        }

        public Task<IEnumerable<Titulo>> GetLetras()
        {
            return GetTitulos(_letrasFetcher);
        }

        private async Task<IEnumerable<Titulo>> GetTitulos(IFetcher fetcher)
        {
            string content = await fetcher.Fetch().ConfigureAwait(false);
            return MapToTitulo(ConvertContentToTitulos(content));
        }

        private IEnumerable<TituloDto> ConvertContentToTitulos(string content)
            => _jsonTituloDeserializer
                .Deserialize(JsonNode.Parse(content)!)
                .Titulos
                .ToArray();

        private IEnumerable<Titulo> MapToTitulo(IEnumerable<TituloDto> dtos)
            => dtos.Select(d => _tituloMapper.Map(d));

        private async Task<bool> IsMarketOpen()
        {
            var content = await _isMarketOpenFetcher.Fetch();
            return bool.Parse(content);
        }
    }
}
