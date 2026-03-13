using Microsoft.EntityFrameworkCore;

namespace MaaldoCom.Api.Application.Dtos.Validators;

public class CreateMediaAlbumValidator : AbstractValidator<MediaAlbumDto>
{
    private readonly IMaaldoComDbContext _maaldoComDbContext;

    public CreateMediaAlbumValidator(IMaaldoComDbContext maaldoComDbContext)
    {
        _maaldoComDbContext = maaldoComDbContext;

        RuleFor(dto => dto)
            .Must(IsUniqueAsync)
            .WithMessage("Media album already exists")
            .When(dto => !string.IsNullOrEmpty(dto.Name) && !string.IsNullOrEmpty(dto.Slug));
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Media album name is required")
            .MaximumLength(50)
            .WithMessage("Media album name must be 50 characters or less");
        RuleFor(dto => dto.Slug)
            .NotEmpty()
            .WithMessage("Media album slug is required")
            .MaximumLength(50)
            .WithMessage("Media album slug must be 50 characters or less");
        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("Media album description is required")
            .MaximumLength(200)
            .WithMessage("Media album description must be 200 characters or less");
        RuleFor(dto => dto.Media)
            .NotNull()
            .WithMessage("Media album media list cannot be null")
            .NotEmpty()
            .WithMessage("Media album media list cannot be empty")
            .ForEach(x => x.SetValidator(new CreateMediaValidator()));
        RuleFor(dto => dto.Tags)
            .ForEach(x => x.SetValidator(new TagValidator()));
    }

    private bool IsUniqueAsync(MediaAlbumDto dto)
    {
        var results = _maaldoComDbContext.MediaAlbums
            .Where(ma => ma.Name!.ToLower() == dto.Name!.ToLower() || ma.Slug!.ToLower() == dto.Slug!.ToLower())
            .ToListAsync().Result;

        return results.Count == 0;
    }
}
