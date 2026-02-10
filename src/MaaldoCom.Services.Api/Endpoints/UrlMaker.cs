namespace MaaldoCom.Services.Api.Endpoints;

internal static class UrlMaker
{
    public const string MediaAlbumsRoute = "/media-albums";

    public static string GetMediaAlbumUrl(Guid id) => GetMediaAlbumUrl(id.ToString());
    public static string GetMediaAlbumUrl(string idOrUrlFriendlyName) => $"{MediaAlbumsRoute}/{idOrUrlFriendlyName}";

    public static string GetHotShotsMediaAlbumUrl() => GetMediaAlbumUrl("hotshots");

    private static string GetMediaUrl(string mediaAlbumId, string mediaId) => $"{MediaAlbumsRoute}/{mediaAlbumId}/media/{mediaId}";
    public static string GetMediaUrl(Guid mediaAlbumId, Guid mediaId) => GetMediaUrl(mediaAlbumId.ToString(), mediaId.ToString());

    private static string GetMediaUrl(string mediaAlbumId, string mediaId, string mediaType) => $"{MediaAlbumsRoute}/{mediaAlbumId}/media/{mediaId}/{mediaType}";
    public static string GetMediaUrl(Guid mediaAlbumId, Guid mediaId, string mediaType) => GetMediaUrl(mediaAlbumId.ToString(), mediaId.ToString(), mediaType);

    public static string GetOriginalMediaUrl(string mediaAlbumId, string mediaId) => $"{GetMediaUrl(mediaAlbumId, mediaId)}/original";
    public static string GetOriginalMediaUrl(Guid mediaAlbumId, Guid mediaId) => $"{GetOriginalMediaUrl(mediaAlbumId.ToString(), mediaId.ToString())}";

    public static string GetViewerMediaUrl(string mediaAlbumId, string mediaId) => $"{GetMediaUrl(mediaAlbumId, mediaId)}/viewer";
    public static string GetViewerMediaUrl(Guid mediaAlbumId, Guid mediaId) => $"{GetViewerMediaUrl(mediaAlbumId.ToString(), mediaId.ToString())}";

    public static string GetThumbnailMediaUrl(string mediaAlbumId, string mediaId) => $"{GetMediaUrl(mediaAlbumId, mediaId)}/thumb";
    public static string GetThumbnailMediaUrl(Guid mediaAlbumId, Guid mediaId) => $"{GetThumbnailMediaUrl(mediaAlbumId.ToString(), mediaId.ToString())}";

    public const string KnowledgeRoute = "/knowledge";
    public static string GetKnowledgeUrl(Guid id) => $"{KnowledgeRoute}/{id}";
    public static string GetKnowledgeUrl(string idRouteParam) => $"{KnowledgeRoute}/{idRouteParam}";
    public static string GetRandomKnowledgeUrl() => $"{KnowledgeRoute}/random";

    public const string TagsRoute = "/tags";
    public static string GetTagUrl(Guid id) => GetTagUrl(id.ToString());
    public static string GetTagUrl(string idOrName) => $"{TagsRoute}/{idOrName}";

    public const string SystemRoute = "/system";
    public static string GetCacheRefreshUrl() => $"{SystemRoute}/cache-refreshes";
    public static string GetRuntimeInfoUrl() => $"{SystemRoute}/runtime-info";
}
