using R3Polska.Sse.Mercure.Message;
using Shouldly;

namespace R3Polska.Sse.Mercure.Tests;

public class MercureMessageTests
{
    [Fact]
    public void MercureMessage_Id_DefaultsToNull()
    {
        var message = new MercureMessage
        {
            Topic = "test-topic",
            Payload = new ReadyPayload()
        };
        message.Id.ShouldBeNull();
    }

    [Fact]
    public void MercureMessage_Id_CanBeSet()
    {
        var message = new MercureMessage
        {
            Topic = "test-topic",
            Payload = new ReadyPayload(),
            Id = "message-123"
        };
        message.Id.ShouldBe("message-123");
    }

    [Fact]
    public void MercureMessage_Topic_IsRequired()
    {
        var message = new MercureMessage
        {
            Topic = "scan",
            Payload = new ReadyPayload()
        };
        message.Topic.ShouldBe("scan");
    }

    [Fact]
    public void MercureMessage_Topic_CanBeChanged()
    {
        var message = new MercureMessage
        {
            Topic = "initial-topic",
            Payload = new ReadyPayload()
        };
        message.Topic = "changed-topic";
        message.Topic.ShouldBe("changed-topic");
    }

    [Fact]
    public void MercureMessage_Payload_IsRequired()
    {
        var payload = new ReadyPayload();
        var message = new MercureMessage
        {
            Topic = "scan",
            Payload = payload
        };
        message.Payload.ShouldBeSameAs(payload);
    }

    [Fact]
    public void MercureMessage_Payload_CanBeAnyIMercureMessagePayload()
    {
        var payloads = new IMercureMessagePayload[]
        {
            new ReadyPayload(),
            new BagClosing(),
            new AwaitsDropPayload("ean"),
            new PrinterStatusPayload("ready")
        };

        foreach (var payload in payloads)
        {
            var message = new MercureMessage
            {
                Topic = "scan",
                Payload = payload
            };
            message.Payload.ShouldBeSameAs(payload);
        }
    }

    [Fact]
    public void MercureMessage_WithId_ShouldMaintainAllProperties()
    {
        var payload = new FinishingPayload();
        var message = new MercureMessage
        {
            Id = "unique-id-456",
            Topic = "notifications",
            Payload = payload
        };

        message.Id.ShouldBe("unique-id-456");
        message.Topic.ShouldBe("notifications");
        message.Payload.ShouldBeSameAs(payload);
    }

    [Theory]
    [InlineData("scan")]
    [InlineData("notifications")]
    [InlineData("updates")]
    [InlineData("")]
    public void MercureMessage_Topic_AcceptsVariousValues(string topic)
    {
        var message = new MercureMessage
        {
            Topic = topic,
            Payload = new ReadyPayload()
        };
        message.Topic.ShouldBe(topic);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("INIT")]
    [InlineData("message-with-dashes")]
    [InlineData("message_with_underscores")]
    public void MercureMessage_Id_AcceptsVariousValues(string? id)
    {
        var message = new MercureMessage
        {
            Topic = "scan",
            Payload = new ReadyPayload(),
            Id = id
        };
        message.Id.ShouldBe(id);
    }
}