using Azure.Storage.Blobs;

namespace MaaldoCom.Api.Infrastructure.Blobs;

public class AzureStorageBlobsProvider(BlobServiceClient blobServiceClient) : IBlobsProvider
{
    private readonly BlobServiceClient _blobServiceClient = blobServiceClient;

    public async Task<MediaDto?> GetBlobAsync(string containerName, string blobName, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        if (!await blobClient.ExistsAsync(ct)) { return null; }

        var stream = await blobClient.OpenReadAsync(cancellationToken: ct);
        var properties = await blobClient.GetPropertiesAsync(cancellationToken: ct);

        return new MediaDto
        {
            Stream = stream,
            FileName = blobName,
            ContentType = properties.Value.ContentType ?? "application/octet-stream",
            SizeInBytes = properties.Value.ContentLength
        };
    }
}
