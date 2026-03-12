namespace MaaldoCom.Api.Infrastructure.Database.Configurations;

public class WritingCommentConfiguration : IEntityTypeConfiguration<WritingComment>
{
    public void Configure(EntityTypeBuilder<WritingComment> builder)
    {
        builder.ToTable("WritingComments");
        builder.HasKey(x => new { x.WritingId, x.CommentId });

        builder.HasOne(x => x.Writing)
            .WithMany(x => x.WritingComments)
            .HasForeignKey(x => x.WritingId);

        builder.HasOne(x => x.Comment)
            .WithMany(x => x.WritingComments)
            .HasForeignKey(x => x.CommentId);
    }
}
