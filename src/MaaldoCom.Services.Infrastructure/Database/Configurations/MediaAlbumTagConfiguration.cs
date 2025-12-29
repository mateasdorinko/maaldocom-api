using MaaldoCom.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaaldoCom.Services.Infrastructure.Database.Configurations;

public class MediaAlbumTagConfiguration : IEntityTypeConfiguration<MediaAlbumTag>
{
    public void Configure(EntityTypeBuilder<MediaAlbumTag> builder)
    {
        builder.ToTable("MediaAlbumTags");
        builder.HasKey(x => new { x.MediaAlbumId, x.TagId });

        builder.HasOne(x => x.MediaAlbum)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.MediaAlbumId);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.MediaAlbumTags)
            .HasForeignKey(x => x.MediaAlbumId);
    }
}