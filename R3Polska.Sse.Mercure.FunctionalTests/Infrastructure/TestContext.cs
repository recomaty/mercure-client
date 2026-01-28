namespace R3Polska.Sse.Mercure.FunctionalTests.Infrastructure;

public class TestContext
{
    public DockerComposeFixture? Fixture { get; set; }
    public MercureSubscriber? Subscriber { get; set; }
    public IMercureService? MercureService { get; set; }
    public MercureEvent? LastReceivedEvent { get; set; }
    public List<MercureEvent> ReceivedEvents { get; } = new();
    public Exception? LastException { get; set; }
    public string? LastPublishedTopic { get; set; }
    public IMercureMessagePayload? LastPublishedPayload { get; set; }
    public int EventCountBeforePublish { get; set; }
    public int EventCountBeforeFirstPublish { get; set; }
    public bool HasRecordedFirstPublish { get; set; }
}