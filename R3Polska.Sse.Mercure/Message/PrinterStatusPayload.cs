using System.Text.Json.Serialization;

namespace R3Polska.Sse.Mercure.Message;

public class PrinterStatusPayload(string printerState) : IMercureMessagePayload
{
    public PrinterStatusPayload(string printerState, string jobId, int waitTimeSeconds) : this(printerState)
    {
        PrintJobId = jobId;
        WaitTimeInSeconds = waitTimeSeconds;
    }
    
    [JsonPropertyName("state")]
    public string State { get; init; } = $"printer_{printerState}";

    public string? PrintJobId { get; set; }
    public int? WaitTimeInSeconds { get; set; }
    
    [JsonPropertyName("paper_state")]
    public int? PaperStatus { get; set; }
}