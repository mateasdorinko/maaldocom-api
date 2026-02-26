namespace MaaldoCom.Api.Application.Errors;

public class BlobNotFoundError(string containerName, string blobName) : IError
{
    public string Message { get; } = $"Blob {containerName}/{blobName} was not found.";
    public Dictionary<string, object> Metadata { get; } = new()
    {
        { "ContainerName", containerName },
        { "BlobName", blobName }
    };

    public List<IError> Reasons { get; } = [];
}
