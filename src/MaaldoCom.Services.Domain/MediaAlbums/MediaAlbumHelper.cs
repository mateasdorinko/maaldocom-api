namespace MaaldoCom.Services.Domain.MediaAlbums;

public static class MediaAlbumHelper
{
    private static readonly string[] picExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" };
    private static readonly string[] vidExtensions = [".mp4", ".mov", ".avi", ".mkv", ".wmv", ".flv",".webm"];

    public static bool IsPic(FileInfo file)
        => picExtensions.Contains(file.Extension, StringComparer.OrdinalIgnoreCase);

    public static bool IsVid(FileInfo file)
        => vidExtensions.Contains(file.Extension, StringComparer.OrdinalIgnoreCase);

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

    public static string GetMetaFileExtension(string fileName)
    {
        var currentExtension = Path.GetExtension(fileName);

        return !picExtensions.Contains(currentExtension, StringComparer.OrdinalIgnoreCase) ?
            Path.ChangeExtension(fileName, ".jpg") :
            fileName;
    }
}
