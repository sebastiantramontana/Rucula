using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma
{
    internal class BymaLetrasFetcher : BymaFetcherBase, IBymaLetrasFetcher
    {
        public BymaLetrasFetcher(ILetrasRequestFactory letrasRequestFactory, IHttpReader httpReader)
            : base(letrasRequestFactory, httpReader) { }
    }
}
