namespace MaaldoCom.Api.Infrastructure.Database.Configurations;

public class MediaAlbumCommentConfiguration : IEntityTypeConfiguration<MediaAlbumComment>
{
    public void Configure(EntityTypeBuilder<MediaAlbumComment> builder)
    {
        builder.ToTable("MediaAlbumComments");
        builder.HasKey(x => new { x.MediaAlbumId, x.CommentId });

        builder.HasOne(x => x.MediaAlbum)
            .WithMany(x => x.MediaAlbumComments)
            .HasForeignKey(x => x.MediaAlbumId);

        builder.HasOne(x => x.Comment)
            .WithMany(x => x.MediaAlbumComments)
            .HasForeignKey(x => x.CommentId);
    }
}
