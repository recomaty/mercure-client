using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class PrinterPaperStatePayload(string paperState) : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; init; } = $"paper_state_{paperState}";
}