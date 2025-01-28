using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal sealed class JsonToPaginationDtoDeserializer(IJsonValueReader valueReader) : IJsonDeserializer<PaginationDto>
{
    public Optional<PaginationDto> Deserialize(JsonNode? node)
    {
        if (node is null)
        {
            return Optional<PaginationDto>.Empty;
        }

        var pageNumber = valueReader.GetValue<int>(node!, "page_number");
        var pageCount = valueReader.GetValue<int>(node!, "page_count");
        var pageSize = valueReader.GetValue<int>(node!, "page_size");
        var totalElementsCount = valueReader.GetValue<int>(node!, "total_elements_count");

        return Optional<PaginationDto>.Sure(new PaginationDto(pageNumber.Value, pageCount.Value, pageSize.Value, totalElementsCount.Value));
    }
}
