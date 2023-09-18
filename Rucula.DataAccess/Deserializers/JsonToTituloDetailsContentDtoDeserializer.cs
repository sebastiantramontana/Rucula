using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers
{
    internal class JsonToTituloDetailsContentDtoDeserializer : IJsonDeserializer<TituloDetailsContentDto>
    {
        private readonly IJsonDeserializer<PaginationDto> _paginationDeserializer;
        private readonly IJsonDeserializer<TituloDetailsDto> _tituloDetailsDeserializer;

        public JsonToTituloDetailsContentDtoDeserializer(IJsonDeserializer<PaginationDto> paginationDeserializer, IJsonDeserializer<TituloDetailsDto> tituloDetailsDeserializer)
        {
            _paginationDeserializer = paginationDeserializer;
            _tituloDetailsDeserializer = tituloDetailsDeserializer;
        }

        public TituloDetailsContentDto Deserialize(JsonNode node)
        {
            var paginationDto = _paginationDeserializer.Deserialize(node["content"]!);
            var tituloDetailsDtos = node["data"]!
                .AsArray()
                .Select(n => _tituloDetailsDeserializer.Deserialize(n!))
                .ToArray();

            return new TituloDetailsContentDto(paginationDto, tituloDetailsDtos);
        }
    }
}
