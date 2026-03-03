#pragma warning disable IDE0007 // Explicit typing is important for unsafe code.

using System.Runtime.InteropServices;

namespace Nimble.Performance;

/// <summary>
/// This class contains methods for native memory management, in a framework-agnostic manner.
/// </summary>
public static unsafe class Memory
{
#if NET6_0_OR_GREATER

    /// <summary>
    ///     Allocates a block of memory of the specified size, in bytes.
    /// </summary>
    /// <param name="length"> The size, in bytes, of the block to allocate. </param>
    /// <param name="alignment"> The alignment, in bytes, of the block to allocate. This must be a power of 2. </param>
    /// <param name="zeroed"> Whether the allocated block should be zeroed. </param>
    /// <returns> A pointer to the allocated block of memory. </returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="OutOfMemoryException"/>
    public static void* Allocate(nuint length, nuint alignment = 1, bool zeroed = false)
    {
        if (alignment == 1)
        {
            return zeroed ? NativeMemory.AllocZeroed(length) : NativeMemory.Alloc(length);
        }
        else
        {
            void* ptr = NativeMemory.AlignedAlloc(length, alignment);
            if (zeroed) NativeMemory.Clear(ptr, length);
            return ptr;
        }
    }

    /// <summary>
    ///     Clears a block of memory.
    /// </summary>
    /// <param name="ptr"> A pointer to the block of memory that should be cleared. </param>
    /// <param name="length"> The size, in bytes, of the block to clear. </param>
    /// <remarks>
    ///     <para> If this method is called with <paramref name="ptr" /> being <see langword="null"/> and <paramref name="length" /> being 0, it will be equivalent to a no-op. </para>
    ///     <para> The behavior when <paramref name="ptr" /> is <see langword="null"/> and <paramref name="length" /> is greater than 0 is undefined. </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Clear(void* ptr, nuint length) => NativeMemory.Clear(ptr, length);

    /// <summary>
    ///     Copies a block of memory from memory location <paramref name="source"/>, to memory location <paramref name="destination"/>.
    /// </summary>
    /// <param name="source"> A pointer to the source of data to be copied. </param>
    /// <param name="destination"> A pointer to the destination memory block where the data is to be copied. </param>
    /// <param name="length"> The size, in bytes, to be copied from the source location to the destination. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Copy(void* source, void* destination, nuint length) => NativeMemory.Copy(source, destination, length);

    /// <summary>
    ///     Copies the byte <paramref name="value"/> to the first <paramref name="count"/> bytes of the memory located at <paramref name="ptr"/>.
    /// </summary>
    /// <param name="ptr"> A pointer to the block of memory to fill. </param>
    /// <param name="count"> The number of bytes to be set to <paramref name="value"/>. </param>
    /// <param name="value"> The value to be set. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fill(void* ptr, nuint count, byte value) => NativeMemory.Fill(ptr, count, value);

    /// <summary>
    ///     Frees a block of memory.
    /// </summary>
    /// <param name="ptr"> A pointer to the block of memory that should be freed. </param>
    /// <param name="aligned"> Whether an aligned free should be used. Must be set when clearing aligned blocks. </param>
    /// <remarks>
    ///    This method does nothing if <paramref name="ptr"/> is <see langword="null"/>.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Free(void* ptr, bool aligned = false) { if (aligned) NativeMemory.AlignedFree(ptr); else NativeMemory.Free(ptr); }

    /// <summary>
    ///     Reallocates an aligned block of memory of the specified size, in bytes.
    /// </summary>
    /// <param name="ptr"> The previously allocated block of memory. </param>
    /// <param name="length"> The size, in bytes, of the block to allocate. </param>
    /// <param name="alignment"> The alignment, in bytes, of the block to allocate. This must be a power of 2. </param>
    /// <returns> A pointer to the reallocated block of memory. </returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="OutOfMemoryException"/>
    /// <remarks>
    ///     <para> If <paramref name="ptr" /> is <see langword="null"/>, this method acts as <see cref="Allocate(nuint, nuint, bool)" />. </para>
    ///     <para> This method allows <paramref name="length" /> to be 0, and will return a valid pointer that should not be dereferenced, and must be freed to avoid memory leaks. </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nint Reallocate(void* ptr, nuint length, nuint alignment = 1) => (nint)(alignment == 1 ? NativeMemory.Realloc(ptr, length) : NativeMemory.AlignedRealloc(ptr, length, alignment));

    /// <summary>
    ///     Reallocates an aligned block of memory of the specified size, in bytes.
    /// </summary>
    /// <param name="ptr"> The previously allocated block of memory. </param>
    /// <param name="length"> The size, in bytes, of the original memory block. </param>
    /// <param name="newLength"> The size, in bytes, of the new memory block. </param>
    /// <param name="alignment"> The alignment, in bytes, of the block to allocate. This must be a power of 2. </param>
    /// <returns> A pointer to the reallocated block of memory. </returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="OutOfMemoryException"/>
    /// <remarks>
    ///     <para> If <paramref name="ptr" /> is <see langword="null"/>, this method acts as <see cref="Allocate(nuint, nuint, bool)" />. </para>
    ///     <para> This method allows <paramref name="length" /> to be 0, and will return a valid pointer that should not be dereferenced, and must be freed to avoid memory leaks. </para>
    /// </remarks>
    public static nint ReallocateZeroed(void* ptr, nuint length, nuint newLength, nuint alignment = 1)
    {
        nint newPtr = Reallocate(ptr, newLength, alignment);
        Clear((void*)(newPtr + (nint)length), newLength - length);
        return newPtr;
    }

#else

    /// <summary>
    ///     Allocates a block of memory of the specified size, in bytes.
    /// </summary>
    /// <param name="length"> The size, in bytes, of the block to allocate. </param>
    /// <param name="alignment"> The alignment, in bytes, of the block to allocate. This must be a power of 2. </param>
    /// <param name="zeroed"> Whether the allocated block should be zeroed. </param>
    /// <returns> A pointer to the allocated block of memory. </returns>
    /// <exception cref="OutOfMemoryException"/>
    public static nint Allocate(nuint length, nuint alignment = 1, bool zeroed = false)
    {
        if (alignment == 1)
        {
            nint ptr = Marshal.AllocHGlobal((int)length);
            if (zeroed) Clear(ptr, length);
            return ptr;
        }
        else
        {
            int size = (int)(length + alignment) + sizeof(nint);
            nint raw = Marshal.AllocHGlobal(size);

            if (zeroed) Clear(raw, (nuint)size);

            byte* aligned = (byte*)(((nuint)((byte*)raw + sizeof(nint) + alignment - 1)) & ~(alignment - 1));
            ((nint*)aligned)[-1] = raw;

            return (nint)aligned;
        }
    }

    /// <summary>
    ///     Clears a block of memory.
    /// </summary>
    /// <param name="ptr"> A pointer to the block of memory that should be cleared. </param>
    /// <param name="length"> The size, in bytes, of the block to clear. </param>
    /// <remarks>
    ///     If this method is called with <paramref name="ptr" /> being 0 and <paramref name="length" /> being 0, it will be equivalent to a no-op.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Clear(nint ptr, nuint length)
    {
        if (ptr == 0 || length == 0) return;

        nuint* wp = (nuint*)ptr; nuint words = length / (nuint)sizeof(nuint);

        for (nuint i = 0; i < words; i++) *wp++ = 0;

        byte* bp = (byte*)ptr;

        for (words *= (nuint)sizeof(nuint); words < length; words++) *bp++ = 0;
    }

    /// <summary>
    ///     Copies a block of memory from memory location <paramref name="source"/>, to memory location <paramref name="destination"/>.
    /// </summary>
    /// <param name="source"> A pointer to the source of data to be copied. </param>
    /// <param name="destination"> A pointer to the destination memory block where the data is to be copied. </param>
    /// <param name="length"> The size, in bytes, to be copied from the source location to the destination. </param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Copy(nint source, nint destination, nuint length) => Buffer.MemoryCopy((void*)source, (void*)destination, length, length);

    /// <summary>
    ///     Copies the byte <paramref name="value"/> to the first <paramref name="count"/> bytes of the memory located at <paramref name="ptr"/>.
    /// </summary>
    /// <param name="ptr"> A pointer to the block of memory to fill. </param>
    /// <param name="count"> The number of bytes to be set to <paramref name="value"/>. </param>
    /// <param name="value"> The value to be set. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fill(nint ptr, nuint count, byte value)
    {
        if (count == 0) return;

        nuint words = count / (nuint)sizeof(nuint), wValue = 0;
        
        nuint* wp = (nuint*)ptr;

        if (sizeof(nuint) == 8)
        {
            ((byte*)&wValue)[0] = value; ((byte*)&wValue)[1] = value; ((byte*)&wValue)[2] = value; ((byte*)&wValue)[3] = value;
            ((byte*)&wValue)[4] = value; ((byte*)&wValue)[5] = value; ((byte*)&wValue)[6] = value; ((byte*)&wValue)[7] = value;
        }
        else
        {
            ((byte*)&wValue)[0] = value; ((byte*)&wValue)[1] = value; ((byte*)&wValue)[2] = value; ((byte*)&wValue)[3] = value;
        }

        for (nuint i = 0; i < words; i++) *wp++ = wValue;

        byte* bp = (byte*)ptr;

        for (words *= (nuint)sizeof(nuint); words < count; words++) *bp++ = value;
    }

    /// <summary>
    ///     Frees a block of memory.
    /// </summary>
    /// <param name="ptr"> A pointer to the block of memory that should be freed. </param>
    /// <param name="aligned"> Whether an aligned free should be used. Must be set when clearing aligned blocks. </param>
    /// <remarks>
    ///    This method does nothing if <paramref name="ptr"/> is 0.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Free(nint ptr, bool aligned = false)
    {
        if (ptr == 0) return;

        Marshal.FreeHGlobal(aligned ? ((nint*)ptr)[-1] : ptr);
    }

    /// <summary>
    ///     Reallocates an aligned block of memory of the specified size, in bytes.
    /// </summary>
    /// <param name="ptr"> The previously allocated block of memory. </param>
    /// <param name="length"> The size, in bytes, of the original memory block. </param>
    /// <param name="newLength"> The size, in bytes, of the new memory block. </param>
    /// <param name="alignment"> The alignment, in bytes, of the block to allocate. This must be a power of 2. </param>
    /// <param name="zeroed"> Whether the allocated block should be zeroed. </param>
    /// <returns> A pointer to the reallocated block of memory. </returns>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="OutOfMemoryException"/>
    /// <remarks>
    ///     <para> If <paramref name="ptr" /> is 0, this method acts as <see cref="Allocate(nuint, nuint, bool)" />. </para>
    /// </remarks>
    public static nint Reallocate(nint ptr, nuint length, nuint newLength, nuint alignment = 1, bool zeroed = false)
    {
        nint newPtr = Allocate(newLength, alignment);

        if (ptr == 0) return newPtr;

        Copy(ptr, newPtr, newLength > length ? length : newLength);
        Free(ptr, alignment != 1);

        if (zeroed) Clear(newPtr + (nint)length, newLength - length);

        return newPtr;
    }

#endif
}