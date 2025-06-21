namespace SharpKit.Commands;

public class GroupedList<TKey, T> : List<T>, IGrouping<TKey, T>
    where TKey : notnull
{
    public TKey Key { get; }

    public GroupedList(TKey key, int capacity)
        : base(capacity) => Key = key;

    public GroupedList(IGrouping<TKey, T> enumerable)
        : base(enumerable) => Key = enumerable.Key;

    public GroupedList(TKey key, IEnumerable<T> enumerable)
        : base(enumerable) => Key = key;
}
