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
        
        builder.HasIndex(e => e.Name).IsUnique();
        
        builder.Property(x => x.Name)
            .HasMaxLength(20)
            .IsRequired()
            .HasColumnOrder(1);
    }   
}