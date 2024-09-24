using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
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

        public Optional<TituloDetailsContentDto> Deserialize(JsonNode? node)
        {
            var paginationDto = _paginationDeserializer.Deserialize(node?["content"]);

            if (!paginationDto.HasValue)
                return Optional<TituloDetailsContentDto>.Empty;

            var tituloDetailsDtos = node!["data"]!
                .AsArray()
                .Select(n => _tituloDetailsDeserializer.Deserialize(n!))
                .Where(t => t.HasValue)
                .Select(t => t.Value)
                .ToArray();

            return Optional<TituloDetailsContentDto>.Sure(new TituloDetailsContentDto(paginationDto.Value, tituloDetailsDtos));
        }
    }
}
