namespace MaaldoCom.Services.Application.Dtos;

public abstract class BaseDto
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public bool Active { get; set; }
}