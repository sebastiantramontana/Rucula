namespace Rucula.DataAccess.Providers.DolarApp;

internal sealed class DolarAppFetcher(IHttpReader httpReader) : IDolarAppFetcher
{
    private const string ReaderKey = "DolarApp";
    private const string Url = "https://ruculaproxycors-cqdccfanbbexh5hb.brazilsouth-01.azurewebsites.net/api/ProxyCors?code=4taHWnlKbpqwMTufEJ-UAWqKhepOL7pUuDRz8v_20-hdAzFuhuJLHQ==&url=https%3A%2F%2Fapi.dolarapp.com%2Fv1%2Ftickers%3Fcurrencies%3DARS";

    public async Task<string> Fetch()
    {
        using var request = CreateRequest();
        return await httpReader.Read(ReaderKey, request);
    }

    private static HttpRequestMessage CreateRequest()
        => new(HttpMethod.Get, Url);
}

