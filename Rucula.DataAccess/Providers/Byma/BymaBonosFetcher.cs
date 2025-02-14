﻿using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma;

internal sealed class BymaBonosFetcher(IBonosRequestFactory bonosRequestFactory, IHttpReader httpReader) : BymaFetcherBase(bonosRequestFactory, httpReader), IBymaBonosFetcher
{
}
