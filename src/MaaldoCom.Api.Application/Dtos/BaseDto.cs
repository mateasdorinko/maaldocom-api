namespace MaaldoCom.Api.Application.Dtos;

public abstract class BaseDto
{
    public Guid Id { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public bool Active { get; set; }
}