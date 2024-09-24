using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToTitulosContentDtoDeserializer : IJsonDeserializer<TitulosContentDto>
{
    private readonly IJsonDeserializer<PaginationDto> _paginationDeserializer;
    private readonly IJsonDeserializer<TituloDto> _tituloDeserializer;

    public JsonToTitulosContentDtoDeserializer(IJsonDeserializer<PaginationDto> paginationDeserializer, IJsonDeserializer<TituloDto> tituloDeserializer)
    {
        _paginationDeserializer = paginationDeserializer;
        _tituloDeserializer = tituloDeserializer;
    }

    public Optional<TitulosContentDto> Deserialize(JsonNode? node)
    {
        if (node is null)
            return Optional<TitulosContentDto>.Empty;

        var pagination = _paginationDeserializer.Deserialize(node!["content"]!);

        if (!pagination.HasValue)
            return Optional<TitulosContentDto>.Empty;

        var data = node!["data"]!
            .AsArray()
            .Select(n => _tituloDeserializer.Deserialize(n!))
            .Where(t => t.HasValue)
            .Select(t => t.Value)
            .ToArray();

        return Optional<TitulosContentDto>.Sure(new TitulosContentDto(pagination.Value, data));
    }
}
