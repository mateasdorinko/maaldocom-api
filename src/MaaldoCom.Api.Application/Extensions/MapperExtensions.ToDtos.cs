namespace MaaldoCom.Api.Application.Extensions;

public static partial class MapperExtensions
{
    public static IEnumerable<MediaAlbumDto> ToDtos(this IEnumerable<MediaAlbum> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(e => e.ToDto()).ToList();
    }

    public static IEnumerable<MediaDto> ToDtos(this IEnumerable<Media> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(e => e.ToDto()).ToList();
    }

    public static IEnumerable<TagDto> ToDtos(this IEnumerable<Tag> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(e => e.ToDto()).ToList();
    }

    public static IEnumerable<KnowledgeDto> ToDtos(this IEnumerable<Knowledge> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(e => e.ToDto()).ToList();
    }
}
