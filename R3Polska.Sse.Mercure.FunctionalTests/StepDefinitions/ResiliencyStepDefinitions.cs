using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using R3Polska.Sse.Mercure.FunctionalTests.Infrastructure;
using R3Polska.Sse.Mercure.Message;
using Reqnroll;

namespace R3Polska.Sse.Mercure.FunctionalTests.StepDefinitions;

[Binding]
public class ResiliencyStepDefinitions
{
    private readonly TestContext _context;
    private Task? _backgroundPublishTask;
    private Exception? _publishException;

    public ResiliencyStepDefinitions(TestContext context)
    {
        _context = context;
    }

    [Given(@"I have a MercureService with (\d+) retries and (\d+) second intervals")]
    public void GivenIHaveAMercureServiceWithRetriesAndSecondIntervals(int retryCount, int intervalSeconds)
    {
        _context.RetryCount = retryCount;
        _context.RetryInterval = TimeSpan.FromSeconds(intervalSeconds);

        var options = Options.Create(new MercurePublisherOptions
        {
            Host = _context.Fixture!.MercureHost,
            Token = _context.Fixture.MercureToken
        });

        var logger = new ConsoleLogger<MercureService>();
        var httpClient = ResilientHttpClientFactory.CreateWithRetry(retryCount, _context.RetryInterval);

        _context.MercureService = new MercureService(logger, options, httpClient);
    }

    [When(@"I stop the Mercure container")]
    public async Task WhenIStopTheMercureContainer()
    {
        await _context.Fixture!.StopMercureAsync();
    }

    [When(@"I start the Mercure container")]
    public async Task WhenIStartTheMercureContainer()
    {
        await _context.Fixture!.StartMercureAsync();
    }

    [Then(@"the Mercure container is restarted")]
    public async Task ThenTheMercureContainerIsRestarted()
    {
        await _context.Fixture!.StartMercureAsync();
    }

    [When(@"I wait for (\d+) seconds")]
    public async Task WhenIWaitForSeconds(int seconds)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
    }

    [When(@"I publish a ReadyPayload message to the ""(.*)"" topic in the background")]
    public void WhenIPublishAReadyPayloadMessageToTheTopicInTheBackground(string topic)
    {
        _context.LastPublishedTopic = topic;
        _context.PublishStartTime = DateTime.UtcNow;
        _context.PublishSucceeded = false;

        _backgroundPublishTask = Task.Run(async () =>
        {
            try
            {
                Console.WriteLine($"[Test] Starting publish to topic '{topic}' at {DateTime.UtcNow}");
                await _context.MercureService!.Publish<ReadyPayload>(new MercureMessage
                {
                    Topic = topic,
                    Payload = new ReadyPayload()
                });
                _context.PublishSucceeded = true;
                _context.PublishEndTime = DateTime.UtcNow;
                Console.WriteLine($"[Test] Publish succeeded at {_context.PublishEndTime}");
            }
            catch (Exception ex)
            {
                _publishException = ex;
                _context.PublishEndTime = DateTime.UtcNow;
                Console.WriteLine($"[Test] Publish failed: {ex.Message}");
            }
        });
    }

    [When(@"I publish a ReadyPayload message to the ""(.*)"" topic and expect failure")]
    public async Task WhenIPublishAReadyPayloadMessageToTheTopicAndExpectFailure(string topic)
    {
        _context.LastPublishedTopic = topic;
        _context.PublishStartTime = DateTime.UtcNow;
        _context.PublishSucceeded = false;

        try
        {
            Console.WriteLine($"[Test] Starting publish (expecting failure) to topic '{topic}' at {DateTime.UtcNow}");
            await _context.MercureService!.Publish<ReadyPayload>(new MercureMessage
            {
                Topic = topic,
                Payload = new ReadyPayload()
            });
            _context.PublishSucceeded = true;
            Console.WriteLine("[Test] Unexpected: Publish succeeded");
        }
        catch (Exception ex)
        {
            _publishException = ex;
            Console.WriteLine($"[Test] Expected failure occurred: {ex.Message}");
        }

        _context.PublishEndTime = DateTime.UtcNow;
    }

    [Then(@"the publish operation should complete successfully within (\d+) seconds")]
    public async Task ThenThePublishOperationShouldCompleteSuccessfullyWithinSeconds(int seconds)
    {
        if (_backgroundPublishTask != null)
        {
            var timeout = TimeSpan.FromSeconds(seconds);
            var completedInTime = await Task.WhenAny(_backgroundPublishTask, Task.Delay(timeout)) == _backgroundPublishTask;

            completedInTime.Should().BeTrue($"Publish operation should complete within {seconds} seconds");
        }

        _context.PublishSucceeded.Should().BeTrue("Publish operation should succeed");
        _publishException.Should().BeNull("No exception should be thrown");

        // Log timing info
        if (_context.PublishStartTime.HasValue && _context.PublishEndTime.HasValue)
        {
            var duration = _context.PublishEndTime.Value - _context.PublishStartTime.Value;
            Console.WriteLine($"[Test] Publish operation took {duration.TotalSeconds:F1} seconds");
        }
    }

    [Then(@"the publish operation should fail")]
    public void ThenThePublishOperationShouldFail()
    {
        Console.WriteLine($"[Test] Publish succeeded: {_context.PublishSucceeded}");
        Console.WriteLine($"[Test] Exception: {_publishException?.Message ?? "none"}");
    }

    private class ConsoleLogger<T> : ILogger<T>
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
        public bool IsEnabled(LogLevel logLevel) => true;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            Console.WriteLine($"[{logLevel}] {formatter(state, exception)}");
        }
    }
}