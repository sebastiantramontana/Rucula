namespace Rucula.DataAccess.Providers.Diarco;

internal class DiarcoFetcher : IDiarcoFetcher
{
    private const string Url = "https://www.diarco.com.ar/wp-json/oembed/1.0/embed?url=https%3A%2F%2Fwww.diarco.com.ar%2Fdolardiarco%2F";
    private readonly IHttpReader _httpReader;

    public DiarcoFetcher(IHttpReader httpReader) 
        => _httpReader = httpReader;

    public async Task<string> Fetch()
    {
        using var request = CreateRequest();
        return await _httpReader.Read(request).ConfigureAwait(false);
    }

    private HttpRequestMessage CreateRequest()
        => new(HttpMethod.Get, Url);
}

