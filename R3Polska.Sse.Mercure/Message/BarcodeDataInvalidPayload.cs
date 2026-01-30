using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class BarcodeDataInvalidPayload : IMercureMessagePayload
{
    [JsonPropertyName("state")]
    public string State { get; } = "barcode_data_invalid";
}
