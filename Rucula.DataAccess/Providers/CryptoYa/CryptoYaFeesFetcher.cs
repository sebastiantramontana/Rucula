
namespace Rucula.DataAccess.Providers.CryptoYa;

internal class CryptoYaFeesFetcher(IHttpReader httpReader) : ICryptoYaFeesFetcher
{
    private const string Url = "https://criptoya.com/api/fees";

    public async Task<string> Fetch()
    {
        using var request = CreateRequest();
        return await httpReader.Read(request).ConfigureAwait(false);
    }

    private static HttpRequestMessage CreateRequest()
        => new(HttpMethod.Get, Url);
}
