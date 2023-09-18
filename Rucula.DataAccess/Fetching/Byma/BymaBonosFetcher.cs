using Rucula.DataAccess.Fetching.Byma.Config;

namespace Rucula.DataAccess.Fetching.Byma
{
    internal class BymaBonosFetcher : BymaFetcherBase, IBymaBonosFetcher
    {
        public BymaBonosFetcher(IBonosBymaHttpConfig letrasBymaHttpConfig, IBymaHttpReader bymaHttpReader)
            : base(letrasBymaHttpConfig, bymaHttpReader) { }
    }
}
