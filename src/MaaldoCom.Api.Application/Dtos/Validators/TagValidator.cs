namespace MaaldoCom.Api.Application.Dtos.Validators;

public class TagValidator : AbstractValidator<TagDto>
{
    public TagValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Tag name cannot be empty")
            .MaximumLength(20)
            .WithMessage("Tag name must be 20 characters or less");
    }
}
