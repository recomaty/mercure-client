using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class SessionExpiredPayload : IMercureMessagePayload
{

    [JsonPropertyName("state")]
    public string State { get; } = "session_expired";
}