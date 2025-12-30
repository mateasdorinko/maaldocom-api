using MaaldoCom.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaaldoCom.Services.Infrastructure.Database.Configurations;

public static class EntityConfigurationExtensions
{
    public static void ConfigureBaseEntity<TBaseEntity>(this EntityTypeBuilder<TBaseEntity> builder) 
        where TBaseEntity : BaseEntity
    {
        builder.Property(e => e.Id)
            .HasDefaultValueSql("newsequentialid()")
            .HasColumnOrder(0);
    }
    
    public static void ConfigureBaseAuditableEntity<TBaseAuditableEntity>(this EntityTypeBuilder<TBaseAuditableEntity> builder)
        where TBaseAuditableEntity : BaseAuditableEntity
    {
        builder.Property(e => e.Id)
            .HasDefaultValueSql("newsequentialid()")
            .HasColumnOrder(0);
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnOrder(1);
        builder.Property(e => e.Created)
            .HasColumnType("datetime2")
            .IsRequired()
            .HasColumnOrder(2);
        builder.Property(e => e.LastModified)
            .HasColumnType("datetime2")
            .HasColumnOrder(3);
        builder.Property(e => e.LastModifiedBy)
            .HasMaxLength(50)
            .HasColumnOrder(4);
        builder.Property(e => e.Active)
            .HasColumnOrder(5);
    }
}