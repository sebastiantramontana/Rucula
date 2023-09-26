namespace Rucula.DataAccess.Providers.Ambito
{
    internal class AmbitoHttpReader : IAmbitoHttpReader
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AmbitoHttpReader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Read(HttpRequestMessage request)
        {
            using var client = _httpClientFactory.CreateClient();
            using var msg = await client.SendAsync(request).ConfigureAwait(false);
            return await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
