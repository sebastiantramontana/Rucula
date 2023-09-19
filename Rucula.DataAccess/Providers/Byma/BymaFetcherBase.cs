using Rucula.DataAccess.Fetching.Byma.Config;
using Rucula.DataAccess.Providers;

namespace Rucula.DataAccess.Fetching.Byma
{
    internal abstract class BymaFetcherBase : IFetcher
    {
        private readonly IBymaHttpConfig _bymaHttpConfig;
        private readonly IBymaHttpReader _bymaHttpReader;

        protected BymaFetcherBase(IBymaHttpConfig bymaHttpConfig, IBymaHttpReader bymaHttpReader)
        {
            _bymaHttpConfig = bymaHttpConfig;
            _bymaHttpReader = bymaHttpReader;
        }

        public async Task<string> Fetch()
        {
            using var request = _bymaHttpConfig.CreateRequest();
            using var handler = _bymaHttpConfig.CreateHandler();

            return await _bymaHttpReader.Read(request, handler);
        }
    }
}
