using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers.CryptoYa;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Providers;

internal class DolarCryptoFeesProvider : IDolarCryptoFeesProvider
{
    private readonly ICryptoYaFeesFetcher _fetcher;
    private readonly IJsonDeserializer<IEnumerable<DolarCryptoFeesDto>> _dolarCrpyoFeesDtoJsonDeserializer;
    private readonly IMapper<DolarCryptoFeesDto, DolarCryptoFees> _dolarCrpyoFeesMapper;
    private readonly INotifier _notifier;

    public DolarCryptoFeesProvider(ICryptoYaFeesFetcher fetcher,
                                   IJsonDeserializer<IEnumerable<DolarCryptoFeesDto>> dolarCrpyoFeesDtoJsonDeserializer,
                                   IMapper<DolarCryptoFeesDto, DolarCryptoFees> dolarCrpyoFeesMapper,
                                   INotifier notifier)
    {
        _fetcher = fetcher;
        _dolarCrpyoFeesDtoJsonDeserializer = dolarCrpyoFeesDtoJsonDeserializer;
        _dolarCrpyoFeesMapper = dolarCrpyoFeesMapper;
        _notifier = notifier;
    }

    public async Task<IEnumerable<DolarCryptoFees>> Get()
    {
        await _notifier.NotifyProgress("Consultando Comisiones Dolar Crypto...").ConfigureAwait(false);
        var content = await _fetcher.Fetch().ConfigureAwait(false);
        return MapToDolarCryptoFees(ConvertContentToDolarCryptoFees(content));
    }

    private IEnumerable<DolarCryptoFeesDto> ConvertContentToDolarCryptoFees(string content)
    {
        var feesDtos = _dolarCrpyoFeesDtoJsonDeserializer.Deserialize(JsonNode.Parse(content)!);
        return feesDtos.HasValue ? feesDtos.Value : [];
    }
    private IEnumerable<DolarCryptoFees> MapToDolarCryptoFees(IEnumerable<DolarCryptoFeesDto> dtos)
        => dtos
        .Select(dto => _dolarCrpyoFeesMapper.Map(Optional<DolarCryptoFeesDto>.Maybe(dto)))
        .Where(dto => dto.HasValue)
        .Select(dto => dto.Value);
}
