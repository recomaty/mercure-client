namespace R3Polska.Sse.Mercure;

/// <summary>
/// Interface for publishing messages to Mercure.
/// </summary>
public interface IMercureService
{
    /// <summary>
    /// Publishes a message to Mercure.
    /// </summary>
    /// <typeparam name="T">The payload type implementing IMercureMessagePayload.</typeparam>
    /// <param name="mercureMessage">The message to publish.</param>
    Task Publish<T>(MercureMessage mercureMessage) where T : IMercureMessagePayload;

    /// <summary>
    /// Kills/clears the publishers queue by sending an initialization message.
    /// </summary>
    Task KillPublishersQueue();
}