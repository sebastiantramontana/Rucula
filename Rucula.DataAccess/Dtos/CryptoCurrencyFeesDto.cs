namespace Rucula.DataAccess.Dtos;

internal sealed record class CryptoCurrencyFeesDto(string CryptoCurrencyKey, IEnumerable<BlockchainDto> Blockchains);
