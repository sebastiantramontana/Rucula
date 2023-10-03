namespace Rucula.DataAccess.Providers.WesternUnion
{
    internal class WesternUnionFetcher : IWesternUnionFetcher
    {
        private const string Url = "https://www.westernunion.com/wuconnect/prices/catalog";
        private readonly IHttpReader _httpReader;

        public WesternUnionFetcher(IHttpReader httpReader)
        {
            _httpReader = httpReader;
        }

        public async Task<string> Fetch()
        {
            using var request = CreateRequest();
            return await _httpReader.Read(request).ConfigureAwait(false);
        }

        private HttpRequestMessage CreateRequest()
            => new(HttpMethod.Post, Url) { Content = new StringContent(JsonParameter) };

        private string JsonParameter { get; } = @"{
                                        ""header_request"": {
                                        ""version"": ""0.5"",
                                        ""request_type"": ""PRICECATALOG"",
                                        ""correlation_id"": ""web-4541fb53-975e-4a83-815e-eabebcfa3c7d"",
                                        ""transaction_id"": ""web-4541fb53-975e-4a83-815e-eabebcfa3c7d-1696277376679""
                                        },
                                        ""sender"": {
                                            ""client"": ""WUCOM"",
                                            ""channel"": ""WWEB"",
                                            ""cty_iso2_ext"": ""US"",
                                            ""curr_iso3"": ""USD"",
                                            ""province_state"": ""CA"",
                                            ""funds_in"": ""*"",
                                            ""send_amount"": 10000,
                                            ""air_requested"": ""Y"",
                                            ""efl_type"": ""STATE"",
                                            ""efl_value"": ""CA""
                                        },
                                        ""receiver"": {
                                            ""curr_iso3"": ""ARS"",
                                            ""cty_iso2_ext"": ""AR"",
                                            ""cty_iso2"": ""AR""
                                        },
                                        ""visit"": {
                                            ""local_datetime"": {
                                                ""timestamp_ms"": 1696277376680,
                                                ""timezone"": 180
                                            }
                                        },
                                        ""visitor"": {
                                            ""fingerprint_id"": ""029ba1c600c44626189fcf8270f499e6""
                                        }
                                }";
    }
}

