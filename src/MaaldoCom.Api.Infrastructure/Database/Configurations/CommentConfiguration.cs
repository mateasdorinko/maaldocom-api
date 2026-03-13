namespace MaaldoCom.Api.Infrastructure.Database.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ConfigureBaseEntity();
        builder.ToTable("Comments");

        builder.Property(x => x.Author)
            .HasMaxLength(20)
            .IsRequired()
            .HasColumnOrder(1);
        builder.Property(x => x.Body)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnOrder(2);
    }
}
