namespace Nimble.Collections;

/// <summary>
///     Represents an implementation of <see cref="Grouping{TKey, TValue}"/> with list-based mutability.
/// </summary>
/// <remarks>
///     If mutability is not required, consider using <see cref="Grouping{TKey, TValue}"/> instead.
/// </remarks>
/// <typeparam name="TKey">The collection key, which represents itself as the primary value to group <typeparamref name="TValue"/> by.</typeparam>
/// <typeparam name="TValue">The collection element, which is the type that is contained within the group constrained by <typeparamref name="TKey"/>.</typeparam>
public class GroupedList<TKey, TValue> : List<TValue>, IGrouping<TKey, TValue>
    where TKey : notnull
{
    /// <inheritdoc />
    public TKey Key { get; }

    /// <summary>
    ///     Creates a new <see cref="GroupedList{TKey, TValue}"/> with the provided key and capacity for the elements.
    /// </summary>
    /// <param name="key">The value of <typeparamref name="TKey"/>.</param>
    /// <param name="capacity">The starting capacity of the <see cref="GroupedList{TKey, TValue}"/>.</param>
    public GroupedList(TKey key, int capacity)
        : base(capacity) => Key = key;

    /// <summary>
    ///     Creates a new <see cref="GroupedList{TKey, TValue}"/> with the provided <see cref="IGrouping{TKey, TValue}"/> as the source of the grouping elements and key.
    /// </summary>
    /// <param name="enumerable">The group to reinterpret as a value of <see cref="GroupedList{TKey, TValue}"/>.</param>
    public GroupedList(IGrouping<TKey, TValue> enumerable)
        : base(enumerable) => Key = enumerable.Key;

    /// <summary>
    ///     Creates a new <see cref="GroupedList{TKey, TValue}"/> with the provided key and elements.
    /// </summary>
    /// <param name="key">The value of <typeparamref name="TKey"/>.</param>
    /// <param name="enumerable">The set of values to reinterpret as a collection of <typeparamref name="TValue"/>.</param>
    public GroupedList(TKey key, IEnumerable<TValue> enumerable)
        : base(enumerable) => Key = key;
}
