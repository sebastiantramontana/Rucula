using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToTituloDetailsContentDtoDeserializer(IJsonDeserializer<PaginationDto> paginationDeserializer, IJsonDeserializer<TituloDetailsDto> tituloDetailsDeserializer) : IJsonDeserializer<TituloDetailsContentDto>
{
    public Optional<TituloDetailsContentDto> Deserialize(JsonNode? node)
    {
        var paginationDto = paginationDeserializer.Deserialize(node?["content"]);

        if (!paginationDto.HasValue)
        {
            return Optional<TituloDetailsContentDto>.Empty;
        }

        var tituloDetailsDtos = node!["data"]!
            .AsArray()
            .Select(n => tituloDetailsDeserializer.Deserialize(n!))
            .Where(t => t.HasValue)
            .Select(t => t.Value)
            .ToArray();

        return Optional<TituloDetailsContentDto>.Sure(new TituloDetailsContentDto(paginationDto.Value, tituloDetailsDtos));
    }
}
