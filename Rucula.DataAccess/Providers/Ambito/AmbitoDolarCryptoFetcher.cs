namespace Rucula.DataAccess.Providers.Ambito
{
    internal class AmbitoDolarCryptoFetcher : IAmbitoDolarCryptoFetcher
    {
        private const string Url = "https://mercados.ambito.com//dolarcripto/variacion";
        private readonly IHttpReader _httpReader;

        public AmbitoDolarCryptoFetcher(IHttpReader httpReader)
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
