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
        
        builder.Property(x => x.FileName)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnOrder(6);
        builder.Property(x => x.Description)
            .HasMaxLength(200)
            .HasColumnOrder(7);
        builder.Property(x => x.SizeInBytes)
            .IsRequired()
            .HasColumnOrder(8);
        builder.Property(x => x.FileExtension)
            .HasMaxLength(20)
            .IsRequired()
            .HasColumnOrder(9);
        
        builder.HasOne(x => x.MediaAlbum).WithMany(x => x.Media).HasForeignKey(x => x.MediaAlbumId).IsRequired();
    }
}