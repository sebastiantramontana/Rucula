
namespace Rucula.DataAccess.Providers.CryptoYa;

internal class CryptoYaFeesFetcher : ICryptoYaFeesFetcher
{
    private const string Url = "https://criptoya.com/api/fees";
    private readonly IHttpReader _httpReader;

    public CryptoYaFeesFetcher(IHttpReader httpReader)
        => _httpReader = httpReader;

    public async Task<string> Fetch()
    {
        using var request = CreateRequest();
        return await _httpReader.Read(request).ConfigureAwait(false);
    }

    private static HttpRequestMessage CreateRequest()
        => new(HttpMethod.Get, Url);
}
