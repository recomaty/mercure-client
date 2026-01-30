using System.Diagnostics;

namespace R3Polska.Sse.Mercure.FunctionalTests.Infrastructure;

public class DockerComposeFixture : IAsyncLifetime
{
    private readonly string _dockerComposeDir;
    private readonly string _projectName = "mercure-functional-tests";

    public string MercureHost => "http://localhost:3000";
    public string MercureToken { get; private set; } = string.Empty;

    public DockerComposeFixture()
    {
        // Find the docker-compose.yml directory
        var currentDir = Directory.GetCurrentDirectory();
        var searchDir = currentDir;

        while (searchDir != null)
        {
            var dockerComposePath = Path.Combine(searchDir, "docker-compose.yml");
            if (File.Exists(dockerComposePath))
            {
                _dockerComposeDir = searchDir;
                break;
            }

            // Also check in FunctionalTests subdirectory
            dockerComposePath = Path.Combine(searchDir, "R3Polska.Sse.Mercure.FunctionalTests", "docker-compose.yml");
            if (File.Exists(dockerComposePath))
            {
                _dockerComposeDir = Path.Combine(searchDir, "R3Polska.Sse.Mercure.FunctionalTests");
                break;
            }

            searchDir = Directory.GetParent(searchDir)?.FullName;
        }

        _dockerComposeDir ??= currentDir;
    }

    public async Task InitializeAsync()
    {
        // Generate JWT token for publisher
        MercureToken = GenerateJwtToken();

        // Start docker-compose (without --wait, we'll do our own health check)
        await RunDockerComposeAsync("up", "-d");

        // Wait for Mercure to be healthy
        await WaitForMercureAsync();
    }

    public async Task DisposeAsync()
    {
        // Stop and remove containers
        await RunDockerComposeAsync("down", "-v", "--remove-orphans");
    }

    public async Task StopMercureAsync()
    {
        await RunDockerComposeAsync("stop", "mercure");
        Console.WriteLine("[Docker] Mercure container stopped");
    }

    public async Task StartMercureAsync()
    {
        await RunDockerComposeAsync("start", "mercure");
        Console.WriteLine("[Docker] Mercure container starting...");
        await WaitForMercureAsync();
        Console.WriteLine("[Docker] Mercure container ready");
    }

    private string GenerateJwtToken()
    {
        // Simple JWT token for testing with the secret key from docker-compose
        // Header: {"alg":"HS256","typ":"JWT"}
        // Payload: {"mercure":{"publish":["*"],"subscribe":["*"]}}
        // Secret: !ChangeThisMercureHubJWTSecretKey!

        var header = Base64UrlEncode("{\"alg\":\"HS256\",\"typ\":\"JWT\"}");
        var payload = Base64UrlEncode("{\"mercure\":{\"publish\":[\"*\"],\"subscribe\":[\"*\"]}}");
        var signature = ComputeHmacSha256($"{header}.{payload}", "!ChangeThisMercureHubJWTSecretKey!");

        return $"{header}.{payload}.{signature}";
    }

    private static string Base64UrlEncode(string input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(bytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }

    private static string ComputeHmacSha256(string data, string key)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(key));
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(hash)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }

    private async Task WaitForMercureAsync(int maxRetries = 30, int delayMs = 1000)
    {
        using var client = new HttpClient();

        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                var response = await client.GetAsync($"{MercureHost}/.well-known/mercure");
                // Mercure returns 400 for GET without topic, 401 without auth - both mean it's running
                if (response.IsSuccessStatusCode ||
                    response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return;
                }
            }
            catch (HttpRequestException)
            {
                // Container not ready yet
            }

            await Task.Delay(delayMs);
        }

        throw new TimeoutException("Mercure container did not become healthy in time");
    }

    private async Task RunDockerComposeAsync(params string[] args)
    {
        var arguments = $"-p {_projectName} " + string.Join(" ", args);

        var startInfo = new ProcessStartInfo
        {
            FileName = "docker",
            Arguments = $"compose {arguments}",
            WorkingDirectory = _dockerComposeDir,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = startInfo };
        process.Start();

        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            throw new Exception($"Docker compose failed with exit code {process.ExitCode}. Error: {error}. Output: {output}");
        }
    }
}