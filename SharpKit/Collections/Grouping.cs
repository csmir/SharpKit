namespace SharpKit.Collections;

public class Grouping<TKey, T> : IGrouping<TKey, T>
    where TKey : notnull
{
    private readonly IEnumerable<T> _elements;

    public TKey Key { get; }

    public Grouping(IGrouping<TKey, T> enumerable)
    {
        _elements = enumerable;

        Key = enumerable.Key;
    }

    public Grouping(TKey key, IEnumerable<T> enumerable)
    {
        _elements = enumerable;

        Key = key;
    }

    public IEnumerator<T> GetEnumerator()
        => _elements.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}