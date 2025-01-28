﻿using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma;

internal sealed class BymaTituloDetailsFetcher(ITituloDetailsRequestFactory tituloDetailsRequestFactory, IHttpReader httpReader) : BymaParametrizableFetcherBase(tituloDetailsRequestFactory, httpReader), IBymaTituloDetailsFetcher
{
}
