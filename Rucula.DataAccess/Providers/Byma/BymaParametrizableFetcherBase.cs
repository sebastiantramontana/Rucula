using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma
{
    internal abstract class BymaParametrizableFetcherBase : IParametrizableFetcher
    {
        private readonly IParametrizableRequestFactory _parametrizableRequestFactory;
        private readonly IBymaHttpReader _bymaHttpReader;

        protected BymaParametrizableFetcherBase(IParametrizableRequestFactory parametrizableRequestFactory, IBymaHttpReader bymaHttpReader)
        {
            _parametrizableRequestFactory = parametrizableRequestFactory;
            _bymaHttpReader = bymaHttpReader;
        }

        public async Task<string> Fetch(string parameters)
        {
            using var request = _parametrizableRequestFactory.CreateRequest(parameters);
            return await _bymaHttpReader.Read(request);
        }
    }
}
