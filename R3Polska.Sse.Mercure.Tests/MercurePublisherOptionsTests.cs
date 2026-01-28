namespace R3Polska.Sse.Mercure.Tests;

public class MercurePublisherOptionsTests
{
    [Fact]
    public void MercurePublisherOptions_Host_IsRequired()
    {
        var options = new MercurePublisherOptions
        {
            Host = "http://localhost:3000",
            Token = "test-token"
        };
        Assert.Equal("http://localhost:3000", options.Host);
    }

    [Fact]
    public void MercurePublisherOptions_Token_IsRequired()
    {
        var options = new MercurePublisherOptions
        {
            Host = "http://localhost:3000",
            Token = "my-secret-token"
        };
        Assert.Equal("my-secret-token", options.Token);
    }

    [Fact]
    public void MercurePublisherOptions_Host_CanBeChanged()
    {
        var options = new MercurePublisherOptions
        {
            Host = "http://localhost:3000",
            Token = "token"
        };
        options.Host = "http://mercure:3000";
        Assert.Equal("http://mercure:3000", options.Host);
    }

    [Fact]
    public void MercurePublisherOptions_Token_CanBeChanged()
    {
        var options = new MercurePublisherOptions
        {
            Host = "http://localhost:3000",
            Token = "old-token"
        };
        options.Token = "new-token";
        Assert.Equal("new-token", options.Token);
    }

    [Theory]
    [InlineData("http://localhost:3000")]
    [InlineData("http://mercure:3000")]
    [InlineData("https://mercure.example.com")]
    [InlineData("http://192.168.1.100:8080")]
    public void MercurePublisherOptions_Host_AcceptsVariousUrls(string host)
    {
        var options = new MercurePublisherOptions
        {
            Host = host,
            Token = "token"
        };
        Assert.Equal(host, options.Host);
    }

    [Theory]
    [InlineData("simple-token")]
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIn0.Rq8IjqbeVpS")]
    [InlineData("")]
    public void MercurePublisherOptions_Token_AcceptsVariousFormats(string token)
    {
        var options = new MercurePublisherOptions
        {
            Host = "http://localhost:3000",
            Token = token
        };
        Assert.Equal(token, options.Token);
    }

    [Fact]
    public void MercurePublisherOptions_BothProperties_AreSetTogether()
    {
        var options = new MercurePublisherOptions
        {
            Host = "http://mercure:3000",
            Token = "bearer-token-123"
        };

        Assert.Equal("http://mercure:3000", options.Host);
        Assert.Equal("bearer-token-123", options.Token);
    }
}