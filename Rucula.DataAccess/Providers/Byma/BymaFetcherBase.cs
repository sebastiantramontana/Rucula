using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma
{
    internal abstract class BymaFetcherBase : IFetcher
    {
        private readonly IFixedRequestFactory _fixedRequestFactory;
        private readonly IHttpReader _httpReader;

        protected BymaFetcherBase(IFixedRequestFactory fixedRequestFactory, IHttpReader httpReader)
        {
            _fixedRequestFactory = fixedRequestFactory;
            _httpReader = httpReader;
        }

        public async Task<string> Fetch()
        {
            using var request = _fixedRequestFactory.CreateRequest();

            return await _httpReader.Read(request).ConfigureAwait(false);
        }
    }
}
