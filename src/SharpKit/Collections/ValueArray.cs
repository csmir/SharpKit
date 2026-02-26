#if NET8_0_OR_GREATER

namespace SharpKit.Collections;
public class ValueArray<T>(Array array) : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator() => new ValueArrayEnumerator<T>(array);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static explicit operator ValueArray<T>(Array array) => new(array);
}

#endif