using Rucula.DataAccess.Fetching.Byma.Config;

namespace Rucula.DataAccess.Fetching.Byma
{
    internal class BymaLetrasFetcher : BymaFetcherBase, IBymaLetrasFetcher
    {
        public BymaLetrasFetcher(ILetrasBymaHttpConfig letrasBymaHttpConfig, IBymaHttpReader bymaHttpReader)
            : base(letrasBymaHttpConfig, bymaHttpReader) { }
    }
}
