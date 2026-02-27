using MaaldoCom.Api.Application.Commands.MediaAlbums;
using Tests.Unit.Application.TestHelpers;

namespace Tests.Unit.Application.Commands.MediaAlbums.CreateMediaAlbumCommandValidatorTests;

public class Ctor
{
    private static IMaaldoComDbContext CreateDbContext(List<MediaAlbum>? mediaAlbums = null)
    {
        var dbContext = A.Fake<IMaaldoComDbContext>();
        A.CallTo(() => dbContext.MediaAlbums)
            .Returns(DbSetHelper.CreateFakeDbSet(mediaAlbums ?? []));
        return dbContext;
    }

    private static CreateMediaAlbumCommand ValidCommand() => new(
        new ClaimsPrincipal(),
        new MediaAlbumDto
        {
            Name = "My Album",
            UrlFriendlyName = "my-album",
            Description = "A test album",
            Media = [new MediaDto { FileName = "photo.jpg", FileExtension = ".jpg" }]
        });

    [Fact]
    public async Task Ctor_WithValidCommand_PassesValidation()
    {
        // arrange
        var validator = new CreateMediaAlbumCommandValidator(CreateDbContext());

        // act
        var result = await validator.ValidateAsync(ValidCommand(), TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public async Task Ctor_WithNullMediaAlbum_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaAlbumCommandValidator(CreateDbContext());
        var command = new CreateMediaAlbumCommand(new ClaimsPrincipal(), null!);

        // act
        var result = await validator.ValidateAsync(command, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media album cannot be null");
    }

    [Fact]
    public async Task Ctor_WithInvalidMediaAlbum_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaAlbumCommandValidator(CreateDbContext());
        var command = new CreateMediaAlbumCommand(
            new ClaimsPrincipal(),
            new MediaAlbumDto { Name = string.Empty, UrlFriendlyName = "slug", Description = "desc", Media = [new MediaDto { FileName = "f.jpg", FileExtension = ".jpg" }] });

        // act
        var result = await validator.ValidateAsync(command, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media album name is required");
    }
}
