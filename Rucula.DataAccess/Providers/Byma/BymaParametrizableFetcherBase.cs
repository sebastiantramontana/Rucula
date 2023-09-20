using Rucula.DataAccess.Providers.Byma.Config;

namespace Rucula.DataAccess.Providers.Byma
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

        public async Task<string> Fetch(string parameters)
        {
            using var request = _parametrizableBymaHttpConfig.CreateRequest(parameters);
            using var handler = _parametrizableBymaHttpConfig.CreateHandler();

            return await _bymaHttpReader.Read(request, handler);
        }
    }
}
