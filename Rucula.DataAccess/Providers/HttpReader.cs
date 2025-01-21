namespace Rucula.DataAccess.Providers;

internal class HttpReader(IHttpClientFactory httpClientFactory) : IHttpReader
{
    public async Task<string> Read(HttpRequestMessage request)
    {
        var client = httpClientFactory.CreateClient();
        var msg = await client.SendAsync(request).ConfigureAwait(false);
        return await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
    }
}
