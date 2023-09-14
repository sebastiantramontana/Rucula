using Rucula.DataAccess.Fetching.Byma.Config;

namespace Rucula.DataAccess.Fetching.Byma
{
    internal class LetrasFetcher : BymaFetcherBase, ILetrasFetcher
    {
        public LetrasFetcher(ILetrasBymaHttpConfig letrasBymaHttpConfig, IBymaHttpReader bymaHttpReader)
            : base(letrasBymaHttpConfig, bymaHttpReader) { }
    }
}
