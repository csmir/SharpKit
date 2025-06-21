#pragma warning disable IDE0007 // Explicit typing is important for unsafe code.

using System.Runtime.InteropServices;

namespace Toolkit;

public static unsafe class Memory
{
#if NET6_0_OR_GREATER

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Clear(void* ptr, nuint length) => NativeMemory.Clear(ptr, length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Copy(void* source, void* destination, nuint length) => NativeMemory.Copy(source, destination, length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fill(void* ptr, nuint count, byte value) => NativeMemory.Fill(ptr, count, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Free(void* ptr, bool aligned = false) { if (aligned) NativeMemory.AlignedFree(ptr); else NativeMemory.Free(ptr); }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nint Reallocate(void* ptr, nuint length, nuint alignment = 1) => (nint)(alignment == 1 ? NativeMemory.Realloc(ptr, length) : NativeMemory.AlignedRealloc(ptr, length, alignment));

    public static nint ReallocateZeroed(void* ptr, nuint length, nuint newLength, nuint alignment = 1)
    {
        nint newPtr = Reallocate(ptr, newLength, alignment);
        Clear((void*)(newPtr + (nint)length), newLength - length);
        return newPtr;
    }
#else
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

            if (zeroed) Clear(raw, (nuint)size, alignment);

            byte* aligned = (byte*)(((nuint)((byte*)raw + sizeof(nint) + alignment - 1)) & ~(alignment - 1));
            ((nint*)aligned)[-1] = raw;

            return (nint)aligned;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Clear(nint ptr, nuint length, nuint alignment = 1)
    {
        switch (alignment)
        {
            case >= 8: length /= 8; for (nuint i = 0; i < length; i++) ( (long*)ptr)[i] = 0; break;
               case 4: length /= 4; for (nuint i = 0; i < length; i++) (  (int*)ptr)[i] = 0; break;
               case 2: length /= 2; for (nuint i = 0; i < length; i++) ((short*)ptr)[i] = 0; break;
              default:              for (nuint i = 0; i < length; i++) ( (byte*)ptr)[i] = 0; break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Copy(nint source, nint destination, nuint length) => Buffer.MemoryCopy((void*)source, (void*)destination, length, length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fill(nint ptr, nuint count, byte value)
    {
        for (nuint i = 0; i < count; i++) ((byte*)ptr)[i] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Free(nint ptr, bool aligned = false) => Marshal.FreeHGlobal(aligned ? ((nint*)ptr)[-1] : ptr);

    public static nint Reallocate(nint ptr, nuint length, nuint newLength, nuint alignment = 1, bool zeroed = false)
    {
        nint newPtr = Allocate(newLength, alignment);

        if (ptr == 0) return newPtr;

        Copy(ptr, newPtr, newLength > length ? length : newLength);
        Free(ptr, alignment != 1);

        if (zeroed) Clear(newPtr + (nint)length, newLength - length, alignment);

        return newPtr;
    }
#endif
}
