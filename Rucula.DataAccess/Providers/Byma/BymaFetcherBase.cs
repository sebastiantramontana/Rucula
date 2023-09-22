using Rucula.DataAccess.Providers.Byma.Config;

namespace Rucula.DataAccess.Providers.Byma
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

            return await _bymaHttpReader.Read(request);
        }
    }
}
