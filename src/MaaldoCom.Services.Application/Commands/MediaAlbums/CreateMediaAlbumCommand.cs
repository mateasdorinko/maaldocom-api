namespace MaaldoCom.Services.Application.Commands.MediaAlbums;

public class CreateMediaAlbumCommand(ClaimsPrincipal user, MediaAlbumDto dto) : BaseCommand(user), ICommand<Result<MediaAlbumDto>>
{
    public MediaAlbumDto MediaAlbum { get; set; } = dto;
}

public class CreateMediaAlbumCommandHandler(IMaaldoComDbContext maaldoComDbContext)
    : BaseCommandHandler(maaldoComDbContext), ICommandHandler<CreateMediaAlbumCommand, Result<MediaAlbumDto>>
{
    public async Task<Result<MediaAlbumDto>> ExecuteAsync(CreateMediaAlbumCommand command, CancellationToken ct)
    {
        var validationResult = await new CreateMediaAlbumCommandValidator(MaaldoComDbContext).ValidateAsync(command, ct);

        if (!validationResult.IsValid)
        {
            return Result.Fail<MediaAlbumDto>(validationResult.Errors.Select(IError (e) => new Error(e.ErrorMessage)).ToList());
        }

        command.MediaAlbum.Active = true;
        foreach (var media in command.MediaAlbum.Media) { media.Active = true; }

        var entity = command.MediaAlbum.ToEntity(command.User);

        await MaaldoComDbContext.MediaAlbums.AddAsync(entity, ct);
        await MaaldoComDbContext.SaveChangesAsync(command.User, ct);

        return Result.Ok(entity.ToDto());
    }
}

public class CreateMediaAlbumCommandValidator : AbstractValidator<CreateMediaAlbumCommand>
{
    public CreateMediaAlbumCommandValidator(IMaaldoComDbContext maaldoComDbContext)
    {
        RuleFor(x => x.MediaAlbum)
            .NotNull()
            .WithMessage("Media album cannot be null");
        RuleFor(x => x.MediaAlbum).SetValidator(new CreateMediaAlbumValidator(maaldoComDbContext));
    }
}
