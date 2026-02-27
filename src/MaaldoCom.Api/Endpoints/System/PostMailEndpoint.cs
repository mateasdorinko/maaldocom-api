using MaaldoCom.Api.Endpoints.System.Models;
using MaaldoCom.Api.Application.Commands.System;
using MaaldoCom.Api.Application.Email;

namespace MaaldoCom.Api.Endpoints.System;

public class PostMailEndpoint(Application.Messaging.ICommandHandler<SendEmailCommand, EmailResponse> handler) : Endpoint<PostMailRequest>
{
    public override void Configure()
    {
        Post(UrlMaker.GetMailUrl());
        Description(x => x.WithName("PostMail")
            .WithSummary("Creates a new email."));
        Permissions("write:emails");
    }

    public override async Task HandleAsync(PostMailRequest req, CancellationToken ct)
    {
        var command = new SendEmailCommand(req.From, req.Subject, req.Body);
        var result = await handler.HandleAsync(command, ct);

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
