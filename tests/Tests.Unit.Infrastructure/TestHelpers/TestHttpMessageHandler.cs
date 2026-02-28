namespace Tests.Unit.Infrastructure.TestHelpers;

internal sealed class TestHttpMessageHandler(HttpResponseMessage response) : HttpMessageHandler
{
    public List<HttpRequestMessage> Requests { get; } = [];

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Requests.Add(request);
        return Task.FromResult(response);
    }
}
