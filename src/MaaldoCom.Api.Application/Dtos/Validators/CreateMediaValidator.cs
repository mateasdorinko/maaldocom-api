namespace MaaldoCom.Api.Application.Dtos.Validators;

public class CreateMediaValidator : AbstractValidator<MediaDto>
{
    public CreateMediaValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("Media fileName is required")
            .MaximumLength(50)
            .WithMessage("Media fileName must be 50 characters or less");
        RuleFor(x => x.FileExtension)
            .NotEmpty()
            .WithMessage("Media fileExtension is required")
            .MaximumLength(20)
            .WithMessage("Media fileExtension must be 20 characters or less");
        RuleFor(dto => dto.Tags)
            .ForEach(x => x.SetValidator(new TagValidator()));
    }
}
