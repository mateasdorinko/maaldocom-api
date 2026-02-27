namespace Tests.Unit.Application.Extensions.FluentResultsExtensionsTests;

public class Match
{
    [Fact]
    public void Match_GenericResult_WhenSuccess_CallsOnSuccess()
    {
        // arrange
        var result = Result.Ok("value");

        // act
        var matched = result.Match(
            onSuccess: v => $"success:{v}",
            onFailure: _ => "failure");

        // assert
        matched.ShouldBe("success:value");
    }

    [Fact]
    public void Match_GenericResult_WhenFailure_CallsOnFailure()
    {
        // arrange
        var result = Result.Fail<string>("something went wrong");

        // act
        var matched = result.Match(
            onSuccess: _ => "success",
            onFailure: errors => $"failure:{errors[0].Message}");

        // assert
        matched.ShouldBe("failure:something went wrong");
    }

    [Fact]
    public void Match_GenericResult_WhenFailure_PassesAllErrorsToOnFailure()
    {
        // arrange
        var result = Result.Fail<string>(new List<IError>
        {
            new Error("error1"),
            new Error("error2")
        });

        // act
        var errorCount = result.Match(
            onSuccess: _ => 0,
            onFailure: errors => errors.Count);

        // assert
        errorCount.ShouldBe(2);
    }

    [Fact]
    public void Match_NonGenericResult_WhenSuccess_CallsOnSuccess()
    {
        // arrange
        var result = Result.Ok();

        // act
        var matched = result.Match(
            onSuccess: () => "success",
            onFailure: _ => "failure");

        // assert
        matched.ShouldBe("success");
    }

    [Fact]
    public void Match_NonGenericResult_WhenFailure_CallsOnFailure()
    {
        // arrange
        var result = Result.Fail("something went wrong");

        // act
        var matched = result.Match(
            onSuccess: () => "success",
            onFailure: errors => $"failure:{errors[0].Message}");

        // assert
        matched.ShouldBe("failure:something went wrong");
    }
}
