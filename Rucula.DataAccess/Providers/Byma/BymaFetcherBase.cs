using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma
{
    internal abstract class BymaFetcherBase : IFetcher
    {
        private readonly IFixedRequestFactory _fixedRequestFactory;
        private readonly IBymaHttpReader _bymaHttpReader;

        protected BymaFetcherBase(IFixedRequestFactory fixedRequestFactory, IBymaHttpReader bymaHttpReader)
        {
            _fixedRequestFactory = fixedRequestFactory;
            _bymaHttpReader = bymaHttpReader;
        }

        public async Task<string> Fetch()
        {
            using var request = _fixedRequestFactory.CreateRequest();

            return await _bymaHttpReader.Read(request);
        }
    }
}
