namespace MaaldoCom.Api.Domain.Entities;

public class Knowledge : BaseEntity
{
    public string? Title { get; set; }
    public string? Quote { get; set; }

    public override string ToString() => $"{Title}:{Quote}";
}