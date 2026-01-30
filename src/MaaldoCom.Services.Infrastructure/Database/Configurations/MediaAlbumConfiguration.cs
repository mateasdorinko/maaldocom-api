namespace MaaldoCom.Services.Infrastructure.Database.Configurations;

public class MediaAlbumConfiguration : IEntityTypeConfiguration<MediaAlbum>
{
    public void Configure(EntityTypeBuilder<MediaAlbum> builder)
    {
        builder.ConfigureBaseAuditableEntity();
        builder.ToTable("MediaAlbums");
        
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.UrlFriendlyName).IsUnique();
        
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnOrder(6);
        builder.Property(x => x.UrlFriendlyName)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnOrder(7);
        builder.Property(x => x.Description)
            .HasMaxLength(200)
            .HasColumnOrder(8);
        
        builder.HasMany(x => x.Media).WithOne(x => x.MediaAlbum).HasForeignKey(x => x.MediaAlbumId);
    }
}