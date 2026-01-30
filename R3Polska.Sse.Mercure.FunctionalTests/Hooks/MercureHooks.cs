using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using R3Polska.Sse.Mercure.FunctionalTests.Infrastructure;
using Reqnroll;

namespace R3Polska.Sse.Mercure.FunctionalTests.Hooks;

[Binding]
public class MercureHooks
{
    private static DockerComposeFixture? _fixture;
    private readonly TestContext _context;

    public MercureHooks(TestContext context)
    {
        _context = context;
    }

    [BeforeTestRun]
    public static async Task BeforeTestRun()
    {
        _fixture = new DockerComposeFixture();
        await _fixture.InitializeAsync();
    }

    [AfterTestRun]
    public static async Task AfterTestRun()
    {
        if (_fixture != null)
        {
            await _fixture.DisposeAsync();
        }
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        _context.Fixture = _fixture;

        // Create MercureService with real HttpClient
        var options = Options.Create(new MercurePublisherOptions
        {
            Host = _fixture!.MercureHost,
            Token = _fixture.MercureToken
        });

        var logger = new NullLogger<MercureService>();
        var httpClient = new HttpClient();

        _context.MercureService = new MercureService(logger, options, httpClient);
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_context.Subscriber != null)
        {
            await _context.Subscriber.DisposeAsync();
        }
    }

    private class NullLogger<T> : ILogger<T>
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
        public bool IsEnabled(LogLevel logLevel) => false;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) { }
    }
}