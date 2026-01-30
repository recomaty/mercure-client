using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class UnknownProductPayload(string ean) : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "product_unknown";

    [JsonPropertyName("ean")]
    public string Ean { get; } = ean;
}