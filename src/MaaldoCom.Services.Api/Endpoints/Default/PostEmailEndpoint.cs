using MaaldoCom.Services.Api.Endpoints.Default.Models;
using MaaldoCom.Services.Application.Commands.System;

namespace MaaldoCom.Services.Api.Endpoints.Default;

public class PostMailEndpoint : Endpoint<PostEmailRequest>
{
    public override void Configure()
    {
        Post("/emails");
        Description(x => x.WithName("PostEmail"));
        AllowAnonymous();
        //Options(x => x.ExcludeFromDescription());
        Permissions("write:emails");
    }

    public override async Task HandleAsync(PostEmailRequest req, CancellationToken ct)
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
