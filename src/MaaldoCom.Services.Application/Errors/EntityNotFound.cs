namespace MaaldoCom.Services.Application.Errors;

public class EntityNotFound(string entityType, string searchBy, object searchValue) : IError
{
    public string Message { get; } = $"{entityType} with {searchBy} '{searchValue}' was not found.";
    public Dictionary<string, object> Metadata { get; } = new()
    {
        { "EntityType", entityType },
        { "SearchBy", searchBy },
        { "SearchValue", searchValue }
    };

    public List<IError> Reasons { get; } = [];
}