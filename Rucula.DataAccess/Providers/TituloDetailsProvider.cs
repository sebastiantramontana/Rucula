using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.Byma;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

internal sealed class TituloDetailsProvider(IBymaTituloDetailsFetcher bymaTituloDetailsFetcher, IJsonDeserializer<TituloDetailsContentDto> tituloDetailsContentJsonDeserializer, IMapper<TituloDetailsDto, TituloDetails> tituloDetailsMapper) : ITituloDetailsProvider
{
    public async Task<IEnumerable<TituloDetails>> GetTituloDetails(string symbol)
    {
        var parameters = @$"{{ ""symbol"": ""{symbol}""}}";
        var content = await bymaTituloDetailsFetcher.Fetch(parameters).ConfigureAwait(false);

        return MapToTituloDetails(ConvertContentToDto(content));
    }

    private IEnumerable<TituloDetailsDto> ConvertContentToDto(string content)
    {
        var titulos = tituloDetailsContentJsonDeserializer.Deserialize(JsonNode.Parse(content));
        return titulos.HasValue ? titulos.Value.TitulosDetails : [];
    }
    private IEnumerable<TituloDetails> MapToTituloDetails(IEnumerable<TituloDetailsDto> dtos)
        => dtos
            .Where(dto => !string.IsNullOrWhiteSpace(dto.FechaVencimiento))
            .Select(dto => tituloDetailsMapper.Map(Optional<TituloDetailsDto>.Maybe(dto)))
            .Where(dto => dto.HasValue)
            .Select(dto => dto.Value);
}
