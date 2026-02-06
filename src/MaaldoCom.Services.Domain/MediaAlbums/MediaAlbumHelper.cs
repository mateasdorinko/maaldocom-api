namespace MaaldoCom.Services.Domain.MediaAlbums;

public static class MediaAlbumHelper
{
    private static readonly string[] picExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" };
    private static readonly string[] vidExtensions = [".mp4", ".mov", ".avi", ".mkv", ".wmv", ".flv",".webm"];

    public static bool IsPic(FileInfo file)
        => picExtensions.Contains(file.Extension, StringComparer.OrdinalIgnoreCase);

    public static bool IsPic(string fileName)
        => picExtensions.Contains(Path.GetExtension(fileName), StringComparer.OrdinalIgnoreCase);

    public static bool IsVid(FileInfo file)
        => vidExtensions.Contains(file.Extension, StringComparer.OrdinalIgnoreCase);

    public static bool IsVid(string fileName)
        => vidExtensions.Contains(Path.GetExtension(fileName), StringComparer.OrdinalIgnoreCase);

    public static void SanitizeFileName(FileInfo file)
    {
        // update name
        var newName = file.Name
            .Replace("_", "-")
            .ToLower();

        // replace file
        file.MoveTo($"{file.DirectoryName}\\{newName}", true);
    }

    public static string GetNameFromFolder(string folderName)
    {
        var parts = folderName.Split(['-'], StringSplitOptions.RemoveEmptyEntries);

        var words = parts.Select(p =>
        {
            var lower = p.ToLowerInvariant();
            return char.ToUpperInvariant(lower[0]) + (lower.Length > 1 ? lower.Substring(1) : string.Empty);
        });

        return string.Join(" ", words);
    }

    // this is crap... refactor at some point
    public static string GetThumbnailMetaFile(string originalFileName)
    {
        var currentExtension = Path.GetExtension(originalFileName);
        var thumbNailFile = vidExtensions.Contains(currentExtension, StringComparer.OrdinalIgnoreCase)
            ? Path.ChangeExtension(originalFileName, ".jpg")
            : originalFileName;
        var prefixedThumbNailFile = $"{Constants.ThumbnailFolderName}-{thumbNailFile}";

        return prefixedThumbNailFile;
    }
}
