namespace Rucula.DataAccess.Providers.WesternUnion;

internal sealed class WesternUnionFetcher(IHttpReader httpReader) : IWesternUnionFetcher
{
    private const string Url = "https://www.westernunion.com/wuconnect/prices/catalog";

    public async Task<string> Fetch(double amountToSend)
    {
        using var request = CreateRequest(amountToSend);
        return await httpReader.Read(request).ConfigureAwait(false);
    }

    private static HttpRequestMessage CreateRequest(double amountToSend)
        => new(HttpMethod.Post, Url) { Content = new StringContent(CreateJsonParameter(amountToSend)) };

    private static string CreateJsonParameter(double amountToSend)
        => $@"{{
                    ""header_request"": {{
                    ""version"": ""0.5"",
                    ""request_type"": ""PRICECATALOG"",
                    ""correlation_id"": ""web-4541fb53-975e-4a83-815e-eabebcfa3c7d"",
                    ""transaction_id"": ""web-4541fb53-975e-4a83-815e-eabebcfa3c7d-1696277376679""
                    }},
                    ""sender"": {{
                        ""client"": ""WUCOM"",
                        ""channel"": ""WWEB"",
                        ""cty_iso2_ext"": ""US"",
                        ""curr_iso3"": ""USD"",
                        ""province_state"": ""CA"",
                        ""funds_in"": ""AC"",
                        ""send_amount"": {amountToSend},
                        ""air_requested"": ""Y"",
                        ""efl_type"": ""STATE"",
                        ""efl_value"": ""CA""
                    }},
                    ""receiver"": {{
                        ""curr_iso3"": ""ARS"",
                        ""cty_iso2_ext"": ""AR"",
                        ""cty_iso2"": ""AR""
                    }},
                    ""visit"": {{
                        ""local_datetime"": {{
                            ""timestamp_ms"": 1696277376680,
                            ""timezone"": 180
                        }}
                    }},
                    ""visitor"": {{
                        ""fingerprint_id"": ""029ba1c600c44626189fcf8270f499e6""
                    }}
            }}";
}

