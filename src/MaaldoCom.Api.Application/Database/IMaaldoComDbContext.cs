using Microsoft.EntityFrameworkCore;

namespace MaaldoCom.Api.Application.Database;

public interface IMaaldoComDbContext
{
    DbSet<MediaAlbum> MediaAlbums { get; set; }
    DbSet<Media> Media { get; set; }
    DbSet<Knowledge> Knowledge { get; set; }
    DbSet<Writing> Writings { get; set; }
    DbSet<Tag> Tags { get; set; }
    DbSet<Comment> Comments { get; set; }

    Task<int> SaveChangesAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default, bool audit = true);

    void DisableChangeTracking();
    void EnableChangeTracking();
}
