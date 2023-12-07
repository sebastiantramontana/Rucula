using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.Byma;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers
{
    internal class TituloDetailsProvider : ITituloDetailsProvider
    {
        private readonly IBymaTituloDetailsFetcher _bymaTituloDetailsFetcher;
        private readonly IJsonDeserializer<TituloDetailsContentDto> _tituloDetailsContentJsonDeserializer;
        private readonly IMapper<TituloDetailsDto, TituloDetails> _tituloDetailsMapper;

        public TituloDetailsProvider(IBymaTituloDetailsFetcher bymaTituloDetailsFetcher, IJsonDeserializer<TituloDetailsContentDto> tituloDetailsContentJsonDeserializer, IMapper<TituloDetailsDto, TituloDetails> tituloDetailsMapper)
        {
            _bymaTituloDetailsFetcher = bymaTituloDetailsFetcher;
            _tituloDetailsContentJsonDeserializer = tituloDetailsContentJsonDeserializer;
            _tituloDetailsMapper = tituloDetailsMapper;
        }

        public async Task<IEnumerable<TituloDetails>> GetTituloDetails(string symbol)
        {
            var parameters = @$"{{ ""symbol"": ""{symbol}""}}";
            var content = await _bymaTituloDetailsFetcher.Fetch(parameters).ConfigureAwait(false);

            return MapToTituloDetails(ConvertContentToDto(content));
        }

        private IEnumerable<TituloDetailsDto> ConvertContentToDto(string content)
            => _tituloDetailsContentJsonDeserializer
                .Deserialize(JsonNode.Parse(content)!)
                .TitulosDetails;

        private IEnumerable<TituloDetails> MapToTituloDetails(IEnumerable<TituloDetailsDto> dtos)
            => dtos
                .Where(dto => !string.IsNullOrWhiteSpace(dto.FechaVencimiento))
                .Select(dto => _tituloDetailsMapper.Map(dto));
    }
}
