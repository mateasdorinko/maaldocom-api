namespace Tests.Unit.Application.Errors.DuplicateEntityCreationErrorTests;

public class Ctor
{
    [Fact]
    public void Ctor_Instantiated_ArgsAreMappedCorrectly()
    {
        // arrange
        var mediaAlbum = new MediaAlbum
        {
            Name = "Test Media Album",
            Description = "A media album for testing purposes",
            UrlFriendlyName = "test-media-album"
        };

        object entity = mediaAlbum.Name;

        // assert
        var error = new DuplicateEntityCreationError<MediaAlbum>(mediaAlbum);

        // act
        error.Metadata["EntityType"].ShouldBe("MediaAlbum");
        error.Metadata["Entity"].ToString().ShouldBe("Test Media Album");
    }
}