using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Converters
{
    internal class JsonToTitulosContentDtoConverter : IJsonConverter<TitulosContentDto>
    {
        private readonly IJsonConverter<paginationDto> _paginationConverter;
        private readonly IJsonConverter<TituloDto> _tituloConverter;

        public JsonToTitulosContentDtoConverter(IJsonConverter<paginationDto> paginationConverter, IJsonConverter<TituloDto> tituloConverter)
        {
            _paginationConverter = paginationConverter;
            _tituloConverter = tituloConverter;
        }

        public TitulosContentDto Convert(JsonNode node)
        {
            var pagination = _paginationConverter.Convert(node["content"]!);
            var data = node["data"]!
                .AsArray()
                .Select(n =>
                    _tituloConverter.Convert(n!))
                .ToArray();

            return new TitulosContentDto(pagination, data);
        }
    }
}
