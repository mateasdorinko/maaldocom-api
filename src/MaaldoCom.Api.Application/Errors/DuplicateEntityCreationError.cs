namespace MaaldoCom.Api.Application.Errors;

public class DuplicateEntityCreationError<TEntity> : IError where TEntity : BaseEntity
{
    public DuplicateEntityCreationError(TEntity entity)
    {
        var entityTypeName = entity?.GetType().Name ?? typeof(TEntity).Name;
        Message = $"{entityTypeName} '{entity}' already exists.";
        Metadata = new Dictionary<string, object>
        {
            { "EntityType", entityTypeName },
            { "Entity", entity! }
        };
    }

    public string Message { get; }
    public Dictionary<string, object> Metadata { get; }

    public List<IError> Reasons { get; } = [];
}