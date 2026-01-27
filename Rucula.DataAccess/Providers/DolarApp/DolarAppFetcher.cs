namespace Rucula.DataAccess.Providers.DolarApp;

internal sealed class DolarAppFetcher(IHttpReader httpReader) : IDolarAppFetcher
{
    private const string ReaderKey = "DolarApp";
    private const string Url = "https://api.dolarapp.com/v1/tickers?currencies=ARS";

    public async Task<string> Fetch()
    {
        using var request = CreateRequest();
        return await httpReader.Read(ReaderKey, request);
    }

    private static HttpRequestMessage CreateRequest()
        => new(HttpMethod.Get, Url);
}

