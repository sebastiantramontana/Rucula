using System.Net.Http.Headers;

namespace Rucula.DataAccess.Providers.Byma.RequestFactories;

internal abstract class ParametrizableRequestFactoryBase(string url, IBymaRequestPostFactory bymaRequestFactory) : IParametrizableRequestFactory
{
    public HttpRequestMessage CreateRequest(string parameters)
        => bymaRequestFactory.CreateRequestPost(url, parameters);
}
