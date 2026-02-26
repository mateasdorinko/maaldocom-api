namespace MaaldoCom.Api.Application.Errors;

public class ValidationFailureError<TEntity> : IError where TEntity : BaseEntity
{
    public ValidationFailureError(TEntity entity)
    {
        var entityTypeName = entity?.GetType().Name ?? typeof(TEntity).Name;
        Message = $"{entityTypeName} validation error(s).";
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