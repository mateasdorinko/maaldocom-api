namespace MaaldoCom.Api.Infrastructure.Database.Configurations;

public class WritingTagConfiguration : IEntityTypeConfiguration<WritingTag>
{
    public void Configure(EntityTypeBuilder<WritingTag> builder)
    {
        builder.ToTable("WritingTags");
        builder.HasKey(x => new { x.WritingId, x.TagId });

        builder.HasOne(x => x.Writing)
            .WithMany(x => x.WritingTags)
            .HasForeignKey(x => x.WritingId);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.WritingTags)
            .HasForeignKey(x => x.TagId);
    }
}
