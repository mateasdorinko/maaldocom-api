namespace MaaldoCom.Api.Application.Blobs;

public interface IBlobsProvider
{
    Task<MediaDto?> GetBlobAsync(string containerName, string blobName, CancellationToken ct);
}
