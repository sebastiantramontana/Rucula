﻿namespace Rucula.DataAccess.Providers.Byma
{
    internal class HttpReader : IHttpReader
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpReader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Read(HttpRequestMessage request)
        {
            var client = _httpClientFactory.CreateClient();
            var msg = await client.SendAsync(request).ConfigureAwait(false);
            return await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
