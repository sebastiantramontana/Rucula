using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers
{
    internal class JsonToTitulosContentDtoDeserializer : IJsonDeserializer<TitulosContentDto>
    {
        private readonly IJsonDeserializer<PaginationDto> _paginationDeserializer;
        private readonly IJsonDeserializer<TituloDto> _tituloDeserializer;

        public JsonToTitulosContentDtoDeserializer(IJsonDeserializer<PaginationDto> paginationDeserializer, IJsonDeserializer<TituloDto> tituloDeserializer)
        {
            _paginationDeserializer = paginationDeserializer;
            _tituloDeserializer = tituloDeserializer;
        }

        public TitulosContentDto Deserialize(JsonNode node)
        {
            var pagination = _paginationDeserializer.Deserialize(node["content"]!);
            var data = node["data"]!
                .AsArray()
                .Select(n =>
                    _tituloDeserializer.Deserialize(n!))
                .ToArray();

            return new TitulosContentDto(pagination, data);
        }
    }
}
