using System.Diagnostics;
using System.Text;
using MaaldoCom.Services.Domain.MediaAlbums;

namespace MaaldoCom.Services.Infrastructure.MediaMetaData;

public class FFmpegMediaMetaDataCreator : IMediaMetaDataCreator
{
    public async Task CreateMediaMetaDataFilesAsync(string mediaAlbumFolderPath, Action<string> writeToConsole, CancellationToken cancellationToken)
    {
        var mediaAlbumFolder = new DirectoryInfo(mediaAlbumFolderPath);
        var mediaAlbumFiles = mediaAlbumFolder.GetFiles();

        mediaAlbumFolder.CreateSubdirectory(Constants.OriginalResFolderName);
        mediaAlbumFolder.CreateSubdirectory(Constants.ViewerFolderName);
        mediaAlbumFolder.CreateSubdirectory(Constants.ThumbnailFolderName);

        foreach (var file in mediaAlbumFiles)
        {
            MediaAlbumHelper.SanitizeFileName(file);

            if (MediaAlbumHelper.IsPic(file)) { await CreatePicMetaFilesAsync(file, mediaAlbumFolderPath); }
            if (MediaAlbumHelper.IsVid(file)) { await CreateVidMetaFileAsync(file, mediaAlbumFolderPath); }

            writeToConsole($"Processed: {file.FullName}");

            // move originals to original directory
            file.MoveTo($@"{file.DirectoryName}\{Constants.OriginalResFolderName}\{file.Name}");
        }
    }

    private static async Task CreatePicMetaFilesAsync(FileInfo file, string mediaAlbumFolderPath)
    {
        var thumbImageArgs = BuildFFmpegArguments(true, file.FullName,
            Constants.ThumbnailWidth,
            $@"{mediaAlbumFolderPath}\{Constants.ThumbnailFolderName}\{Constants.ThumbnailFolderName}-{file.Name}");

        var viewerImageArgs = BuildFFmpegArguments(true, file.FullName,
            Constants.ViewerWidth,
            $@"{mediaAlbumFolderPath}\{Constants.ViewerFolderName}\{Constants.ViewerFolderName}-{file.Name}");

        await CreateMetaFileAsync(thumbImageArgs);
        await CreateMetaFileAsync(viewerImageArgs);
    }

    private static async Task CreateVidMetaFileAsync(FileInfo file, string mediaAlbumFolderPath)
    {
        var newVidThumbnailPath = Path.ChangeExtension(file.Name, ".jpg");
        var thumbImageArgs = BuildFFmpegArguments(false, file.FullName,
            Constants.ThumbnailWidth,
            $@"{mediaAlbumFolderPath}\{Constants.ThumbnailFolderName}\{Constants.ThumbnailFolderName}-{newVidThumbnailPath}");

        await CreateMetaFileAsync(thumbImageArgs);
    }

    private static string BuildFFmpegArguments(bool inputFileIsPic, string fullyQualifiedInputFileName, int width, string fullyQualifiedOutputFileName)
    {
        var args = new StringBuilder();

        if (!inputFileIsPic) { args.Append($"-ss 00:00:05 "); }                                                     // vid file
        args.Append($"-i \"{fullyQualifiedInputFileName}\" ");                                                      // pic/vid file
        args.Append($"-hide_banner ");                                                                              // pic/vid file
        args.Append($"-loglevel error ");                                                                           // pic/vid file
        if (!inputFileIsPic) { args.Append($"-frames:v 1 "); }                                                      // video file
        if (!inputFileIsPic) { args.Append($"-vf thumbnail,scale={width}:{Constants.CalculatedImageHeight} "); }    // video file
        else { args.Append($"-vf scale={width}:{Constants.CalculatedImageHeight} "); }                              // pic file
        args.Append($"-q:v 2 ");                                                                                    // pic/vid file
        args.Append($"\"{fullyQualifiedOutputFileName}\"");                                                         // pic/vid file

        return args.ToString();
    }

    private static async Task CreateMetaFileAsync(string args)
    {
        var process = new Process();
        process.StartInfo.FileName = "ffmpeg";
        process.StartInfo.Arguments = args;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        process.Start();

        await process.WaitForExitAsync();
    }
}
