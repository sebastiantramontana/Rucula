using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers;

internal sealed class DolarCrpyoFeesMapper : IMapper<DolarCryptoFeesDto, DolarCryptoFees>
{
    public Optional<DolarCryptoFees> Map(Optional<DolarCryptoFeesDto> from)
        => from.HasValue
            ? Optional<DolarCryptoFees>.Sure(new(from.Value.ExchangeName, Map(from.Value.CryptoCurrencyFees)))
            : Optional<DolarCryptoFees>.Empty;

    private IEnumerable<CryptoCurrencyFees> Map(IEnumerable<CryptoCurrencyFeesDto> dtos)
        => dtos.Select(Map);

    private CryptoCurrencyFees Map(CryptoCurrencyFeesDto dto)
        => new(dto.CryptoCurrencyKey, Map(dto.Blockchains));

    private IEnumerable<CurrencyBlockchainFee> Map(IEnumerable<BlockchainDto> dtos)
        => dtos.Select(Map);

    private CurrencyBlockchainFee Map(BlockchainDto dto)
        => new(new Blockchain(dto.Name), dto.Fees);
}
