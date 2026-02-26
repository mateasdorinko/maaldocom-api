namespace MaaldoCom.Api.Infrastructure.Database.Configurations;

public class MediaTagConfiguration : IEntityTypeConfiguration<MediaTag>
{
    public void Configure(EntityTypeBuilder<MediaTag> builder)
    {
        builder.ToTable("MediaTags");
        builder.HasKey(x => new { x.MediaId, x.TagId });

        builder.HasOne(x => x.Media)
            .WithMany(x => x.MediaTags)
            .HasForeignKey(x => x.MediaId);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.MediaTags)
            .HasForeignKey(x => x.TagId);
    }
}