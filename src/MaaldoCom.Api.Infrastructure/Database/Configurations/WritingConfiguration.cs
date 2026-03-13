namespace MaaldoCom.Api.Infrastructure.Database.Configurations;

public class WritingConfiguration : IEntityTypeConfiguration<Writing>
{
    public void Configure(EntityTypeBuilder<Writing> builder)
    {
        builder.ConfigureBaseAuditableEntity();
        builder.ToTable("Writings");

        builder.HasIndex(x => x.Title).IsUnique();
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.Property(x => x.Title)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnOrder(6);
        builder.Property(x => x.Slug)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnOrder(7);
        builder.Property(x => x.Blurb)
            .HasMaxLength(250)
            .IsRequired()
            .HasColumnOrder(8);
        builder.Property(x => x.Body)
            .HasColumnType("nvarchar(MAX)")
            .HasColumnOrder(9);
    }
}
