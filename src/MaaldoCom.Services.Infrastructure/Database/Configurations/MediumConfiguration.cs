using MaaldoCom.Services.Domain.Entities;
using MaaldoCom.Services.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaaldoCom.Services.Infrastructure.Database.Configurations;

public class MediumConfiguration : IEntityTypeConfiguration<Medium>
{
    public void Configure(EntityTypeBuilder<Medium> builder)
    {
        builder.ConfigureBaseAuditableEntity();

        builder.ToTable("Media");
        
        builder.Property(x => x.FileName).HasMaxLength(40).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(200);
        builder.Property(x => x.SizeInBytes).IsRequired();
        builder.Property(x => x.FileExtension).HasMaxLength(10).IsRequired();
        
        builder.HasOne(x => x.MediaAlbum).WithMany(x => x.Media).HasForeignKey(x => x.MediaAlbumId).IsRequired();
        builder.HasMany(x => x.Tags).WithMany(x => x.Media);
    }
}