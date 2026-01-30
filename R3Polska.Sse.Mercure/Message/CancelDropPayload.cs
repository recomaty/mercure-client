using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

/// <summary>
/// Mercure payload for when drop is canceled by user
/// </summary>
public class CancelDropPayload : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "cancel_drop";
}
