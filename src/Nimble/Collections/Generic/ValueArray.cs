#if NET6_0_OR_GREATER

using System.Runtime.InteropServices;

namespace Nimble.Collections.Generic;

/// <summary>
///     An array that represents <typeparamref name="T"/> as values inside the array.
/// </summary>
/// <typeparam name="T">The type of value the array.</typeparam>
/// <param name="array">The array to reinterpret as a value-bound array.</param>
public class ValueArray<T>(Array array) : IEnumerable<T>
{
    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() => new ValueArrayEnumerator(array);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    ///     Reinterprets the provided <see cref="Array"/> as a <see cref="ValueArray{T}"/>.
    /// </summary>
    /// <param name="array">The array to reinterpret as a <see cref="ValueArray{T}"/>.</param>
    public static explicit operator ValueArray<T>(Array array) => new(array);

    internal sealed class ValueArrayEnumerator(Array array) : IEnumerator<T>
    {
        private readonly int _length = array.Length;
        private int i = -1;

        public T Current => MemoryMarshal.CreateSpan(ref Unsafe.As<byte, T>(ref MemoryMarshal.GetArrayDataReference(array)), _length)[i];
        
        object IEnumerator.Current => Current!;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => i++ != _length;

        public void Reset() => i = -1;
        
        public void Dispose() { }
    }
}

#endif