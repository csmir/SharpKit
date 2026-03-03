namespace Nimble.Collections;

/// <summary>
///     An implementation of <see cref="IGrouping{TKey, TElement}"/> that can be used for key-driven collections outside of emitting through LINQ.
/// </summary>
/// <remarks>
///     If mutability is desired, consider using <see cref="GroupedList{TKey, TValue}"/> instead.
/// </remarks>
/// <typeparam name="TKey">The collection key, which represents itself as the primary value to group <typeparamref name="TValue"/> by.</typeparam>
/// <typeparam name="TValue">The collection element, which is the type that is contained within the group constrained by <typeparamref name="TKey"/>.</typeparam>
public class Grouping<TKey, TValue> : IGrouping<TKey, TValue>
    where TKey : notnull
{
    private readonly IEnumerable<TValue> _elements;

    /// <inheritdoc />
    public TKey Key { get; }

    /// <summary>
    ///     Creates a new <see cref="Grouping{TKey, TValue}"/> with the provided <see cref="IGrouping{TKey, TValue}"/> as the source of the grouping elements and key.
    /// </summary>
    /// <param name="enumerable">The group to reinterpret as a value of <see cref="Grouping{TKey, TValue}"/>.</param>
    public Grouping(IGrouping<TKey, TValue> enumerable)
    {
        _elements = enumerable;

        Key = enumerable.Key;
    }

    /// <summary>
    ///     Creates a new <see cref="Grouping{TKey, TValue}"/> with the provided key and elements.
    /// </summary>
    /// <param name="key">The value of <typeparamref name="TKey"/>.</param>
    /// <param name="enumerable">The set of values to reinterpret as a collection of <typeparamref name="TValue"/>.</param>
    public Grouping(TKey key, IEnumerable<TValue> enumerable)
    {
        _elements = enumerable;

        Key = key;
    }

    /// <inheritdoc />
    public IEnumerator<TValue> GetEnumerator()
        => _elements.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}