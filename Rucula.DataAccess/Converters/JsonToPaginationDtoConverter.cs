using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Converters
{
    internal class JsonToPaginationDtoConverter : IJsonConverter<paginationDto>
    {
        private readonly IJsonValueReader _valueReader;

        public JsonToPaginationDtoConverter(IJsonValueReader valueReader)
        {
            _valueReader = valueReader;
        }

        public paginationDto Convert(JsonNode node)
        {
            var pageNumber = _valueReader.GetValue<int>(node, "page_number");
            var pageCount = _valueReader.GetValue<int>(node, "page_count");
            var pageSize = _valueReader.GetValue<int>(node, "page_size");
            var totalElementsCount = _valueReader.GetValue<int>(node, "total_elements_count");

            return new paginationDto(pageNumber, pageCount, pageSize, totalElementsCount);
        }
    }
}
