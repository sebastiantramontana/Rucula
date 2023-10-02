using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma
{
    internal class BymaBonosFetcher : BymaFetcherBase, IBymaBonosFetcher
    {
        public BymaBonosFetcher(IBonosRequestFactory bonosRequestFactory, IHttpReader httpReader)
            : base(bonosRequestFactory, httpReader) { }
    }
}
