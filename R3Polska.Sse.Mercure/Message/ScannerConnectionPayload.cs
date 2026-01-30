using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

/// <summary>
/// Ładunek wiadomości Mercure zawierający informacje o stanie połączenia skanera.
/// </summary>
public class ScannerConnectionPayload : IMercureMessagePayload
{
    /// <summary>
    /// Stan połączenia skanera.
    /// Możliwe wartości: "scanner_ok" (podłączony), "scanner_missing" (odłączony).
    /// </summary>
    [JsonPropertyName("state")]
    public required string State { get; set; }
}