using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class ScanLimitPayload : IMercureMessagePayload
{

    [JsonPropertyName("state")]
    public string State { get; } = "scan_limit";
}