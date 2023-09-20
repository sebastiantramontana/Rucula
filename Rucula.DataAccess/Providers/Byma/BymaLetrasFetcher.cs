using Rucula.DataAccess.Providers.Byma.Config;

namespace Rucula.DataAccess.Providers.Byma
{
    internal class BymaLetrasFetcher : BymaFetcherBase, IBymaLetrasFetcher
    {
        public BymaLetrasFetcher(ILetrasBymaHttpConfig letrasBymaHttpConfig, IBymaHttpReader bymaHttpReader)
            : base(letrasBymaHttpConfig, bymaHttpReader) { }
    }
}
