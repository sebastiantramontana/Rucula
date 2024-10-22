namespace Rucula.DataAccess.Dtos;

internal record class CryptoCurrencyFeesDto(string CryptoCurrencyKey, IEnumerable<BlockchainDto> Blockchains);
