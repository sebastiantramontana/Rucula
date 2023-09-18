using Rucula.DataAccess.Fetching.Byma.Config;

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

        public Task<string> Fetch() => _bymaHttpReader.Read(_bymaHttpConfig.Request, _bymaHttpConfig.Handler);
    }
}
