using Microsoft.EntityFrameworkCore;
using MaaldoCom.Api.Application.Commands.MediaAlbums;

namespace Tests.Unit.Application.Commands.MediaAlbums.CreateMediaCommandHandlerTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidCommand_CreatesEntityAndReturnsDto()
    {
        // arrange
        var dbContext = A.Fake<IMaaldoComDbContext>();
        var fakeMediaDbSet = A.Fake<DbSet<Media>>();
        var ct = CancellationToken.None;

        A.CallTo(() => dbContext.Media).Returns(fakeMediaDbSet);

        var media = new MediaDto { FileName = "photo.jpg", FileExtension = ".jpg", SizeInBytes = 1024 };
        var user = new ClaimsPrincipal();
        var command = new CreateMediaCommand(user, media);
        var handler = new CreateMediaCommandHandler(dbContext);

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.FileName.ShouldBe("photo.jpg");
        result.Value.FileExtension.ShouldBe(".jpg");
        A.CallTo(() => dbContext.SaveChangesAsync(user, ct, A<bool>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task HandleAsync_WithValidCommand_AddsEntityToDbSet()
    {
        // arrange
        var dbContext = A.Fake<IMaaldoComDbContext>();
        var fakeMediaDbSet = A.Fake<DbSet<Media>>();
        var ct = CancellationToken.None;

        A.CallTo(() => dbContext.Media).Returns(fakeMediaDbSet);

        var media = new MediaDto { FileName = "photo.jpg", FileExtension = ".jpg" };
        var user = new ClaimsPrincipal();
        var command = new CreateMediaCommand(user, media);
        var handler = new CreateMediaCommandHandler(dbContext);

        // act
        await handler.HandleAsync(command, ct);

        // assert
        A.CallTo(() => fakeMediaDbSet.AddAsync(A<Media>.That.Matches(m => m.FileName == "photo.jpg"), ct))
            .MustHaveHappenedOnceExactly();
    }
}
