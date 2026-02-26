#if NET8_0_OR_GREATER

using static System.Runtime.InteropServices.MemoryMarshal;

namespace SharpKit.Collections;
internal sealed class ValueArrayEnumerator<T>(Array array) : IEnumerator<T>
{
    private readonly int _length = array.Length;
    private int i = -1;

    public T Current => CreateSpan(ref Unsafe.As<byte, T>(ref GetArrayDataReference(array)), _length)[i];
    object IEnumerator.Current => Current!;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() => i++ != _length;
    public void Reset() => i = -1;
    public void Dispose() {}
}

#endif