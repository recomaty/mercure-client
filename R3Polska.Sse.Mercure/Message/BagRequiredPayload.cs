using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class BagRequiredPayload : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "bag_required";
}
