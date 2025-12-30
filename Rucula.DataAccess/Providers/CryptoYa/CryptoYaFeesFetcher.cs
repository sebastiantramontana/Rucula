namespace Rucula.DataAccess.Providers.CryptoYa;

internal sealed class CryptoYaFeesFetcher(IHttpReader httpReader) : ICryptoYaFeesFetcher
{
    private const string ReaderKey = "CryptoYaFees";
    private const string Url = "https://criptoya.com/api/fees";

    public async Task<string> Fetch()
    {
        using var request = CreateRequest();
        return await httpReader.Read(ReaderKey, request);
    }

    private static HttpRequestMessage CreateRequest()
        => new(HttpMethod.Get, Url);
}
