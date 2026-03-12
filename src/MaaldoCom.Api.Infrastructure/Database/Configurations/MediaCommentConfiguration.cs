namespace MaaldoCom.Api.Infrastructure.Database.Configurations;

public class MediaCommentConfiguration : IEntityTypeConfiguration<MediaComment>
{
    public void Configure(EntityTypeBuilder<MediaComment> builder)
    {
        builder.ToTable("MediaComments");
        builder.HasKey(x => new { x.MediaId, x.CommentId });

        builder.HasOne(x => x.Media)
            .WithMany(x => x.MediaComments)
            .HasForeignKey(x => x.MediaId);

        builder.HasOne(x => x.Comment)
            .WithMany(x => x.MediaComments)
            .HasForeignKey(x => x.CommentId);
    }
}
