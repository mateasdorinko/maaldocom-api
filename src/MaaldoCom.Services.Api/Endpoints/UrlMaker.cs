namespace MaaldoCom.Services.Api.Endpoints;

internal static class UrlMaker
{
    public static string GetMediaAlbumsUrl() => "/media-albums";
    public static string GetMediaAlbumUrl(Guid id) => $"{GetMediaAlbumsUrl()}/{id}";
    public static string GetMediaAlbumUrl(string urlFriendlyName) => $"{GetMediaAlbumsUrl()}/{urlFriendlyName}";
    public static string GetMediaUrl(Guid mediaAlbumId, Guid mediaId) => $"{GetMediaAlbumsUrl()}/{mediaAlbumId}/media/{mediaId}";

    public static string GetKnowledgeUrl() => "/knowledge";
    public static string GetKnowledgeUrl(Guid id) => $"{GetKnowledgeUrl()}/{id}";

    public static string GetTagsUrl() => "/tags";
    public static string GetTagUrl(Guid id) => $"{GetTagsUrl()}/{id}";
    public static string GetTagUrl(string name) => $"{GetTagsUrl()}/{name}";
}