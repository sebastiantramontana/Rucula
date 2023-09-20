using Rucula.DataAccess.Providers.Byma.Config;

namespace Rucula.DataAccess.Providers.Byma
{
    internal class BymaTituloDetailsFetcher : BymaParametrizableFetcherBase, IBymaTituloDetailsFetcher
    {
        public BymaTituloDetailsFetcher(ITituloDetailsBymaHttpConfig tituloDetailsBymaHttpConfig, IBymaHttpReader bymaHttpReader)
            : base(tituloDetailsBymaHttpConfig, bymaHttpReader)
        {
        }
    }
}
