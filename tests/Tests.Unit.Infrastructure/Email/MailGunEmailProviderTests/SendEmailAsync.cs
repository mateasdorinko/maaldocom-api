using System.Net;
using MaaldoCom.Api.Infrastructure.Email;

namespace Tests.Unit.Infrastructure.Email.MailGunEmailProviderTests;

public sealed class SendEmailAsync
{
    private readonly TestHttpMessageHandler _handler;
    private readonly MailGunEmailProvider _sut;

    public SendEmailAsync()
    {
        _handler = new TestHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK));
        var httpClient = new HttpClient(_handler) { BaseAddress = new Uri("https://api.mailgun.net") };
        _sut = new MailGunEmailProvider(httpClient, "testdomain.com", "default@from.com", "default@to.com");
    }

    [Fact]
    public async Task SendEmailAsync_WhenCalled_PostsToCorrectUri()
    {
        await _sut.SendEmailAsync("to@test.com", "from@test.com", "Subject", "Body", TestContext.Current.CancellationToken);

        _handler.Requests.ShouldHaveSingleItem();
        _handler.Requests[0].RequestUri!.PathAndQuery.ShouldBe("/v3/testdomain.com/messages");
    }

    [Fact]
    public async Task SendEmailAsync_SuccessStatusCode_ReturnsIsSuccessStatusCodeTrue()
    {
        var result = await _sut.SendEmailAsync("to@test.com", "from@test.com", "Subject", "Body", TestContext.Current.CancellationToken);

        result.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Fact]
    public async Task SendEmailAsync_DefaultToOverload_UsesDefaultToAddress()
    {
        await _sut.SendEmailAsync("from@test.com", "Subject", "Body", TestContext.Current.CancellationToken);

        var content = await _handler.Requests[0].Content!.ReadAsStringAsync(TestContext.Current.CancellationToken);
        content.ShouldContain("to=default%40to.com");
    }

    [Fact]
    public async Task SendEmailAsync_DefaultFromAndToOverload_UsesDefaultAddresses()
    {
        await _sut.SendEmailAsync("Subject", "Body", TestContext.Current.CancellationToken);

        var content = await _handler.Requests[0].Content!.ReadAsStringAsync(TestContext.Current.CancellationToken);
        content.ShouldContain("from=default%40from.com");
        content.ShouldContain("to=default%40to.com");
    }
}
