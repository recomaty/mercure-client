using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

/// <summary>
/// Mercure payload for when machine enters DEBOUNCING state (attempted bottle removal)
/// </summary>
public class DebouncingPayload : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "debouncing";
}
