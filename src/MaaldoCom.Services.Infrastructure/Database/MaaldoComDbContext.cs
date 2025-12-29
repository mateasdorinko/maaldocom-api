using System.Security.Claims;
using MaaldoCom.Services.Application.Interfaces;
using MaaldoCom.Services.Domain.Entities;
using MaaldoCom.Services.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MaaldoCom.Services.Infrastructure.Database;

public class MaaldoComDbContext(DbContextOptions<MaaldoComDbContext> options) : DbContext(options), IMaaldoComDbContext
{
    public DbSet<MediaAlbum> MediaAlbums { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<Knowledge> Knowledge { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public async Task<int> SaveChangesAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default, bool audit = true)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = user?.GetUserId() ?? entry.Entity.CreatedBy;
                    entry.Entity.Created = now;
                    entry.Entity.LastModified = now;
                    entry.Entity.LastModifiedBy = user?.GetUserId() ?? entry.Entity.CreatedBy;
                    break;
                case EntityState.Modified:
                    if (audit)
                    {
                        entry.Entity.LastModifiedBy = user?.GetUserId() ?? entry.Entity.LastModifiedBy;
                        entry.Entity.LastModified = now;
                    }

                    break;
                case EntityState.Deleted:
                    if (audit)
                    {
                        entry.Entity.LastModifiedBy = user?.GetUserId() ?? entry.Entity.LastModifiedBy;
                        entry.Entity.LastModified = now;
                        entry.State = EntityState.Modified;
                    }

                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public void DisableChangeTracking() => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    public void EnableChangeTracking() => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MaaldoComDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}