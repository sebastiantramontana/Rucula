﻿namespace Rucula.DataAccess.Providers.Ambito
{
    internal class AmbitoBlueFetcher : IAmbitoBlueFetcher
    {
        private const string Url = "https://mercados.ambito.com//dolar/informal/variacion";
        private readonly IHttpReader _httpReader;

        public AmbitoBlueFetcher(IHttpReader httpReader)
        {
            _httpReader = httpReader;
        }

        public async Task<string> Fetch()
        {
            using var request = CreateRequest();
            return await _httpReader.Read(request).ConfigureAwait(false);
        }

        private HttpRequestMessage CreateRequest()
            => new HttpRequestMessage(HttpMethod.Get, Url);
    }
}
