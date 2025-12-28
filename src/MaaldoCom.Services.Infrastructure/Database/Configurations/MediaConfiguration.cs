using MaaldoCom.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaaldoCom.Services.Infrastructure.Database.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ConfigureBaseAuditableEntity();
        builder.ToTable("Media");
        
        builder.Property(x => x.FileName).IsUnicode(false).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Description).IsUnicode(false).HasMaxLength(200); 
        builder.Property(x => x.SizeInBytes).IsRequired();
        builder.Property(x => x.FileExtension).IsUnicode(false).HasMaxLength(20).IsRequired();
        
        builder.HasOne(x => x.MediaAlbum).WithMany(x => x.Media).HasForeignKey(x => x.MediaAlbumId).IsRequired();
        builder.HasMany(x => x.Tags).WithMany(x => x.Media);
    }
}