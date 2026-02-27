namespace Tests.Unit.Application.Errors.BlobNotFoundErrorTests;

public class Ctor
{
    [Fact]
    public void Ctor_Instantiated_ArgsAreMappedCorrectly()
    {
        // arrange
        const string containerName = "media-albums";
        const string blobName = "album-1/original/photo.jpg";

        // act
        var error = new BlobNotFoundError(containerName, blobName);

        // assert
        error.Message.ShouldBe($"Blob {containerName}/{blobName} was not found.");
        error.Metadata["ContainerName"].ShouldBe(containerName);
        error.Metadata["BlobName"].ShouldBe(blobName);
        error.Reasons.ShouldBeEmpty();
    }
}
