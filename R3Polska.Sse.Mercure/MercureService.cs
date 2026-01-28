using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using R3Polska.Sse.Mercure.Message;

namespace R3Polska.Sse.Mercure;


/// <summary>
/// Serwis wysyłający wiadomości do lokalnego Mercure
/// </summary>
public class MercureService(ILogger<MercureService> logger, IOptions<MercurePublisherOptions> options, HttpClient httpClient) : IMercureService
{
    const string ScanTopic = "scan";
    public string Host { get; protected set; } = options.Value.Host;
    public string Token { get; protected set; } = options.Value.Token;
    protected ILogger Logger = logger;
    protected HttpClient HttpClient = httpClient;

    public async Task KillPublishersQueue()
    {
        //https://tasks.idct.tech/issues/123 niechciana podwójna inicjalizacja
        // await Publish<InitializationPayload>(new MercureMessage()
        // {
        //     Topic = Consts.Topics.ScanTopic,
        //     Id = "INIT",
        //     Payload = new InitializationPayload()
        // });

        await Publish<InitializationPayload>(new MercureMessage()
        {
            Topic = ScanTopic,
            Payload = new InitializationPayload()
        });
    }

    /// <summary>
    /// Wyślij wiadomość do Mercure.
    /// </summary>
    /// <param name="topic">Temat</param>
    /// <param name="data">Payload</param>
    /// <returns>Nic, asynchroniczny void.</returns>
    public async Task Publish<T>(MercureMessage mercureMessage) where T : IMercureMessagePayload
    {
        //Działamy w środowisku Docker Compose więc adres Mercure jest zawsze taki sam, ale formalnie powinniśmy przekazać z configu.
        HttpRequestMessage requestMessage = new(HttpMethod.Post, Host + "/.well-known/mercure");

        //Token autoryzacyjny.
        requestMessage.Headers.Add("Authorization", "Bearer " + Token);

        T payload = (T) mercureMessage.Payload;

        //Musimy dopasować kontekst do wiadomości
        var body = JsonSerializer.Serialize(payload);

        var message = "topic=" + mercureMessage.Topic + "&data=" + HttpUtility.UrlEncode(body);

        if (mercureMessage.Id != null)
        {
            message = "id=" + mercureMessage.Id + "&" + message;
        }

        requestMessage.Content = new StringContent(message, Encoding.UTF8, new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded"));
        Logger.LogInformation("Sending message to Mercure: " + message);
        HttpResponseMessage response = await HttpClient.SendAsync(requestMessage);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            Logger.LogInformation("Wysłano do Mercure: " + body);
        }
        else
        {
            Logger.LogError("Nie udało się wysłać do Mercure: " + body);
        }
    }
}