using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.CryptoYa;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

internal sealed class DolarCryptoFeesProvider(ICryptoYaFeesFetcher fetcher,
                               IJsonDeserializer<IEnumerable<DolarCryptoFeesDto>> dolarCrpyoFeesDtoJsonDeserializer,
                               IMapper<DolarCryptoFeesDto, DolarCryptoFees> dolarCrpyoFeesMapper,
                               INotifier notifier) : IDolarCryptoFeesProvider
{
    public async Task<IEnumerable<DolarCryptoFees>> Get()
    {
        await notifier.Notify("Consultando Comisiones Dolar Crypto...");
        var content = await fetcher.Fetch().ConfigureAwait(false);
        return MapToDolarCryptoFees(ConvertContentToDolarCryptoFees(content));
    }

    private IEnumerable<DolarCryptoFeesDto> ConvertContentToDolarCryptoFees(string content)
    {
        var feesDtos = dolarCrpyoFeesDtoJsonDeserializer.Deserialize(JsonNode.Parse(content)!);
        return feesDtos.HasValue ? feesDtos.Value : [];
    }

    private IEnumerable<DolarCryptoFees> MapToDolarCryptoFees(IEnumerable<DolarCryptoFeesDto> dtos)
        => dtos
        .Select(dto => dolarCrpyoFeesMapper.Map(Optional<DolarCryptoFeesDto>.Maybe(dto)))
        .Where(dto => dto.HasValue)
        .Select(dto => dto.Value);
}
