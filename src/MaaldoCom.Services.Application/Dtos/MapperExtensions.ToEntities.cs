using MaaldoCom.Services.Domain.Entities;

namespace MaaldoCom.Services.Application.Dtos;

public static partial class MapperExtensions
{
    public static IEnumerable<MediaAlbum> ToEntities(this IEnumerable<MediaAlbumDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }
    
    public static IEnumerable<Media> ToEntities(this IEnumerable<MediaDto> dtos)
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