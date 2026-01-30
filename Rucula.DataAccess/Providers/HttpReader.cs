namespace Rucula.DataAccess.Providers;

internal sealed class HttpReader(IHttpClientFactory httpClientFactory) : IHttpReader
{
    public async Task<string> Read(string readerKey, HttpRequestMessage request)
    {
        using var client = httpClientFactory.CreateClient(readerKey);
        using var msg = await client.SendAsync(request);
        return await msg.Content.ReadAsStringAsync();
    }
}