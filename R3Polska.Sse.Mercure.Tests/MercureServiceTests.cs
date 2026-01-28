using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using R3Polska.Sse.Mercure.Message;
using RichardSzalay.MockHttp;

namespace R3Polska.Sse.Mercure.Tests;

public class MercureServiceTests
{
    private readonly Mock<ILogger<MercureService>> _loggerMock;
    private readonly MercurePublisherOptions _options;
    private readonly IOptions<MercurePublisherOptions> _optionsWrapper;

    public MercureServiceTests()
    {
        _loggerMock = new Mock<ILogger<MercureService>>();
        _options = new MercurePublisherOptions
        {
            Host = "http://localhost:3000",
            Token = "test-token"
        };
        _optionsWrapper = Options.Create(_options);
    }

    private MercureService CreateService(HttpClient httpClient)
    {
        return new MercureService(_loggerMock.Object, _optionsWrapper, httpClient);
    }

    #region Constructor and Properties

    [Fact]
    public void Constructor_SetsHostFromOptions()
    {
        var mockHttp = new MockHttpMessageHandler();
        var httpClient = mockHttp.ToHttpClient();

        var service = CreateService(httpClient);

        Assert.Equal("http://localhost:3000", service.Host);
    }

    [Fact]
    public void Constructor_SetsTokenFromOptions()
    {
        var mockHttp = new MockHttpMessageHandler();
        var httpClient = mockHttp.ToHttpClient();

        var service = CreateService(httpClient);

        Assert.Equal("test-token", service.Token);
    }

    [Fact]
    public void Constructor_WithDifferentOptions_SetsCorrectValues()
    {
        var options = new MercurePublisherOptions
        {
            Host = "http://mercure:8080",
            Token = "different-token"
        };
        var optionsWrapper = Options.Create(options);
        var mockHttp = new MockHttpMessageHandler();
        var httpClient = mockHttp.ToHttpClient();

        var service = new MercureService(_loggerMock.Object, optionsWrapper, httpClient);

        Assert.Equal("http://mercure:8080", service.Host);
        Assert.Equal("different-token", service.Token);
    }

    #endregion

    #region Publish Method

    [Fact]
    public async Task Publish_SendsPostRequestToCorrectUrl()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new ReadyPayload()
        });

        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task Publish_IncludesAuthorizationHeader()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .WithHeaders("Authorization", "Bearer test-token")
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new ReadyPayload()
        });

        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task Publish_SendsCorrectContentType()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request => request.Content?.Headers.ContentType?.MediaType == "application/x-www-form-urlencoded")
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new ReadyPayload()
        });

        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task Publish_IncludesTopicInBody()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "my-topic",
            Payload = new ReadyPayload()
        });

        Assert.NotNull(capturedContent);
        Assert.Contains("topic=my-topic", capturedContent);
    }

    [Fact]
    public async Task Publish_IncludesSerializedPayloadInBody()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new ReadyPayload()
        });

        Assert.NotNull(capturedContent);
        Assert.Contains("data=", capturedContent);
        Assert.Contains("state", capturedContent);
    }

    [Fact]
    public async Task Publish_WithId_IncludesIdInBody()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Id = "message-123",
            Topic = "scan",
            Payload = new ReadyPayload()
        });

        Assert.NotNull(capturedContent);
        Assert.Contains("id=message-123", capturedContent);
    }

    [Fact]
    public async Task Publish_WithoutId_DoesNotIncludeIdInBody()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new ReadyPayload()
        });

        Assert.NotNull(capturedContent);
        Assert.DoesNotContain("id=", capturedContent);
    }

    [Fact]
    public async Task Publish_OnSuccess_LogsInformationTwice()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("*").Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new ReadyPayload()
        });

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Exactly(2));
    }

    [Fact]
    public async Task Publish_OnFailure_LogsError()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("*").Respond(HttpStatusCode.InternalServerError);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new ReadyPayload()
        });

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.Forbidden)]
    [InlineData(HttpStatusCode.NotFound)]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    public async Task Publish_OnNonOkStatusCode_LogsError(HttpStatusCode statusCode)
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("*").Respond(statusCode);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new ReadyPayload()
        });

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Publish_WithEanPayload_SerializesCorrectly()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<AwaitsDropPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new AwaitsDropPayload("1234567890123")
        });

        Assert.NotNull(capturedContent);
        Assert.Contains("1234567890123", capturedContent);
    }

    [Fact]
    public async Task Publish_WithPrinterStatusPayload_SerializesAllFields()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<PrinterStatusPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new PrinterStatusPayload("printing", "job-123", 30)
        });

        Assert.NotNull(capturedContent);
        Assert.Contains("printer_printing", capturedContent);
    }

    #endregion

    #region KillPublishersQueue Method

    [Fact]
    public async Task KillPublishersQueue_PublishesInitializationPayload()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.KillPublishersQueue();

        Assert.NotNull(capturedContent);
        Assert.Contains("initialization", capturedContent);
    }

    [Fact]
    public async Task KillPublishersQueue_UsesScanTopic()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.KillPublishersQueue();

        Assert.NotNull(capturedContent);
        Assert.Contains("topic=scan", capturedContent);
    }

    [Fact]
    public async Task KillPublishersQueue_DoesNotIncludeId()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.KillPublishersQueue();

        Assert.NotNull(capturedContent);
        Assert.DoesNotContain("id=", capturedContent);
    }

    #endregion

    #region Different Payload Types

    [Fact]
    public async Task Publish_WithFinishingPayload_IncludesMessage()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<FinishingPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new FinishingPayload()
        });

        Assert.NotNull(capturedContent);
        Assert.Contains("finishing", capturedContent);
        Assert.Contains("message", capturedContent);
    }

    [Fact]
    public async Task Publish_WithInternetConnectivityPayload_SerializesDynamicState()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<InternetConnectivityPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new InternetConnectivityPayload("online")
        });

        Assert.NotNull(capturedContent);
        Assert.Contains("inet_connection_online", capturedContent);
    }

    [Fact]
    public async Task Publish_WithScannerConnectionPayload_SerializesCorrectly()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ScannerConnectionPayload>(new MercureMessage
        {
            Topic = "scan",
            Payload = new ScannerConnectionPayload { State = "scanner_ok" }
        });

        Assert.NotNull(capturedContent);
        Assert.Contains("scanner_ok", capturedContent);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public async Task Publish_WithSpecialCharactersInTopic_EncodesCorrectly()
    {
        string? capturedContent = null;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .With(request =>
            {
                capturedContent = request.Content?.ReadAsStringAsync().Result;
                return true;
            })
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "topic/with/slashes",
            Payload = new ReadyPayload()
        });

        Assert.NotNull(capturedContent);
        Assert.Contains("topic=topic/with/slashes", capturedContent);
    }

    [Fact]
    public async Task Publish_WithEmptyTopic_StillSendsRequest()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, "http://localhost:3000/.well-known/mercure")
            .Respond(HttpStatusCode.OK);

        var httpClient = mockHttp.ToHttpClient();
        var service = CreateService(httpClient);

        await service.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = "",
            Payload = new ReadyPayload()
        });

        mockHttp.VerifyNoOutstandingExpectation();
    }

    #endregion
}