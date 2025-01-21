using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.WesternUnion;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

internal class WesternUnionProvider(IWesternUnionFetcher westernUnionFetcher,
                            IJsonDeserializer<DolarWesternUnionDto> dolarWesternUnionJsonDeserializer,
                            IMapper<DolarWesternUnionDto, DolarWesternUnionInfo> dolarWesterUnionMapper,
                            INotifier notifier) : IWesternUnionProvider
{
    public async Task<Optional<DolarWesternUnionInfo>> GetCurrentDolarWesternUnion(WesternUnionParameters westernUnionComissions)
    {
        await notifier.Notify("Consultando Dolar Western Union...");
        var content = await westernUnionFetcher.Fetch(westernUnionComissions.AmountToSend).ConfigureAwait(false);
        return MapToDolarWesterUnion(ConvertContentToDolarWesterUnion(content));
    }

    private Optional<DolarWesternUnionDto> ConvertContentToDolarWesterUnion(string content)
        => dolarWesternUnionJsonDeserializer
            .Deserialize(JsonNode.Parse(content)!);

    private Optional<DolarWesternUnionInfo> MapToDolarWesterUnion(Optional<DolarWesternUnionDto> dto)
        => dolarWesterUnionMapper.Map(dto);
}
