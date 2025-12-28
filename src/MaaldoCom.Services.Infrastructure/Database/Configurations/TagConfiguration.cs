using MaaldoCom.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaaldoCom.Services.Infrastructure.Database.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ConfigureBaseEntity();
        builder.ToTable("Tags");
        
        builder.Property(t => t.Name).HasMaxLength(50).IsUnicode(false).IsRequired();

        builder.HasMany(x => x.MediaAlbums).WithMany(x => x.Tags); 
        builder.HasMany(x => x.Media).WithMany(x => x.Tags);
    }
}