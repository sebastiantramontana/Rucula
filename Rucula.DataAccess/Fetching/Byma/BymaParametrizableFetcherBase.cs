using Rucula.DataAccess.Fetching.Byma.Config;

namespace Rucula.DataAccess.Fetching.Byma
{
    internal abstract class BymaParametrizableFetcherBase : IParametrizableFetcher
    {
        private readonly IParametrizableBymaHttpConfig _parametrizableBymaHttpConfig;
        private readonly IBymaHttpReader _bymaHttpReader;

        protected BymaParametrizableFetcherBase(IParametrizableBymaHttpConfig parametrizableBymaHttpConfig, IBymaHttpReader bymaHttpReader)
        {
            _parametrizableBymaHttpConfig = parametrizableBymaHttpConfig;
            _bymaHttpReader = bymaHttpReader;
        }

        public Task<string> Fetch(string parameters)
            => _bymaHttpReader.Read(_parametrizableBymaHttpConfig.CreateRequest(parameters), _parametrizableBymaHttpConfig.Handler);
    }
}
