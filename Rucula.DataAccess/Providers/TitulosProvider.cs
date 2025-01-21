using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Providers.Byma;
using Rucula.DataAccess.Mappers;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

internal class TitulosProvider(IBymaLetrasFetcher letrasFetcher,
                       IBymaBonosFetcher bonosFetcher,
                       IBymaIsMarketOpenFetcher isMarketOpenFetcher,
                       IJsonDeserializer<TitulosContentDto> jsonTituloDeserializer,
                       IMapper<TituloDto, Titulo> tituloMapper) : ITitulosProvider
{
    public async Task<IEnumerable<Titulo>> Get()
    {
        if (!await IsMarketOpen().ConfigureAwait(false))
        {
            return [];
        }

        var letrasTask = await GetLetras().ConfigureAwait(false);
        var bonosTask = await GetBonos().ConfigureAwait(false);

        return letrasTask.Concat(bonosTask);
    }

    public Task<IEnumerable<Titulo>> GetBonos()
        => GetTitulos(bonosFetcher);

    public Task<IEnumerable<Titulo>> GetLetras()
        => GetTitulos(letrasFetcher);

    private async Task<IEnumerable<Titulo>> GetTitulos(IFetcher fetcher)
    {
        var content = await fetcher.Fetch().ConfigureAwait(false);
        return MapToTitulo(ConvertContentToTitulos(content));
    }

    private IEnumerable<TituloDto> ConvertContentToTitulos(string content)
    {
        var titulos = jsonTituloDeserializer.Deserialize(JsonNode.Parse(content));
        return titulos.HasValue ? titulos.Value.Titulos : [];
    }
    private IEnumerable<Titulo> MapToTitulo(IEnumerable<TituloDto> dtos)
        => dtos
            .Select(t => tituloMapper.Map(Optional<TituloDto>.Maybe(t)))
            .Where(t => t.HasValue)
            .Select(t => t.Value);

    private async Task<bool> IsMarketOpen()
    {
        bool isMarketOpen;

        try
        {
            var content = await isMarketOpenFetcher.Fetch().ConfigureAwait(false);
            isMarketOpen = bool.Parse(content);
        }
        catch
        {
            isMarketOpen = true;
        }

        return isMarketOpen;
    }
}
