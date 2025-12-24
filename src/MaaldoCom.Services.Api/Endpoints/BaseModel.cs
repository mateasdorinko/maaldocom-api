namespace MaaldoCom.Services.Api.Endpoints;

public abstract class BaseModel
{
    public Guid Guid { get; set; }
    public DateTime Created { get; set; }
    public bool Active { get; set; }
}