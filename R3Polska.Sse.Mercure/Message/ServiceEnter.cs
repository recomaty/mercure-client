using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class ServiceEnterPayload : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "service_enter";
}