using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Tests.Unit.Application.TestHelpers;

internal sealed class TestAsyncQueryProvider<TEntity>(IQueryProvider inner) : IAsyncQueryProvider
{
    public IQueryable CreateQuery(Expression expression) =>
        new TestAsyncEnumerable<TEntity>(inner.CreateQuery<TEntity>(expression));

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
        new TestAsyncEnumerable<TElement>(inner.CreateQuery<TElement>(expression));

    public object? Execute(Expression expression) => inner.Execute(expression);

    public TResult Execute<TResult>(Expression expression) => inner.Execute<TResult>(expression);

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    {
        var result = Execute(expression);
        var resultType = typeof(TResult).GetGenericArguments()[0];
        return (TResult)typeof(Task)
            .GetMethod(nameof(Task.FromResult))!
            .MakeGenericMethod(resultType)
            .Invoke(null, [result])!;
    }
}

internal sealed class TestAsyncEnumerable<T> : IAsyncEnumerable<T>, IQueryable<T>
{
    private readonly IQueryable<T> _inner;
    private readonly IQueryProvider _provider;

    public TestAsyncEnumerable(IEnumerable<T> enumerable)
    {
        _inner = enumerable.AsQueryable();
        _provider = new TestAsyncQueryProvider<T>(_inner.Provider);
    }

    public TestAsyncEnumerable(IQueryable<T> queryable)
    {
        _inner = queryable;
        _provider = new TestAsyncQueryProvider<T>(queryable.Provider);
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) =>
        new TestAsyncEnumerator<T>(_inner.GetEnumerator());

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => _inner.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() =>
        ((System.Collections.IEnumerable)_inner).GetEnumerator();

    Type IQueryable.ElementType => _inner.ElementType;
    Expression IQueryable.Expression => _inner.Expression;
    IQueryProvider IQueryable.Provider => _provider;
}

internal sealed class TestAsyncEnumerator<T>(IEnumerator<T> inner) : IAsyncEnumerator<T>
{
    public T Current => inner.Current;
    public ValueTask<bool> MoveNextAsync() => new(inner.MoveNext());
    public ValueTask DisposeAsync() { inner.Dispose(); return ValueTask.CompletedTask; }
}

internal static class DbSetHelper
{
    internal static DbSet<T> CreateFakeDbSet<T>(IEnumerable<T> data) where T : class
    {
        var fakeDbSet = A.Fake<DbSet<T>>(x => x.Implements<IQueryable<T>>());
        var asyncQueryable = new TestAsyncEnumerable<T>(data);
        var q = (IQueryable<T>)asyncQueryable;

        A.CallTo(() => ((IQueryable<T>)fakeDbSet).Provider).Returns(q.Provider);
        A.CallTo(() => ((IQueryable<T>)fakeDbSet).Expression).Returns(q.Expression);
        A.CallTo(() => ((IQueryable<T>)fakeDbSet).ElementType).Returns(q.ElementType);
        A.CallTo(() => ((IEnumerable<T>)fakeDbSet).GetEnumerator())
            .ReturnsLazily(() => asyncQueryable.AsEnumerable().GetEnumerator());

        return fakeDbSet;
    }
}
