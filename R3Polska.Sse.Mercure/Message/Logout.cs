using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class LogoutPayload : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "signed_out";
}