namespace MaaldoCom.Services.Api.Endpoints;

internal static class UrlMaker
{
    public const string MediaAlbumsRoute = "/media-albums";
    public static string GetMediaAlbumUrl(Guid id) => $"{MediaAlbumsRoute}/{id}";
    public static string GetMediaAlbumUrl(string urlFriendlyName) => $"{MediaAlbumsRoute}/{urlFriendlyName}";
    public static string GetMediaUrl(Guid mediaAlbumId, Guid mediaId) => $"{MediaAlbumsRoute}/{mediaAlbumId}/media/{mediaId}";

    public const string KnowledgeRoute = "/knowledge";
    public static string GetKnowledgeUrl(Guid id) => $"{KnowledgeRoute}/{id}";

    public const string TagsRoute = "/tags";
    public static string GetTagUrl(Guid id) => $"{TagsRoute}/{id}";
    public static string GetTagUrl(string name) => $"{TagsRoute}/{name}";
}