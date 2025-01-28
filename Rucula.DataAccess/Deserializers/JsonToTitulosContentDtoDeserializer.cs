using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal sealed class JsonToTitulosContentDtoDeserializer(IJsonDeserializer<PaginationDto> paginationDeserializer, IJsonDeserializer<TituloDto> tituloDeserializer) : IJsonDeserializer<TitulosContentDto>
{
    public Optional<TitulosContentDto> Deserialize(JsonNode? node)
    {
        if (node is null)
        {
            return Optional<TitulosContentDto>.Empty;
        }

        var pagination = paginationDeserializer.Deserialize(node!["content"]!);

        if (!pagination.HasValue)
        {
            return Optional<TitulosContentDto>.Empty;
        }

        var data = node!["data"]!
            .AsArray()
            .Select(n => tituloDeserializer.Deserialize(n!))
            .Where(t => t.HasValue)
            .Select(t => t.Value)
            .ToArray();

        return Optional<TitulosContentDto>.Sure(new TitulosContentDto(pagination.Value, data));
    }
}
