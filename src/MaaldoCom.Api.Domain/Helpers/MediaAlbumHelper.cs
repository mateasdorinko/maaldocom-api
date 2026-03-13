namespace MaaldoCom.Api.Domain.Helpers;

public static class MediaAlbumHelper
{
    public const string OriginalResolutionFolderName = "original";
    public const string ViewerFolderName = "viewer";
    public const string ThumbnailFolderName = "thumb";

    public const int ViewerWidth = 1000;
    public const int ThumbnailWidth = 200;
    public const int CalculatedImageHeight = -1;

    private static readonly string[] picExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" };
    private static readonly string[] vidExtensions = [".mp4", ".mov", ".avi", ".mkv", ".wmv", ".flv",".webm"];

    public static bool IsPic(FileInfo file) => IsPic(file.Name);
    public static bool IsPic(string fileName) => picExtensions.Contains(Path.GetExtension(fileName), StringComparer.OrdinalIgnoreCase);

    public static bool IsVid(FileInfo file) => IsVid(file.Name);
    public static bool IsVid(string fileName) => vidExtensions.Contains(Path.GetExtension(fileName), StringComparer.OrdinalIgnoreCase);

    public static void SanitizeFileName(FileInfo file)
    {
        // update name
        var newName = file.Name
            .Replace("_", "-")
            .Replace(" ", "-")
            .ToLower();

        // replace file
        file.MoveTo(Path.Combine(file.DirectoryName!, newName), true);
    }

    public static string GetOriginalMetaFilePath(string mediaAlbumFolder, string originalFileName)
    {
        return $"{mediaAlbumFolder}/{OriginalResolutionFolderName}/{originalFileName}";
    }

    public static string GetViewerMetaFilePath(string mediaAlbumFolder, string originalFileName)
    {
        var currentExtension = Path.GetExtension(originalFileName);
        var viewerFile = vidExtensions.Contains(currentExtension, StringComparer.OrdinalIgnoreCase)
            ? $"{mediaAlbumFolder}/{OriginalResolutionFolderName}/{originalFileName}"                         // vid - viewer file is same as original
            : $"{mediaAlbumFolder}/{ViewerFolderName}/{ViewerFolderName}-{originalFileName}";       // pic - viewer file is in viewer folder

        return viewerFile;
    }

    public static string GetThumbnailMetaFilePath(string mediaAlbumFolder, string originalFileName)
    {
        var currentExtension = Path.GetExtension(originalFileName);
        var thumbNailFile = vidExtensions.Contains(currentExtension, StringComparer.OrdinalIgnoreCase)
            ? Path.ChangeExtension(originalFileName, ".jpg")                                               // vid - thumbnail is jpg of the video
            : originalFileName;                                                                                         // pic - thumbnail type is same as original
        var prefixedThumbNailFile = $"{mediaAlbumFolder}/{ThumbnailFolderName}/{ThumbnailFolderName}-{thumbNailFile}";

        return prefixedThumbNailFile;
    }
}
