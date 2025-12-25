namespace MaaldoCom.Services.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public Guid Uid { get; set; }
}