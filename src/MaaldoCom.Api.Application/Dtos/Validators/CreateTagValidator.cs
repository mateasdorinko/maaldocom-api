using Microsoft.EntityFrameworkCore;

namespace MaaldoCom.Api.Application.Dtos.Validators;

public class CreateTagValidator : AbstractValidator<TagDto>
{
    private readonly IMaaldoComDbContext _maaldoComDbContext;

    public CreateTagValidator(IMaaldoComDbContext maaldoComDbContext)
    {
        _maaldoComDbContext = maaldoComDbContext;

        RuleFor(dto => dto)
            .Must(IsUniqueAsync)
            .WithMessage("Tag already exists");
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Tag name is required")
            .MaximumLength(20)
            .WithMessage("Tag name must be 20 characters or less");
    }

    private bool IsUniqueAsync(TagDto dto)
    {
        var results = _maaldoComDbContext.Tags
            .Where(t => t.Name!.ToLower() == dto.Name!.ToLower())
            .ToListAsync().Result;

        return results.Count == 0;
    }
}
