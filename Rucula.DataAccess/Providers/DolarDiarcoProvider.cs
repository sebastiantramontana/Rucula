using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.Diarco;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

internal class DolarDiarcoProvider : IDolarDiarcoProvider
{
    private readonly IDiarcoFetcher _diarcoFetcher;
    private readonly IJsonDeserializer<DolarDiarcoDto> _dolarDiarcoJsonDeserializer;
    private readonly IMapper<DolarDiarcoDto, DolarDiarco> _dolarDiarcoMapper;
    private readonly INotifier _notifier;

    public DolarDiarcoProvider(IDiarcoFetcher diarcoFetcher,
                                IJsonDeserializer<DolarDiarcoDto> dolarDiarcoJsonDeserializer,
                                IMapper<DolarDiarcoDto, DolarDiarco> dolarDiarcoMapper,
                                INotifier notifier)
    {
        _diarcoFetcher = diarcoFetcher;
        _dolarDiarcoJsonDeserializer = dolarDiarcoJsonDeserializer;
        _dolarDiarcoMapper = dolarDiarcoMapper;
        _notifier = notifier;
    }

    public async Task<Optional<DolarDiarco>> GetCurrentDolarDiarco()
    {
        await _notifier.NotifyProgress("Consultando Dolar Diarco...").ConfigureAwait(false);
        var content = await _diarcoFetcher.Fetch().ConfigureAwait(false);
        return MapToDolarDiarco(ConvertContentToDolarDiarco(content));
    }

    private Optional<DolarDiarcoDto> ConvertContentToDolarDiarco(string content)
        => _dolarDiarcoJsonDeserializer
            .Deserialize(JsonNode.Parse(content)!);

    private Optional<DolarDiarco> MapToDolarDiarco(Optional<DolarDiarcoDto> dto)
        => _dolarDiarcoMapper.Map(dto);
}
