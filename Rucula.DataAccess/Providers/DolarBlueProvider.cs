using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.Ambito;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

internal sealed class DolarBlueProvider(
    IAmbitoBlueFetcher ambitoBlueFetcher,
    IJsonDeserializer<BlueDto> blueJsonDeserializer,
    IMapper<BlueDto, Blue> blueMapper,
    INotifier notifier) : IDolarBlueProvider
{
    public async Task<Optional<Blue>> GetCurrentBlue()
    {
        await notifier.Notify("Consultando Dolar Blue...");
        var content = await ambitoBlueFetcher.Fetch().ConfigureAwait(false);
        return MapToBlue(ConvertContentToBlue(content));
    }

    private Optional<BlueDto> ConvertContentToBlue(string content)
        => blueJsonDeserializer
            .Deserialize(JsonNode.Parse(content)!);

    private Optional<Blue> MapToBlue(Optional<BlueDto> dto)
        => blueMapper.Map(dto);
}
