using Rucula.DataAccess.Fetching.Byma.Config;

namespace Rucula.DataAccess.Fetching.Byma
{
    internal class BymaHttpReader : IBymaHttpReader
    {
        public async Task<string> Read(HttpRequestMessage request, HttpClientHandler handler)
        {
            using var client = new HttpClient(handler);
            using var msg = await client.SendAsync(request);
            return await msg.Content.ReadAsStringAsync();
        }
    }
}
