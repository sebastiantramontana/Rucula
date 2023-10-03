using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.WesternUnion;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers
{
    internal class WesternUnionProvider : IWesternUnionProvider
    {
        private readonly IWesternUnionFetcher _westernUnionFetcher;
        private readonly IJsonDeserializer<DolarWesternUnionDto> _dolarWesternUnionJsonDeserializer;
        private readonly IMapper<DolarWesternUnionDto, DolarWesternUnion> _dolarWesterUnionMapper;

        public WesternUnionProvider(IWesternUnionFetcher westernUnionFetcher,
                                    IJsonDeserializer<DolarWesternUnionDto> dolarWesternUnionJsonDeserializer,
                                    IMapper<DolarWesternUnionDto, DolarWesternUnion> dolarWesterUnionMapper)
        {
            _westernUnionFetcher = westernUnionFetcher;
            _dolarWesternUnionJsonDeserializer = dolarWesternUnionJsonDeserializer;
            _dolarWesterUnionMapper = dolarWesterUnionMapper;
        }

        public async Task<DolarWesternUnion> GetCurrentDolarWesternUnion()
        {
            var content = await _westernUnionFetcher.Fetch().ConfigureAwait(false);
            return MapToDolarWesterUnion(ConvertContentToDolarWesterUnion(content));
        }

        private DolarWesternUnionDto ConvertContentToDolarWesterUnion(string content)
            => _dolarWesternUnionJsonDeserializer
                .Deserialize(JsonNode.Parse(content)!);

        private DolarWesternUnion MapToDolarWesterUnion(DolarWesternUnionDto dto)
            => _dolarWesterUnionMapper.Map(dto);
    }
}
