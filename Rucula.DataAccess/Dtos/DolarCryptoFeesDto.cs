namespace Rucula.DataAccess.Dtos;

internal record class DolarCryptoFeesDto(string ExchangeName, IEnumerable<CryptoCurrencyFeesDto> CryptoCurrencyFees);
