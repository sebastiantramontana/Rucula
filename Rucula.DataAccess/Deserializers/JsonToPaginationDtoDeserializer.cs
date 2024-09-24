using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers
{
    internal class JsonToPaginationDtoDeserializer : IJsonDeserializer<PaginationDto>
    {
        private readonly IJsonValueReader _valueReader;

        public JsonToPaginationDtoDeserializer(IJsonValueReader valueReader)
        {
            _valueReader = valueReader;
        }

        public Optional<PaginationDto> Deserialize(JsonNode? node)
        {
            if (node is null)
                return Optional<PaginationDto>.Empty;

            var pageNumber = _valueReader.GetValue<int>(node!, "page_number");
            var pageCount = _valueReader.GetValue<int>(node!, "page_count");
            var pageSize = _valueReader.GetValue<int>(node!, "page_size");
            var totalElementsCount = _valueReader.GetValue<int>(node!, "total_elements_count");


            return Optional<PaginationDto>.Sure(new PaginationDto(pageNumber.Value, pageCount.Value, pageSize.Value, totalElementsCount.Value));
        }
    }
}
