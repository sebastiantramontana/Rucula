
namespace Rucula.DataAccess.Providers.CryptoYa;

internal class CryptoYaPricesFetcher : ICryptoYaPricesFetcher
{
    private const string Url = "https://criptoya.com/api/{0}/ARS/{1}";
    private readonly IHttpReader _httpReader;

    public CryptoYaPricesFetcher(IHttpReader httpReader)
        => _httpReader = httpReader;

    public async Task<string> Fetch(CriptoYaPricesFetcherParameters parameters)
    {
        using var request = CreateRequest(parameters);
        return await _httpReader.Read(request).ConfigureAwait(false);
    }

    private static HttpRequestMessage CreateRequest(CriptoYaPricesFetcherParameters parameters)
        => new(HttpMethod.Get, string.Format(Url, parameters.CryptoCurrencyKey, parameters.Volume));
}
