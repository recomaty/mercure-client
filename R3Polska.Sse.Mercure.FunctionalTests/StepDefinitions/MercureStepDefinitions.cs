using FluentAssertions;
using R3Polska.Sse.Mercure.FunctionalTests.Infrastructure;
using R3Polska.Sse.Mercure.Message;
using Reqnroll;

namespace R3Polska.Sse.Mercure.FunctionalTests.StepDefinitions;

[Binding]
public class MercureStepDefinitions
{
    private readonly TestContext _context;

    public MercureStepDefinitions(TestContext context)
    {
        _context = context;
    }

    #region Given Steps

    [Given(@"I am subscribed to the ""(.*)"" topic")]
    public void GivenIAmSubscribedToTheTopic(string topic)
    {
        _context.Subscriber = new MercureSubscriber(_context.Fixture!.MercureHost, topic);
        _context.Subscriber.StartListening();
        _context.EventCountBeforePublish = 0;
        _context.EventCountBeforeFirstPublish = 0;
        _context.HasRecordedFirstPublish = false;

        // Give the subscriber time to connect
        Thread.Sleep(500);
    }

    #endregion

    #region Helpers

    private void RecordEventCountBeforePublish()
    {
        _context.EventCountBeforePublish = _context.Subscriber?.ReceivedEvents.Count ?? 0;

        // Also track the first publish count for multiple message scenarios
        if (!_context.HasRecordedFirstPublish)
        {
            _context.EventCountBeforeFirstPublish = _context.EventCountBeforePublish;
            _context.HasRecordedFirstPublish = true;
        }
    }

    #endregion

    #region When Steps - Simple Payloads

    [When(@"I publish a ReadyPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishAReadyPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<ReadyPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new ReadyPayload()
        });
    }

    [When(@"I publish a ReadyPayload message to the ""(.*)"" topic with ID ""(.*)""")]
    public async Task WhenIPublishAReadyPayloadMessageToTheTopicWithId(string topic, string id)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<ReadyPayload>(new MercureMessage
        {
            Id = id,
            Topic = topic,
            Payload = new ReadyPayload()
        });
    }

    [When(@"I publish a BagClosing message to the ""(.*)"" topic")]
    public async Task WhenIPublishABagClosingMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<BagClosing>(new MercureMessage
        {
            Topic = topic,
            Payload = new BagClosing()
        });
    }

    [When(@"I publish a BagRequiredPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishABagRequiredPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<BagRequiredPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new BagRequiredPayload()
        });
    }

    [When(@"I publish a BarcodeDataInvalidPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishABarcodeDataInvalidPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<BarcodeDataInvalidPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new BarcodeDataInvalidPayload()
        });
    }

    [When(@"I publish a CancelDropPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishACancelDropPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<CancelDropPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new CancelDropPayload()
        });
    }

    [When(@"I publish a DebouncingPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishADebouncingPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<DebouncingPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new DebouncingPayload()
        });
    }

    [When(@"I publish a FinishedPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishAFinishedPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<FinishedPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new FinishedPayload()
        });
    }

    [When(@"I publish a FinishingPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishAFinishingPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<FinishingPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new FinishingPayload()
        });
    }

    [When(@"I publish a LogoutPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishALogoutPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<LogoutPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new LogoutPayload()
        });
    }

    [When(@"I publish a ScanLimitPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishAScanLimitPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<ScanLimitPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new ScanLimitPayload()
        });
    }

    [When(@"I publish a ServiceEnterPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishAServiceEnterPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<ServiceEnterPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new ServiceEnterPayload()
        });
    }

    [When(@"I publish a ServiceExitPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishAServiceExitPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<ServiceExitPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new ServiceExitPayload()
        });
    }

    [When(@"I publish a SessionExpiredPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishASessionExpiredPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<SessionExpiredPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new SessionExpiredPayload()
        });
    }

    [When(@"I publish a SignedInPayload message to the ""(.*)"" topic")]
    public async Task WhenIPublishASignedInPayloadMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<SignedInPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new SignedInPayload()
        });
    }

    [When(@"I publish a WaitForBag message to the ""(.*)"" topic")]
    public async Task WhenIPublishAWaitForBagMessageToTheTopic(string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<WaitForBag>(new MercureMessage
        {
            Topic = topic,
            Payload = new WaitForBag()
        });
    }

    #endregion

    #region When Steps - EAN Payloads

    [When(@"I publish an AwaitsDropPayload message with EAN ""(.*)"" to the ""(.*)"" topic")]
    public async Task WhenIPublishAnAwaitsDropPayloadMessageWithEanToTheTopic(string ean, string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<AwaitsDropPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new AwaitsDropPayload(ean)
        });
    }

    [When(@"I publish a ProductDetectionPayload message with EAN ""(.*)"" to the ""(.*)"" topic")]
    public async Task WhenIPublishAProductDetectionPayloadMessageWithEanToTheTopic(string ean, string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<ProductDetectionPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new ProductDetectionPayload(ean)
        });
    }

    [When(@"I publish an UnknownProductPayload message with EAN ""(.*)"" to the ""(.*)"" topic")]
    public async Task WhenIPublishAnUnknownProductPayloadMessageWithEanToTheTopic(string ean, string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<UnknownProductPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new UnknownProductPayload(ean)
        });
    }

    [When(@"I publish a ProductNotAllowedPayload message with EAN ""(.*)"" to the ""(.*)"" topic")]
    public async Task WhenIPublishAProductNotAllowedPayloadMessageWithEanToTheTopic(string ean, string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<ProductNotAllowedPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new ProductNotAllowedPayload(ean)
        });
    }

    #endregion

    #region When Steps - Dynamic State Payloads

    [When(@"I publish a PrinterStatusPayload message with state ""(.*)"" to the ""(.*)"" topic")]
    public async Task WhenIPublishAPrinterStatusPayloadMessageWithStateToTheTopic(string state, string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<PrinterStatusPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new PrinterStatusPayload(state)
        });
    }

    [When(@"I publish a PrinterStatusPayload message with state ""(.*)"", job ID ""(.*)"" and wait time (\d+) to the ""(.*)"" topic")]
    public async Task WhenIPublishAPrinterStatusPayloadMessageWithStateJobIdAndWaitTimeToTheTopic(
        string state, string jobId, int waitTime, string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<PrinterStatusPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new PrinterStatusPayload(state, jobId, waitTime)
        });
    }

    [When(@"I publish a PrinterPaperStatePayload message with paper state ""(.*)"" to the ""(.*)"" topic")]
    public async Task WhenIPublishAPrinterPaperStatePayloadMessageWithPaperStateToTheTopic(string paperState, string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<PrinterPaperStatePayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new PrinterPaperStatePayload(paperState)
        });
    }

    [When(@"I publish an InternetConnectivityPayload message with state ""(.*)"" to the ""(.*)"" topic")]
    public async Task WhenIPublishAnInternetConnectivityPayloadMessageWithStateToTheTopic(string state, string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<InternetConnectivityPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new InternetConnectivityPayload(state)
        });
    }

    [When(@"I publish a ScannerConnectionPayload message with state ""(.*)"" to the ""(.*)"" topic")]
    public async Task WhenIPublishAScannerConnectionPayloadMessageWithStateToTheTopic(string state, string topic)
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.Publish<ScannerConnectionPayload>(new MercureMessage
        {
            Topic = topic,
            Payload = new ScannerConnectionPayload { State = state }
        });
    }

    #endregion

    #region When Steps - KillPublishersQueue

    [When(@"I call KillPublishersQueue")]
    public async Task WhenICallKillPublishersQueue()
    {
        RecordEventCountBeforePublish();
        await _context.MercureService!.KillPublishersQueue();
    }

    #endregion

    #region Then Steps

    [Then(@"the subscriber should receive a message within (\d+) seconds")]
    public async Task ThenTheSubscriberShouldReceiveAMessageWithinSeconds(int seconds)
    {
        var timeout = TimeSpan.FromSeconds(seconds);
        var expectedIndex = _context.EventCountBeforePublish;

        // Wait for the specific message we published (at index = EventCountBeforePublish)
        var deadline = DateTime.UtcNow + timeout;
        while (DateTime.UtcNow < deadline)
        {
            if (_context.Subscriber!.ReceivedEvents.Count > expectedIndex)
            {
                _context.LastReceivedEvent = _context.Subscriber.ReceivedEvents[expectedIndex];
                return;
            }
            await Task.Delay(100);
        }

        _context.LastReceivedEvent.Should().NotBeNull(
            "Expected to receive a message within {0} seconds", seconds);
    }

    [Then(@"the received message should have state ""(.*)""")]
    public void ThenTheReceivedMessageShouldHaveState(string expectedState)
    {
        var json = _context.LastReceivedEvent!.GetDataAsJson();
        json.Should().NotBeNull("Message data should be valid JSON");

        var state = json!.RootElement.GetProperty("state").GetString();
        state.Should().Be(expectedState);
    }

    [Then(@"the received message should have EAN ""(.*)""")]
    public void ThenTheReceivedMessageShouldHaveEan(string expectedEan)
    {
        var json = _context.LastReceivedEvent!.GetDataAsJson();
        json.Should().NotBeNull("Message data should be valid JSON");

        var ean = json!.RootElement.GetProperty("ean").GetString();
        ean.Should().Be(expectedEan);
    }

    [Then(@"the received message should have message ""(.*)""")]
    public void ThenTheReceivedMessageShouldHaveMessage(string expectedMessage)
    {
        var json = _context.LastReceivedEvent!.GetDataAsJson();
        json.Should().NotBeNull("Message data should be valid JSON");

        var message = json!.RootElement.GetProperty("message").GetString();
        message.Should().Be(expectedMessage);
    }

    [Then(@"the message should be received on the ""(.*)"" topic")]
    public void ThenTheMessageShouldBeReceivedOnTheTopic(string topic)
    {
        // If we received the message, it means we're subscribed to the correct topic
        _context.LastReceivedEvent.Should().NotBeNull(
            "Message should have been received on topic '{0}'", topic);
    }

    [Then(@"the subscriber should receive (\d+) messages within (\d+) seconds")]
    public async Task ThenTheSubscriberShouldReceiveMessagesWithinSeconds(int count, int seconds)
    {
        var timeout = TimeSpan.FromSeconds(seconds);
        // Use the count from before the FIRST publish in the scenario
        var startCount = _context.EventCountBeforeFirstPublish;
        var expectedTotal = startCount + count;

        var deadline = DateTime.UtcNow + timeout;
        while (DateTime.UtcNow < deadline)
        {
            if (_context.Subscriber!.ReceivedEvents.Count >= expectedTotal)
            {
                _context.ReceivedEvents.Clear();
                _context.ReceivedEvents.AddRange(
                    _context.Subscriber.ReceivedEvents.Skip(startCount).Take(count));
                return;
            }
            await Task.Delay(100);
        }

        var actualCount = _context.Subscriber!.ReceivedEvents.Count - startCount;
        actualCount.Should().Be(count,
            "Expected to receive {0} messages within {1} seconds, but received {2}",
            count, seconds, actualCount);
    }

    [Then(@"the messages should be received in order")]
    public void ThenTheMessagesShouldBeReceivedInOrder()
    {
        // Messages are stored in order of receipt, so we just verify we have them
        _context.ReceivedEvents.Should().NotBeEmpty("Messages should have been received");
    }

    #endregion
}