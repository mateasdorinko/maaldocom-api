using MaaldoCom.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaaldoCom.Services.Infrastructure.Database.Configurations;

public class KnowledgeConfiguration : IEntityTypeConfiguration<Knowledge>
{
    public void Configure(EntityTypeBuilder<Knowledge> builder)
    {
        builder.ConfigureBaseEntity();
        builder.ToTable("Knowledge");
        
        builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Quote).HasMaxLength(200).IsRequired();
        
    }
}