namespace Rucula.DataAccess.Providers.Ambito;

internal sealed class AmbitoBlueFetcher(IHttpReader httpReader) : IAmbitoBlueFetcher
{
    private const string ReaderKey = "Ambito";
    private const string Url = "https://mercados.ambito.com//dolar/informal/variacion";

    public async Task<string> Fetch()
    {
        using var request = CreateRequest();
        return await httpReader.Read(ReaderKey, request);
    }

    private static HttpRequestMessage CreateRequest()
        => new(HttpMethod.Get, Url);
}
