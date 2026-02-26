using MaaldoCom.Api.Application.Queries;

namespace MaaldoCom.Api.Application.Errors;

public class EntityNotFoundError(string entityType, SearchBy searchBy, object searchValue) : IError
{
    public string Message { get; } = $"{entityType} with {nameof(searchBy)} '{searchValue}' was not found.";
    public Dictionary<string, object> Metadata { get; } = new()
    {
        { "EntityType", entityType },
        { "SearchBy", searchBy },
        { "SearchValue", searchValue }
    };

    public List<IError> Reasons { get; } = [];
}