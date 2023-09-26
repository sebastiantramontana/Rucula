using System;

namespace Rucula.DataAccess.Providers.Ambito
{
    internal class AmbitoBlueFetcher : IAmbitoBlueFetcher
    {
        private const string Url = "https://mercados.ambito.com//dolar/informal/variacion";
        private readonly IAmbitoHttpReader _ambitoHttpReader;

        public AmbitoBlueFetcher(IAmbitoHttpReader ambitoHttpReader)
        {
            _ambitoHttpReader = ambitoHttpReader;
        }

        public async Task<string> Fetch()
        {
            using var request = CreateRequest();
            return await _ambitoHttpReader.Read(request).ConfigureAwait(false);
        }

        private HttpRequestMessage CreateRequest()
            => new HttpRequestMessage(HttpMethod.Get, Url);
    }
}
