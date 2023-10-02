using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.Ambito;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers
{
    internal class DolarCryptoProvider : IDolarCryptoProvider
    {
        private readonly IAmbitoDolarCryptoFetcher _ambitoDolarCryptoFetcher;
        private readonly IJsonDeserializer<DolarCryptoDto> _dolarCryptoJsonDeserializer;
        private readonly IMapper<DolarCryptoDto, DolarCrypto> _dolarCryptoMapper;

        public DolarCryptoProvider(IAmbitoDolarCryptoFetcher ambitoDolarCryptoFetcher,
                                   IJsonDeserializer<DolarCryptoDto> dolarCryptoJsonDeserializer,
                                   IMapper<DolarCryptoDto, DolarCrypto> dolarCryptoMapper)
        {
            _ambitoDolarCryptoFetcher = ambitoDolarCryptoFetcher;
            _dolarCryptoJsonDeserializer = dolarCryptoJsonDeserializer;
            _dolarCryptoMapper = dolarCryptoMapper;
        }

        public async Task<DolarCrypto> GetCurrentDolarCrypto()
        {
            var content = await _ambitoDolarCryptoFetcher.Fetch().ConfigureAwait(false);
            return MapToCrypto(ConvertContentToCrypto(content));
        }

        private DolarCryptoDto ConvertContentToCrypto(string content)
            => _dolarCryptoJsonDeserializer
                .Deserialize(JsonNode.Parse(content)!);

        private DolarCrypto MapToCrypto(DolarCryptoDto dto)
            => _dolarCryptoMapper.Map(dto);
    }
}
