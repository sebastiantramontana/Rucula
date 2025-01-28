namespace Rucula.DataAccess.Dtos;

internal sealed record class DolarCryptoFeesDto(string ExchangeName, IEnumerable<CryptoCurrencyFeesDto> CryptoCurrencyFees);