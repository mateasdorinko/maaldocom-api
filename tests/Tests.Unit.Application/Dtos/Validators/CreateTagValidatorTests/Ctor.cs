using MaaldoCom.Api.Application.Dtos.Validators;
using Tests.Unit.Application.TestHelpers;

namespace Tests.Unit.Application.Dtos.Validators.CreateTagValidatorTests;

public class Ctor
{
    [Fact]
    public async Task Ctor_WithUniqueValidTag_PassesValidation()
    {
        // arrange
        var dbContext = A.Fake<IMaaldoComDbContext>();
        A.CallTo(() => dbContext.Tags).Returns(DbSetHelper.CreateFakeDbSet<Tag>([]));

        var validator = new CreateTagValidator(dbContext);
        var tag = new TagDto { Name = "nature" };

        // act
        var result = await validator.ValidateAsync(tag, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public async Task Ctor_WithDuplicateTagName_FailsValidation()
    {
        // arrange
        var existingTags = new List<Tag> { new() { Name = "nature" } };
        var dbContext = A.Fake<IMaaldoComDbContext>();
        A.CallTo(() => dbContext.Tags).Returns(DbSetHelper.CreateFakeDbSet(existingTags));

        var validator = new CreateTagValidator(dbContext);
        var tag = new TagDto { Name = "nature" };

        // act
        var result = await validator.ValidateAsync(tag, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Tag already exists");
    }

    [Fact]
    public async Task Ctor_WithEmptyName_FailsValidation()
    {
        // arrange
        var dbContext = A.Fake<IMaaldoComDbContext>();
        A.CallTo(() => dbContext.Tags).Returns(DbSetHelper.CreateFakeDbSet<Tag>([]));

        var validator = new CreateTagValidator(dbContext);
        var tag = new TagDto { Name = string.Empty };

        // act
        var result = await validator.ValidateAsync(tag, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Tag name is required");
    }

    [Fact]
    public async Task Ctor_WithNameExceedingMaxLength_FailsValidation()
    {
        // arrange
        var dbContext = A.Fake<IMaaldoComDbContext>();
        A.CallTo(() => dbContext.Tags).Returns(DbSetHelper.CreateFakeDbSet<Tag>([]));

        var validator = new CreateTagValidator(dbContext);
        var tag = new TagDto { Name = new string('a', 21) };

        // act
        var result = await validator.ValidateAsync(tag, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Tag name must be 20 characters or less");
    }
}
