namespace MaaldoCom.Api.Application.Extensions;

public static class FluentResultsExtensions
{
    public static TMatch Match<T, TMatch>(this Result<T> result, Func<T, TMatch> onSuccess, Func<List<IError>, TMatch> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Errors.ToList());
    }

    public static TMatch Match<TMatch>(this Result result, Func<TMatch> onSuccess, Func<List<IError>, TMatch> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Errors.ToList());
    }
}