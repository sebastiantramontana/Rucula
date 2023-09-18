using Rucula.DataAccess.Fetching.Byma.Config;

namespace Rucula.DataAccess.Fetching.Byma
{
    internal class BymaTituloDetailsFetcher : BymaParametrizableFetcherBase, IBymaTituloDetailsFetcher
    {
        public BymaTituloDetailsFetcher(ITituloDetailsBymaHttpConfig tituloDetailsBymaHttpConfig, IBymaHttpReader bymaHttpReader)
            : base(tituloDetailsBymaHttpConfig, bymaHttpReader)
        {
        }
    }
}
