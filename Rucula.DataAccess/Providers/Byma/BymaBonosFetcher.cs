using Rucula.DataAccess.Providers.Byma.Config;

namespace Rucula.DataAccess.Providers.Byma
{
    internal class BymaBonosFetcher : BymaFetcherBase, IBymaBonosFetcher
    {
        public BymaBonosFetcher(IBonosBymaHttpConfig letrasBymaHttpConfig, IBymaHttpReader bymaHttpReader)
            : base(letrasBymaHttpConfig, bymaHttpReader) { }
    }
}
