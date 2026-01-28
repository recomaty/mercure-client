using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class ProductDetectionPayload(string ean) : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "product_detection";

    [JsonPropertyName("ean")]
    public string Ean { get; } = ean;
}