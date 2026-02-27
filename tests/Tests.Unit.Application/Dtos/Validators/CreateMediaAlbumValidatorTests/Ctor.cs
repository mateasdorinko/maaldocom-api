using MaaldoCom.Api.Application.Dtos.Validators;
using Tests.Unit.Application.TestHelpers;

namespace Tests.Unit.Application.Dtos.Validators.CreateMediaAlbumValidatorTests;

public class Ctor
{
    private static IMaaldoComDbContext CreateDbContext(List<MediaAlbum>? mediaAlbums = null)
    {
        var dbContext = A.Fake<IMaaldoComDbContext>();
        A.CallTo(() => dbContext.MediaAlbums)
            .Returns(DbSetHelper.CreateFakeDbSet(mediaAlbums ?? []));
        return dbContext;
    }

    private static MediaAlbumDto ValidAlbumDto() => new()
    {
        Name = "My Album",
        UrlFriendlyName = "my-album",
        Description = "A test album",
        Media = [new MediaDto { FileName = "photo.jpg", FileExtension = ".jpg" }]
    };

    [Fact]
    public async Task Ctor_WithValidAlbum_PassesValidation()
    {
        // arrange
        var validator = new CreateMediaAlbumValidator(CreateDbContext());

        // act
        var result = await validator.ValidateAsync(ValidAlbumDto(), TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public async Task Ctor_WithDuplicateName_FailsValidation()
    {
        // arrange
        var existing = new List<MediaAlbum> { new() { Name = "My Album", UrlFriendlyName = "other-album" } };
        var validator = new CreateMediaAlbumValidator(CreateDbContext(existing));
        var dto = ValidAlbumDto();
        dto.Name = "My Album";

        // act
        var result = await validator.ValidateAsync(dto, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media album already exists");
    }

    [Fact]
    public async Task Ctor_WithEmptyName_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaAlbumValidator(CreateDbContext());
        var dto = ValidAlbumDto();
        dto.Name = string.Empty;

        // act
        var result = await validator.ValidateAsync(dto, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media album name is required");
    }

    [Fact]
    public async Task Ctor_WithNameExceedingMaxLength_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaAlbumValidator(CreateDbContext());
        var dto = ValidAlbumDto();
        dto.Name = new string('a', 51);

        // act
        var result = await validator.ValidateAsync(dto, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media album name must be 50 characters or less");
    }

    [Fact]
    public async Task Ctor_WithEmptyUrlFriendlyName_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaAlbumValidator(CreateDbContext());
        var dto = ValidAlbumDto();
        dto.UrlFriendlyName = string.Empty;

        // act
        var result = await validator.ValidateAsync(dto, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media album urlFriendlyName is required");
    }

    [Fact]
    public async Task Ctor_WithEmptyDescription_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaAlbumValidator(CreateDbContext());
        var dto = ValidAlbumDto();
        dto.Description = string.Empty;

        // act
        var result = await validator.ValidateAsync(dto, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media album description is required");
    }

    [Fact]
    public async Task Ctor_WithEmptyMediaList_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaAlbumValidator(CreateDbContext());
        var dto = ValidAlbumDto();
        dto.Media = [];

        // act
        var result = await validator.ValidateAsync(dto, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media album media list cannot be empty");
    }
}
