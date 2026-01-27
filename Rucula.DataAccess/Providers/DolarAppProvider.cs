using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.DolarApp;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

internal sealed class DolarAppProvider(IDolarAppFetcher dolarAppFetcher,
                            IJsonDeserializer<DolarAppDto> dolarAppJsonDeserializer,
                            IMapper<DolarAppDto, DolarAppInfo> dolarAppMapper,
                            INotifier notifier) : IDolarAppProvider
{
    public async Task<Optional<DolarAppInfo>> GetCurrentDolarApp()
    {
        await notifier.Notify("Consultando DolarApp...");
        var content = await dolarAppFetcher.Fetch();
        return MapToDolarApp(ConvertContentToDolarApp(content));
    }

    private Optional<DolarAppDto> ConvertContentToDolarApp(string content)
        => dolarAppJsonDeserializer
            .Deserialize(JsonNode.Parse(content)!);

    private Optional<DolarAppInfo> MapToDolarApp(Optional<DolarAppDto> dto)
        => dolarAppMapper.Map(dto);
}

