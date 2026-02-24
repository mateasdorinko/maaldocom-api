namespace MaaldoCom.Services.Application.Commands.MediaAlbums;

public class CreateMediaCommand(ClaimsPrincipal user) : ICommand<Result<MediaDto>>
{
    public ClaimsPrincipal User { get; } = user;
    public required MediaDto Media { get; set; }
}

public class CreateMediaCommandHandler(IMaaldoComDbContext maaldoComDbContext) : ICommandHandler<CreateMediaCommand, Result<MediaDto>>
{
    public async Task<Result<MediaDto>> ExecuteAsync(CreateMediaCommand command, CancellationToken ct)
    {
        var validationResult = await new CreateMediaCommandValidator(maaldoComDbContext).ValidateAsync(command, ct);

        if (!validationResult.IsValid)
        {
            return Result.Fail<MediaDto>(validationResult.Errors.Select(IError (e) => new Error(e.ErrorMessage)).ToList());
        }

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
