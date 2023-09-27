using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma
{
    internal class BymaTituloDetailsFetcher : BymaParametrizableFetcherBase, IBymaTituloDetailsFetcher
    {
        public BymaTituloDetailsFetcher(ITituloDetailsRequestFactory tituloDetailsRequestFactory, IBymaHttpReader bymaHttpReader)
            : base(tituloDetailsRequestFactory, bymaHttpReader)
        {
        }
    }
}
