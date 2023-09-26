using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.Ambito;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers
{
    internal class DolarBlueProvider : IDolarBlueProvider
    {
        private readonly IAmbitoBlueFetcher _ambitoBlueFetcher;
        private readonly IJsonDeserializer<BlueDto> _blueJsonDeserializer;
        private readonly IMapper<BlueDto, Blue> _blueMapper;

        public DolarBlueProvider(IAmbitoBlueFetcher ambitoBlueFetcher, IJsonDeserializer<BlueDto> blueJsonDeserializer, IMapper<BlueDto, Blue> blueMapper)
        {
            _ambitoBlueFetcher = ambitoBlueFetcher;
            _blueJsonDeserializer = blueJsonDeserializer;
            _blueMapper = blueMapper;
        }

        public async Task<Blue> GetCurrentBlue()
        {
            var content = await _ambitoBlueFetcher.Fetch().ConfigureAwait(false);
            return MapToBlue(ConvertContentToBlue(content));
        }

        private BlueDto ConvertContentToBlue(string content)
            => _blueJsonDeserializer
                .Deserialize(JsonNode.Parse(content)!);

        private Blue MapToBlue(BlueDto dto)
            => _blueMapper.Map(dto);
    }
}
