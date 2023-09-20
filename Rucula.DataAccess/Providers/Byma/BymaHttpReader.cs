namespace Rucula.DataAccess.Providers.Byma
{
    internal class BymaHttpReader : IBymaHttpReader
    {
        public async Task<string> Read(HttpRequestMessage request, HttpClientHandler handler)
        {
            using var client = new HttpClient(handler);
            using var msg = await client.SendAsync(request).ConfigureAwait(false);
            return await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
