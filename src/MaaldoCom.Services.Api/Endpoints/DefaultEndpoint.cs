namespace MaaldoCom.Services.Api.Endpoints;

public class DefaultEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
        Options(x => x.ExcludeFromDescription());
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        const string htmlContent = """
                                           <!DOCTYPE html>
                                           <html>
                                           <head>
                                               <title>api.maaldo.com</title>
                                           </head>
                                           <body>
                                               <h1>hi...</h1>
                                           </body>
                                           </html>
                                   """;

        await Send.StringAsync(htmlContent, contentType: "text/html", cancellation: ct);
    }
}