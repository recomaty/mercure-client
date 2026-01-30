namespace R3Polska.Sse.Mercure;

public class MercureMessage
{
    public string? Id { get; set; } = null;

    public required string Topic { get; set; }

    public required IMercureMessagePayload Payload { get; set;}

}