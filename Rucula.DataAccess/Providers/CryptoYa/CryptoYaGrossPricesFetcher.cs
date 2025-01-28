namespace Rucula.DataAccess.Providers.CryptoYa;

internal sealed class CryptoYaGrossPricesFetcher(IHttpReader httpReader) : ICryptoYaGrossPricesFetcher
{
    private const string Url = "https://criptoya.com/api/{0}/ARS/{1}";

    public async Task<string> Fetch(CriptoYaGrossPricesFetcherParameters parameters)
    {
        using var request = CreateRequest(parameters);
        return await httpReader.Read(request).ConfigureAwait(false);
    }

    private static HttpRequestMessage CreateRequest(CriptoYaGrossPricesFetcherParameters parameters)
        => new(HttpMethod.Get, string.Format(Url, parameters.CryptoCurrencyKey, parameters.Volume));
}
