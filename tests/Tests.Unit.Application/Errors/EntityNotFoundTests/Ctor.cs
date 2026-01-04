using MaaldoCom.Services.Application.Queries;

namespace Tests.Unit.Application.Errors.EntityNotFoundTests;

public class Ctor
{
    [Fact]
    public void Ctor_Instantiated_ArgsAreMappedCorrectly()
    {
        // arrange
        const string entityType = "Entity1";
        const SearchBy searchBy = SearchBy.Id;
        const string searchValue = "value1";

        // assert
        var error = new EntityNotFound(entityType, searchBy, searchValue);

        // act
        error.Metadata["EntityType"].ShouldBe(entityType);
        error.Metadata["SearchBy"].ShouldBe(searchBy);
        error.Metadata["SearchValue"].ShouldBe(searchValue);
    }
}