using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class WaitForBag : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "wait_for_bag";
}