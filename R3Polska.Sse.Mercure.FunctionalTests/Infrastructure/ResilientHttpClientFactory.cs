using System.Net;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace R3Polska.Sse.Mercure.FunctionalTests.Infrastructure;

public static class ResilientHttpClientFactory
{
    public static HttpClient CreateWithRetry(int retryCount, TimeSpan retryInterval)
    {
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(response => response.StatusCode == HttpStatusCode.ServiceUnavailable)
            .Or<HttpRequestException>()
            .WaitAndRetryAsync(
                retryCount,
                _ => retryInterval,
                onRetry: (outcome, timespan, retryAttempt, _) =>
                {
                    Console.WriteLine($"[Retry] Attempt {retryAttempt} after {timespan.TotalSeconds}s delay. " +
                                      $"Reason: {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
                });

        var handler = new RetryPolicyHandler(retryPolicy)
        {
            InnerHandler = new HttpClientHandler()
        };

        return new HttpClient(handler)
        {
            Timeout = TimeSpan.FromMinutes(2)
        };
    }
}

internal class RetryPolicyHandler : DelegatingHandler
{
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    public RetryPolicyHandler(AsyncRetryPolicy<HttpResponseMessage> retryPolicy)
    {
        _retryPolicy = retryPolicy;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            // Clone the request for retry (HttpRequestMessage can only be sent once)
            var clonedRequest = await CloneRequestAsync(request);
            return await base.SendAsync(clonedRequest, cancellationToken);
        });
    }

    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        // Copy headers
        foreach (var header in request.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        // Copy content if present
        if (request.Content != null)
        {
            var content = await request.Content.ReadAsStringAsync();
            clone.Content = new StringContent(content);

            // Copy content headers
            foreach (var header in request.Content.Headers)
            {
                clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        return clone;
    }
}