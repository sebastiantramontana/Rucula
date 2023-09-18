using Rucula.DataAccess.Dtos;
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

        public PaginationDto Deserialize(JsonNode node)
        {
            var pageNumber = _valueReader.GetValue<int>(node, "page_number");
            var pageCount = _valueReader.GetValue<int>(node, "page_count");
            var pageSize = _valueReader.GetValue<int>(node, "page_size");
            var totalElementsCount = _valueReader.GetValue<int>(node, "total_elements_count");

            return new PaginationDto(pageNumber, pageCount, pageSize, totalElementsCount);
        }
    }
}
