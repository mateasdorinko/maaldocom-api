using MaaldoCom.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaaldoCom.Services.Infrastructure.Database.Configurations;

public class MediaAlbumTagConfiguration : IEntityTypeConfiguration<MediaAlbumTag>
{
    public void Configure(EntityTypeBuilder<MediaAlbumTag> builder)
    {
        builder.ConfigureBaseEntity();
        builder.ToTable("MediaAlbumTags");

        builder.HasOne(x => x.MediaAlbum)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.MediaAlbumId);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.MediaAlbumTags)
            .HasForeignKey(x => x.MediaAlbumId);
    }
}