using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

/// <summary>
/// Reprezentuje wiadomość Mercure informującą o odrzuceniu produktu.
/// Wysyłana gdy produkt nie jest dozwolony do akceptacji w systemie.
/// </summary>
/// <param name="ean">Kod EAN produktu, który został odrzucony.</param>
public class ProductNotAllowedPayload(string ean) : IMercureMessagePayload
{
    /// <summary>
    /// Stan wiadomości identyfikujący typ zdarzenia.
    /// Domyślnie zwraca "product_not_allowed".
    /// </summary>
    [JsonPropertyName("state")]
    public string State { get; } = "product_not_allowed";

    /// <summary>
    /// Kod EAN produktu, który nie został zaakceptowany.
    /// </summary>
    [JsonPropertyName("ean")]
    public string Ean { get; } = ean;
}