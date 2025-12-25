using MaaldoCom.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaaldoCom.Services.Infrastructure.Extensions;

public static class EntityConfigurationExtensions
{
    public static void ConfigureBaseEntity<TBaseEntity>(this EntityTypeBuilder<TBaseEntity> builder)
        where TBaseEntity : BaseEntity
    {
        builder.Property(e => e.Uid).HasDefaultValueSql("newsequentialid()");
    }
    
    public static void ConfigureBaseAuditableEntity<TBaseAuditableEntity>(
        this EntityTypeBuilder<TBaseAuditableEntity> builder) where TBaseAuditableEntity : BaseAuditableEntity
    {
        ConfigureBaseEntity(builder);

        builder.Property(e => e.CreatedBy).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Created).HasColumnType("datetime").IsRequired();
        builder.Property(e => e.LastModified).HasColumnType("datetime").IsRequired();
        builder.Property(e => e.LastModifiedBy).HasMaxLength(50).IsRequired();
    }
}