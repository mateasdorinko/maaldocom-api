namespace MaaldoCom.Services.Application.Commands.MediaAlbums;

public class CreateMediaCommand(ClaimsPrincipal user) : BaseCommand(user), ICommand<Result<MediaDto>>
{
    public required MediaDto Media { get; set; }
}

public class CreateMediaCommandHandler(IMaaldoComDbContext maaldoComDbContext)
    : BaseCommandHandler(maaldoComDbContext), ICommandHandler<CreateMediaCommand, Result<MediaDto>>
{
    public async Task<Result<MediaDto>> ExecuteAsync(CreateMediaCommand command, CancellationToken ct)
    {
        var validationResult = await new CreateMediaCommandValidator().ValidateAsync(command, ct);

        if (!validationResult.IsValid)
        {
            return Result.Fail<MediaDto>(validationResult.Errors.Select(IError (e) => new Error(e.ErrorMessage)).ToList());
        }

        var entity = command.Media.ToEntity(command.User);

        await MaaldoComDbContext.Media.AddAsync(entity, ct);
        await MaaldoComDbContext.SaveChangesAsync(command.User, ct);

        return Result.Ok(entity.ToDto());
    }
}

public class CreateMediaCommandValidator : AbstractValidator<CreateMediaCommand>
{
    public CreateMediaCommandValidator()
    {
        RuleFor(x => x.Media).SetValidator(new CreateMediaValidator());
    }
}
