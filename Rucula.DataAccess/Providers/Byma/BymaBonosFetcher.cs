using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma;

internal class BymaBonosFetcher(IBonosRequestFactory bonosRequestFactory, IHttpReader httpReader) : BymaFetcherBase(bonosRequestFactory, httpReader), IBymaBonosFetcher
{
}
