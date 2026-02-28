namespace Tests.Unit.Infrastructure.TestHelpers;

/// <summary>
/// A HybridCache test double. Pre-seed entries via Setup&lt;T&gt;; if a key is not
/// pre-seeded the factory is invoked (cache-miss behaviour). Tracks RemoveAsync calls.
/// </summary>
internal sealed class TestHybridCache : HybridCache
{
    private readonly Dictionary<string, object> _entries = new();

    public List<string> RemovedKeys { get; } = [];

    public void Setup<T>(string key, T value) => _entries[key] = value!;

    public override async ValueTask<T> GetOrCreateAsync<TState, T>(
        string key,
        TState state,
        Func<TState, CancellationToken, ValueTask<T>> factory,
        HybridCacheEntryOptions? options = null,
        IEnumerable<string>? tags = null,
        CancellationToken cancellationToken = default)
    {
        if (_entries.TryGetValue(key, out var cached))
            return (T)cached;

        return await factory(state, cancellationToken);
    }

    public override ValueTask RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        RemovedKeys.Add(key);
        return ValueTask.CompletedTask;
    }

    public override ValueTask SetAsync<T>(
        string key,
        T value,
        HybridCacheEntryOptions? options = null,
        IEnumerable<string>? tags = null,
        CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

    public override ValueTask RemoveByTagAsync(string tag, CancellationToken cancellationToken = default)
        => ValueTask.CompletedTask;
}
