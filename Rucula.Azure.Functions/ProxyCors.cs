using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace Rucula.Azure.Functions;

public class ProxyCors(IHttpClientFactory httpClientFactory)
{
    private const string UrlQueryParameter = "url";

    [Function(nameof(ProxyCors))]
    public Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "options")] HttpRequestData req)
        => TryRespondPreflightByOptionsMethod(req) ?? RespondUrlProxy(req);

    private static Task<HttpResponseData>? TryRespondPreflightByOptionsMethod(HttpRequestData req)
    {
        Task<HttpResponseData>? preflightTask = null;

        if (req.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
        {
            var preflight = req.CreateResponse(HttpStatusCode.OK);
            preflight.Headers.Add("Access-Control-Allow-Origin", "*");
            preflight.Headers.Add("Access-Control-Allow-Methods", "GET,OPTIONS");
            preflight.Headers.Add("Access-Control-Allow-Headers", "Content-Type,Authorization,x-functions-key");

            preflightTask = Task.FromResult(preflight);
        }

        return preflightTask;
    }

    private Task<HttpResponseData> RespondUrlProxy(HttpRequestData req)
        => TryRespondUrlParameterMissing(req) ?? RequestToUrlTarget(req);

    private async Task<HttpResponseData> RequestToUrlTarget(HttpRequestData req)
    {
        HttpResponseData response;

        try
        {
            using var targetResponse = await DoHttpGetToUrlTarget(req);

            response = targetResponse.StatusCode == HttpStatusCode.OK
                        ? await CreateOkResponseWithTargetContent(req, targetResponse.Content)
                        : CreateFailedResponseFromTarget(req, targetResponse.StatusCode);
        }
        catch (UriFormatException ex)
        {
            response = CreateBadRequestResponse(req, ex.Message);
        }

        return response;
    }

    private static HttpResponseData CreateFailedResponseFromTarget(HttpRequestData req, HttpStatusCode targetFailedStatusCode)
        => req.CreateResponse(targetFailedStatusCode);

    private static async Task<HttpResponseData> CreateOkResponseWithTargetContent(HttpRequestData req, HttpContent httpTargetContent)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);

        AddNoCacheHeaders(response);
        AddContentType(response);

        var content = await httpTargetContent.ReadAsStringAsync();
        await response.WriteStringAsync(content);

        return response;
    }

    private Task<HttpResponseMessage> DoHttpGetToUrlTarget(HttpRequestData req)
    {
        var client = httpClientFactory.CreateClient("proxycors");
        var urlTarget = req.Query[UrlQueryParameter];

        return Uri.IsWellFormedUriString(urlTarget, UriKind.Absolute)
            ? client.GetAsync(urlTarget)
            : throw new UriFormatException($"Wrong Url format: {urlTarget}");
    }

    private static Task<HttpResponseData>? TryRespondUrlParameterMissing(HttpRequestData req)
        => string.IsNullOrWhiteSpace(req.Query[UrlQueryParameter])
            ? Task.FromResult(CreateBadRequestResponse(req, "Url parameter missing in querystring!"))
            : null;

    private static HttpResponseData CreateBadRequestResponse(HttpRequestData req, string message)
    {
        var response = req.CreateResponse(HttpStatusCode.BadRequest);
        response.WriteString(message);

        return response;
    }

    private static void AddNoCacheHeaders(HttpResponseData response)
    {
        response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0");
        response.Headers.Add("Pragma", "no-cache");
    }

    private static void AddContentType(HttpResponseData response)
        => response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
}