using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class FinishedPayload : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "finished";
}