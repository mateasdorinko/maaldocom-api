using MaaldoCom.Services.Api.Endpoints.System.Models;
using MaaldoCom.Services.Application.Commands.System;

namespace MaaldoCom.Services.Api.Endpoints.System;

public class PostMailEndpoint : Endpoint<PostMailRequest>
{
    public override void Configure()
    {
        Post(UrlMaker.GetMailUrl());
        Description(x => x.WithName("PostMail")
            .WithSummary("Creates a new email."));
        //AllowAnonymous();
        //Options(x => x.ExcludeFromDescription());
        Permissions("write:emails");
    }

    public override async Task HandleAsync(PostMailRequest req, CancellationToken ct)
    {
        var result = await new SendEmailCommand(User, req.From, req.Subject, req.Body).ExecuteAsync(ct);

        await result.Match(
            onSuccess: _ => Send.CreatedAtAsync<PostMailEndpoint>(cancellation: ct),
            onFailure: errors =>
            {
                foreach (var error in errors) { AddError(error.Message); }
                return Send.ErrorsAsync(cancellation: ct);
            }
        );
    }
}
