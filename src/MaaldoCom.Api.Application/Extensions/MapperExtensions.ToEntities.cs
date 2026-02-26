using MaaldoCom.Api.Application.Dtos;

namespace MaaldoCom.Api.Application.Extensions;

public static partial class MapperExtensions
{
    public static IEnumerable<MediaAlbum> ToEntities(this IEnumerable<MediaAlbumDto> dtos, ClaimsPrincipal user)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }

    public static IEnumerable<Media> ToEntities(this IEnumerable<MediaDto> dtos, ClaimsPrincipal user)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }

    public static IEnumerable<Tag> ToEntities(this IEnumerable<TagDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }

    public static IEnumerable<Knowledge> ToEntities(this IEnumerable<KnowledgeDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }
}
