using Microsoft.EntityFrameworkCore;

namespace MaaldoCom.Services.Application.Dtos.Validators;

public class CreateMediaAlbumValidator : AbstractValidator<MediaAlbumDto>
{
    private readonly IMaaldoComDbContext _maaldoComDbContext;

    public CreateMediaAlbumValidator(IMaaldoComDbContext maaldoComDbContext)
    {
        _maaldoComDbContext = maaldoComDbContext;

        RuleFor(dto => dto)
            .Must(IsUniqueAsync)
            .WithMessage("Media album already exists");
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(50)
            .WithMessage("Name must be 50 characters or less");
        RuleFor(dto => dto.UrlFriendlyName)
            .NotEmpty()
            .WithMessage("UrlFriendlyName is required")
            .MaximumLength(50)
            .WithMessage("UrlFriendlyName must be 50 characters or less");
        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(200)
            .WithMessage("Description must be 200 characters or less");
        RuleFor(dto => dto.Media)
            .NotNull()
            .WithMessage("Media list cannot be null")
            .NotEmpty()
            .WithMessage("Media list cannot be empty")
            .ForEach(x => x.SetValidator(new CreateMediaValidator()));
    }

    private bool IsUniqueAsync(MediaAlbumDto dto)
    {
        var results = _maaldoComDbContext.MediaAlbums
            .Where(ma => ma.Name!.ToLower() == dto.Name!.ToLower() || ma.UrlFriendlyName!.ToLower() == dto.UrlFriendlyName!.ToLower())
            .ToListAsync().Result;

        return results.Count == 0;
    }
}
