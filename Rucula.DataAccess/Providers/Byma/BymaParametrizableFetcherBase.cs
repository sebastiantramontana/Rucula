using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma
{
    internal abstract class BymaParametrizableFetcherBase : IParametrizableFetcher
    {
        private readonly IParametrizableRequestFactory _parametrizableRequestFactory;
        private readonly IHttpReader _httpReader;

        protected BymaParametrizableFetcherBase(IParametrizableRequestFactory parametrizableRequestFactory, IHttpReader httpReader)
        {
            _parametrizableRequestFactory = parametrizableRequestFactory;
            _httpReader = httpReader;
        }

        public async Task<string> Fetch(string parameters)
        {
            using var request = _parametrizableRequestFactory.CreateRequest(parameters);
            return await _httpReader.Read(request);
        }
    }
}
