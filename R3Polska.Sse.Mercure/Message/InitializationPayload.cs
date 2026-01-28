using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

internal class InitializationPayload : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; set; } = "initialization";
}