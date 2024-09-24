using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Providers.Byma;
using Rucula.DataAccess.Mappers;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

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
        if (!await IsMarketOpen().ConfigureAwait(false))
            return [];

        var letrasTask = await GetLetras().ConfigureAwait(false);
        var bonosTask = await GetBonos().ConfigureAwait(false);

        return letrasTask.Concat(bonosTask);
    }

    public Task<IEnumerable<Titulo>> GetBonos()
        => GetTitulos(_bonosFetcher);

    public Task<IEnumerable<Titulo>> GetLetras()
        => GetTitulos(_letrasFetcher);

    private async Task<IEnumerable<Titulo>> GetTitulos(IFetcher fetcher)
    {
        var content = await fetcher.Fetch().ConfigureAwait(false);
        return MapToTitulo(ConvertContentToTitulos(content));
    }

    private IEnumerable<TituloDto> ConvertContentToTitulos(string content)
    {
        var titulos = _jsonTituloDeserializer.Deserialize(JsonNode.Parse(content));
        return titulos.HasValue ? titulos.Value.Titulos : [];
    }
    private IEnumerable<Titulo> MapToTitulo(IEnumerable<TituloDto> dtos)
        => dtos
            .Select(t => _tituloMapper.Map(Optional<TituloDto>.Maybe(t)))
            .Where(t => t.HasValue)
            .Select(t => t.Value);

    private async Task<bool> IsMarketOpen()
    {
        bool isMarketOpen;

        try
        {
            var content = await _isMarketOpenFetcher.Fetch().ConfigureAwait(false);
            isMarketOpen = bool.Parse(content);
        }
        catch (HttpRequestException)
        {
            isMarketOpen = true;
        }

        return isMarketOpen;
    }
}
