using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Tests.Unit.Infrastructure.Blobs.AzureStorageBlobsProviderTests;

public sealed class GetBlobAsync
{
    private readonly BlobServiceClient _blobServiceClient = A.Fake<BlobServiceClient>();
    private readonly BlobContainerClient _containerClient = A.Fake<BlobContainerClient>();
    private readonly BlobClient _blobClient = A.Fake<BlobClient>();
    private readonly AzureStorageBlobsProvider _sut;

    public GetBlobAsync()
    {
        A.CallTo(() => _blobServiceClient.GetBlobContainerClient(A<string>._)).Returns(_containerClient);
        A.CallTo(() => _containerClient.GetBlobClient(A<string>._)).Returns(_blobClient);
        _sut = new AzureStorageBlobsProvider(_blobServiceClient);
    }

    [Fact]
    public async Task GetBlobAsync_WhenBlobDoesNotExist_ReturnsNull()
    {
        var existsResponse = A.Fake<Response<bool>>();
        A.CallTo(() => existsResponse.Value).Returns(false);
        A.CallTo(() => _blobClient.ExistsAsync(A<CancellationToken>._)).Returns(existsResponse);

        var result = await _sut.GetBlobAsync("container", "blob.jpg", TestContext.Current.CancellationToken);

        result.ShouldBeNull();
    }

    [Fact]
    public async Task GetBlobAsync_WhenBlobExists_ReturnsPopulatedMediaDto()
    {
        var existsResponse = A.Fake<Response<bool>>();
        A.CallTo(() => existsResponse.Value).Returns(true);
        A.CallTo(() => _blobClient.ExistsAsync(A<CancellationToken>._)).Returns(existsResponse);

        var stream = new MemoryStream([1, 2, 3]);
        A.CallTo(() => _blobClient.OpenReadAsync(A<bool>._, A<long>._, A<int?>._, A<CancellationToken>._)).Returns(stream);

        var properties = BlobsModelFactory.BlobProperties(contentType: "image/jpeg", contentLength: 1024);
        var propertiesResponse = Response.FromValue(properties, A.Fake<Response>());
        A.CallTo(() => _blobClient.GetPropertiesAsync(A<BlobRequestConditions>._, A<CancellationToken>._)).Returns(propertiesResponse);

        var result = await _sut.GetBlobAsync("container", "blob.jpg", TestContext.Current.CancellationToken);

        result.ShouldNotBeNull();
        result.FileName.ShouldBe("blob.jpg");
        result.ContentType.ShouldBe("image/jpeg");
        result.SizeInBytes.ShouldBe(1024);
    }
}
