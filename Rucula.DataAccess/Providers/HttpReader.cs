namespace Rucula.DataAccess.Providers;

internal sealed class HttpReader(IHttpClientFactory httpClientFactory) : IHttpReader
{
    public async Task<string> Read(string readerKey, HttpRequestMessage request)
    {
        var client = httpClientFactory.CreateClient(readerKey);
        var msg = await client.SendAsync(request);
        return await msg.Content.ReadAsStringAsync();
    }
}