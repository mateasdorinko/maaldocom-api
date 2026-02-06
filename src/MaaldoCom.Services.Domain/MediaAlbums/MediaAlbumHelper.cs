namespace MaaldoCom.Services.Domain.MediaAlbums;

public static class MediaAlbumHelper
{
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
            .ToLower();

        // replace file
        file.MoveTo($"{file.DirectoryName}\\{newName}", true);
    }

    public static string GetProperNameFromFolder(string folderName)
    {
        var parts = folderName.Split(['-'], StringSplitOptions.RemoveEmptyEntries);

        var words = parts.Select(p =>
        {
            var lower = p.ToLowerInvariant();
            return char.ToUpperInvariant(lower[0]) + (lower.Length > 1 ? lower.Substring(1) : string.Empty);
        });

        return string.Join(" ", words);
    }

    public static string GetOriginalMetaFilePath(string mediaAlbumFolder, string originalFileName)
    {
        return $"{mediaAlbumFolder}/{Constants.OriginalResolutionFolderName}/{originalFileName}";
    }

    public static string GetViewerMetaFilePath(string mediaAlbumFolder, string originalFileName)
    {
        var currentExtension = Path.GetExtension(originalFileName);
        var viewerFile = vidExtensions.Contains(currentExtension, StringComparer.OrdinalIgnoreCase)
            ? $"{mediaAlbumFolder}/{Constants.OriginalResolutionFolderName}/{originalFileName}"                         // vid - viewer file is same as original
            : $"{mediaAlbumFolder}/{Constants.ViewerFolderName}/{Constants.ViewerFolderName}-{originalFileName}";       // pic - viewer file is in viewer folder

        return viewerFile;
    }

    public static string GetThumbnailMetaFilePath(string mediaAlbumFolder, string originalFileName)
    {
        var currentExtension = Path.GetExtension(originalFileName);
        var thumbNailFile = vidExtensions.Contains(currentExtension, StringComparer.OrdinalIgnoreCase)
            ? Path.ChangeExtension(originalFileName, ".jpg")                                               // vid - thumbnail is jpg of the video
            : originalFileName;                                                                                         // pic - thumbnail type is same as original
        var prefixedThumbNailFile = $"{mediaAlbumFolder}/{Constants.ThumbnailFolderName}/{Constants.ThumbnailFolderName}-{thumbNailFile}";

        return prefixedThumbNailFile;
    }
}
