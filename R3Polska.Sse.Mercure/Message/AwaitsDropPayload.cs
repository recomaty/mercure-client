using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

/// <summary>
/// Mercure payload for when machine enters AWAITS_DROP state
/// </summary>
public class AwaitsDropPayload(string ean) : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "awaits_drop";

    [JsonPropertyName("ean")]
    public string Ean { get; } = ean;
}