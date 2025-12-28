using MaaldoCom.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaaldoCom.Services.Infrastructure.Database.Configurations;

public class MediaAlbumConfiguration : IEntityTypeConfiguration<MediaAlbum>
{
    public void Configure(EntityTypeBuilder<MediaAlbum> builder)
    {
        builder.ConfigureBaseAuditableEntity();
        builder.ToTable("MediaAlbums");
        
        builder.Property(x => x.Name).IsUnicode(false).HasMaxLength(50).IsRequired();
        builder.Property(x => x.UrlFriendlyName).IsUnicode(false).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Description).IsUnicode(false).HasMaxLength(200);
        
        builder.HasMany(x => x.Media).WithOne(x => x.MediaAlbum).HasForeignKey(x => x.MediaAlbumId);
        builder.HasMany(x => x.Tags).WithMany(x => x.MediaAlbums);
    }
}