using Shouldly;

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
        options.Host.ShouldBe("http://localhost:3000");
    }

    [Fact]
    public void MercurePublisherOptions_Token_IsRequired()
    {
        var options = new MercurePublisherOptions
        {
            Host = "http://localhost:3000",
            Token = "my-secret-token"
        };
        options.Token.ShouldBe("my-secret-token");
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
        options.Host.ShouldBe("http://mercure:3000");
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
        options.Token.ShouldBe("new-token");
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
        options.Host.ShouldBe(host);
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
        options.Token.ShouldBe(token);
    }

    [Fact]
    public void MercurePublisherOptions_BothProperties_AreSetTogether()
    {
        var options = new MercurePublisherOptions
        {
            Host = "http://mercure:3000",
            Token = "bearer-token-123"
        };

        options.Host.ShouldBe("http://mercure:3000");
        options.Token.ShouldBe("bearer-token-123");
    }
}