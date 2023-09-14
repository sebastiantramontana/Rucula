using Rucula.DataAccess.Fetching.Byma.Config;

namespace Rucula.DataAccess.Fetching.Byma
{
    internal class BonosFetcher : BymaFetcherBase, IBonosFetcher
    {
        public BonosFetcher(ILetrasBymaHttpConfig letrasBymaHttpConfig, IBymaHttpReader bymaHttpReader)
            : base(letrasBymaHttpConfig, bymaHttpReader) { }
    }
}
