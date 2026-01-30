using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class SignedInPayload : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "signed_in";
}