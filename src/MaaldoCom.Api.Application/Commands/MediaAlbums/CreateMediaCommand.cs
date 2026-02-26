using MaaldoCom.Api.Application.Database;
using MaaldoCom.Api.Application.Dtos;
using MaaldoCom.Api.Application.Dtos.Validators;
using MaaldoCom.Api.Application.Messaging;

namespace MaaldoCom.Api.Application.Commands.MediaAlbums;

public sealed record CreateMediaCommand(ClaimsPrincipal User, MediaDto Media) : ICommand<MediaDto>;

internal sealed class CreateMediaCommandHandler(IMaaldoComDbContext maaldoComDbContext) : ICommandHandler<CreateMediaCommand, MediaDto>
{
    public async Task<Result<MediaDto>> HandleAsync(CreateMediaCommand command, CancellationToken ct)
    {
        var entity = command.Media.ToEntity();

        await maaldoComDbContext.Media.AddAsync(entity, ct);
        await maaldoComDbContext.SaveChangesAsync(command.User, ct);

        return Result.Ok(entity.ToDto());
    }
}

public class CreateMediaCommandValidator : AbstractValidator<CreateMediaCommand>
{
    public CreateMediaCommandValidator(IMaaldoComDbContext maaldoComDbContext)
    {
        RuleFor(x => x.Media).SetValidator(new CreateMediaValidator());
    }
}
