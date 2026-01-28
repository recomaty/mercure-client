using System.Text.Json;

namespace R3Polska.Sse.Mercure.FunctionalTests.Infrastructure;

public class MercureSubscriber : IAsyncDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _mercureHost;
    private readonly string _topic;
    private readonly List<MercureEvent> _receivedEvents = new();
    private CancellationTokenSource? _cts;
    private Task? _subscriptionTask;

    public IReadOnlyList<MercureEvent> ReceivedEvents => _receivedEvents.AsReadOnly();

    public MercureSubscriber(string mercureHost, string topic)
    {
        _mercureHost = mercureHost;
        _topic = topic;
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromMinutes(5);
    }

    public void StartListening()
    {
        _cts = new CancellationTokenSource();
        _subscriptionTask = SubscribeAsync(_cts.Token);
    }

    public async Task StopListeningAsync()
    {
        if (_cts != null)
        {
            await _cts.CancelAsync();
            try
            {
                if (_subscriptionTask != null)
                {
                    await _subscriptionTask;
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when cancelling
            }
        }
    }

    public async Task<MercureEvent?> WaitForEventAsync(TimeSpan timeout, Func<MercureEvent, bool>? predicate = null)
    {
        var deadline = DateTime.UtcNow + timeout;

        while (DateTime.UtcNow < deadline)
        {
            var matchingEvent = predicate != null
                ? _receivedEvents.FirstOrDefault(predicate)
                : _receivedEvents.FirstOrDefault();

            if (matchingEvent != null)
            {
                return matchingEvent;
            }

            await Task.Delay(100);
        }

        return null;
    }

    public async Task<List<MercureEvent>> WaitForEventsAsync(int count, TimeSpan timeout)
    {
        var deadline = DateTime.UtcNow + timeout;

        while (DateTime.UtcNow < deadline)
        {
            if (_receivedEvents.Count >= count)
            {
                return _receivedEvents.Take(count).ToList();
            }

            await Task.Delay(100);
        }

        return _receivedEvents.ToList();
    }

    public void ClearEvents()
    {
        _receivedEvents.Clear();
    }

    private async Task SubscribeAsync(CancellationToken cancellationToken)
    {
        var url = $"{_mercureHost}/.well-known/mercure?topic={Uri.EscapeDataString(_topic)}";

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/event-stream"));

            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var reader = new StreamReader(stream);

            var currentEvent = new MercureEventBuilder();

            while (!cancellationToken.IsCancellationRequested)
            {
                var line = await reader.ReadLineAsync(cancellationToken);

                if (line == null)
                {
                    break;
                }

                if (string.IsNullOrEmpty(line))
                {
                    // Empty line means end of event
                    if (currentEvent.HasData)
                    {
                        _receivedEvents.Add(currentEvent.Build());
                        currentEvent = new MercureEventBuilder();
                    }
                    continue;
                }

                if (line.StartsWith("data:"))
                {
                    currentEvent.Data = line.Substring(5).Trim();
                }
                else if (line.StartsWith("id:"))
                {
                    currentEvent.Id = line.Substring(3).Trim();
                }
                else if (line.StartsWith("event:"))
                {
                    currentEvent.EventType = line.Substring(6).Trim();
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Expected when stopping
        }
        catch (IOException)
        {
            // Connection closed
        }
    }

    public async ValueTask DisposeAsync()
    {
        await StopListeningAsync();
        _cts?.Dispose();
        _httpClient.Dispose();
    }

    private class MercureEventBuilder
    {
        public string? Id { get; set; }
        public string? EventType { get; set; }
        public string? Data { get; set; }

        public bool HasData => !string.IsNullOrEmpty(Data);

        public MercureEvent Build() => new(Id, EventType, Data ?? string.Empty);
    }
}

public record MercureEvent(string? Id, string? EventType, string Data)
{
    public T? DeserializeData<T>() where T : class
    {
        try
        {
            return JsonSerializer.Deserialize<T>(Data);
        }
        catch
        {
            return null;
        }
    }

    public JsonDocument? GetDataAsJson()
    {
        try
        {
            return JsonDocument.Parse(Data);
        }
        catch
        {
            return null;
        }
    }
}