using Rucula.DataAccess.Fetching.Byma.Config;

namespace Rucula.DataAccess.Fetching.Byma
{
    internal class BymaHttpReader : IBymaHttpReader
    {
        public async Task<string> Read(IBymaHttpConfig config)
        {
            using var client = new HttpClient(config.Handler);
            using var msg = await client.SendAsync(config.Request);
            return await msg.Content.ReadAsStringAsync();
        }
    }
}
