namespace Tests.Unit.Application.Errors.ValidationFailureErrorTests;

public class Ctor
{
    [Fact]
    public void Ctor_WithEntity_ArgsAreMappedCorrectly()
    {
        // arrange
        var mediaAlbum = new MediaAlbum { Name = "Test Album" };

        // act
        var error = new ValidationFailureError<MediaAlbum>(mediaAlbum);

        // assert
        error.Message.ShouldBe("MediaAlbum validation error(s).");
        error.Metadata["EntityType"].ShouldBe("MediaAlbum");
        error.Metadata["Entity"].ShouldBe(mediaAlbum);
        error.Reasons.ShouldBeEmpty();
    }

    [Fact]
    public void Ctor_WithNullEntity_UsesGenericTypeName()
    {
        // arrange
        MediaAlbum? entity = null;

        // act
        var error = new ValidationFailureError<MediaAlbum>(entity!);

        // assert
        error.Message.ShouldBe("MediaAlbum validation error(s).");
        error.Metadata["EntityType"].ShouldBe("MediaAlbum");
    }
}
