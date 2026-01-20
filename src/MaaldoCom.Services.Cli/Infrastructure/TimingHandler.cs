using System.Diagnostics;

namespace MaaldoCom.Services.Cli.Infrastructure;

public sealed class TimingHandler(HttpMessageHandler innerHandler) : DelegatingHandler(innerHandler)
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var response = await base.SendAsync(request, cancellationToken);

        stopwatch.Stop();

        AnsiConsole.MarkupLine($"[grey]API call to[/] [blue]{request.RequestUri?.PathAndQuery}[/] [grey]completed in[/] [green]{stopwatch.ElapsedMilliseconds}ms[/]");
        AnsiConsole.WriteLine();

        return response;
    }
}
