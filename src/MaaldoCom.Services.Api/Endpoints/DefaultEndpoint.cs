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
                                   <html lang="">
                                     <head>
                                         <title>api.maaldo.com</title>
                                         <style>
                                             .container { display: flex; flex-direction: column; justify-content: flex-end; align-items: center; min-height: 85vh; }
                                             .bottom-center-div { width: 50%; padding: 1em; text-align: center; font-size: x-small; }
                                         </style>
                                     </head>
                                     <body>
                                         <h1>hello...</h1>
                                         <div class="container"> 
                                             <div class="bottom-center-div">
                                                 v2025.12.30b
                                             </div>
                                         </div>
                                     </body>
                                   </html>
                                   """;

        await Send.StringAsync(htmlContent, contentType: "text/html", cancellation: ct);
    }
}