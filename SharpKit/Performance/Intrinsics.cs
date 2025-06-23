#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace SharpKit.Performance;

public static unsafe partial class Intrinsics
{
    #region Constants
    internal const int rb = 64, eb = 32, sb = 08,
                   rw = 32, ew = 16, sw = 06,
                   rd = 16, ed = 08, sd = 04,
                   rq = 08, eq = 04, sq = 02;

    public static unsafe class Constants
    {
        public const int MaxFactorial = 170;


        static Constants()
        {
            Factorials = new double[MaxFactorial];

            fixed (double* dest = Factorials)
            {
                dest[0] = 1.0;
                for (int i = 1; i <= 170; i++) dest[i] = dest[i - 1] * i;
            }
        }

        public static readonly double[] Factorials;
    }
    #endregion

    #region Bitwise(ReadOnlySpan<T> input, T mask, Span<T> output)

    #region And(ReadOnlySpan<T> input, T mask, Span<T> output)
    public static void And(ReadOnlySpan<byte> input, byte mask, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @out = output) fixed (byte* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<byte> masks = Vector512.Create(mask);
                for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<byte> masks = Vector256.Create(mask);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.And(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<byte> masks = Vector128.Create(mask);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (byte)(@in[i] & mask);
        }
    }

    public static void And(ReadOnlySpan<sbyte> input, sbyte mask, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<sbyte> masks = Vector512.Create(mask);
                for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<sbyte> masks = Vector256.Create(mask);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.And(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<sbyte> masks = Vector128.Create(mask);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (sbyte)(@in[i] & mask);
        }
    }

    public static void And(ReadOnlySpan<short> input, short mask, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @out = output) fixed (short* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<short> masks = Vector512.Create(mask);
                for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<short> masks = Vector256.Create(mask);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.And(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<short> masks = Vector128.Create(mask);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (short)(@in[i] & mask);
        }
    }

    public static void And(ReadOnlySpan<ushort> input, ushort mask, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<ushort> masks = Vector512.Create(mask);
                for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<ushort> masks = Vector256.Create(mask);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.And(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ushort> masks = Vector128.Create(mask);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (ushort)(@in[i] & mask);
        }
    }

    public static void And(ReadOnlySpan<int> input, int mask, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @out = output) fixed (int* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<int> masks = Vector512.Create(mask);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<int> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.And(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<int> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void And(ReadOnlySpan<uint> input, uint mask, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @out = output) fixed (uint* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<uint> masks = Vector512.Create(mask);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<uint> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.And(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<uint> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void And(ReadOnlySpan<long> input, long mask, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @out = output) fixed (long* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<long> masks = Vector512.Create(mask);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<long> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.And(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<long> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void And(ReadOnlySpan<ulong> input, ulong mask, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<ulong> masks = Vector512.Create(mask);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<ulong> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.And(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ulong> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }
    #endregion

    #region Or(ReadOnlySpan<T> input, T mask, Span<T> output)
    public static void Or(ReadOnlySpan<byte> input, byte mask, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @out = output) fixed (byte* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<byte> masks = Vector512.Create(mask);
                for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<byte> masks = Vector256.Create(mask);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.Or(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<byte> masks = Vector128.Create(mask);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (byte)(@in[i] & mask);
        }
    }

    public static void Or(ReadOnlySpan<sbyte> input, sbyte mask, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<sbyte> masks = Vector512.Create(mask);
                for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<sbyte> masks = Vector256.Create(mask);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.Or(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<sbyte> masks = Vector128.Create(mask);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (sbyte)(@in[i] & mask);
        }
    }

    public static void Or(ReadOnlySpan<short> input, short mask, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @out = output) fixed (short* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<short> masks = Vector512.Create(mask);
                for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<short> masks = Vector256.Create(mask);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.Or(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<short> masks = Vector128.Create(mask);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (short)(@in[i] & mask);
        }
    }

    public static void Or(ReadOnlySpan<ushort> input, ushort mask, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<ushort> masks = Vector512.Create(mask);
                for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<ushort> masks = Vector256.Create(mask);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.Or(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ushort> masks = Vector128.Create(mask);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (ushort)(@in[i] & mask);
        }
    }

    public static void Or(ReadOnlySpan<int> input, int mask, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @out = output) fixed (int* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<int> masks = Vector512.Create(mask);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<int> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Or(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<int> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void Or(ReadOnlySpan<uint> input, uint mask, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @out = output) fixed (uint* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<uint> masks = Vector512.Create(mask);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<uint> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Or(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<uint> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void Or(ReadOnlySpan<long> input, long mask, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @out = output) fixed (long* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<long> masks = Vector512.Create(mask);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<long> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Or(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<long> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void Or(ReadOnlySpan<ulong> input, ulong mask, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<ulong> masks = Vector512.Create(mask);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<ulong> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Or(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ulong> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }
    #endregion

    #region Xor(ReadOnlySpan<T> input, T mask, Span<T> output)
    public static void Xor(ReadOnlySpan<byte> input, byte mask, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @out = output) fixed (byte* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<byte> masks = Vector512.Create(mask);
                for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<byte> masks = Vector256.Create(mask);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.Xor(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<byte> masks = Vector128.Create(mask);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (byte)(@in[i] & mask);
        }
    }

    public static void Xor(ReadOnlySpan<sbyte> input, sbyte mask, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<sbyte> masks = Vector512.Create(mask);
                for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<sbyte> masks = Vector256.Create(mask);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.Xor(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<sbyte> masks = Vector128.Create(mask);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (sbyte)(@in[i] & mask);
        }
    }

    public static void Xor(ReadOnlySpan<short> input, short mask, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @out = output) fixed (short* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<short> masks = Vector512.Create(mask);
                for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<short> masks = Vector256.Create(mask);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.Xor(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<short> masks = Vector128.Create(mask);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (short)(@in[i] & mask);
        }
    }

    public static void Xor(ReadOnlySpan<ushort> input, ushort mask, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<ushort> masks = Vector512.Create(mask);
                for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<ushort> masks = Vector256.Create(mask);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.Xor(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ushort> masks = Vector128.Create(mask);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (ushort)(@in[i] & mask);
        }
    }

    public static void Xor(ReadOnlySpan<int> input, int mask, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @out = output) fixed (int* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<int> masks = Vector512.Create(mask);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<int> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Xor(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<int> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void Xor(ReadOnlySpan<uint> input, uint mask, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @out = output) fixed (uint* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<uint> masks = Vector512.Create(mask);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<uint> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Xor(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<uint> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void Xor(ReadOnlySpan<long> input, long mask, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @out = output) fixed (long* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<long> masks = Vector512.Create(mask);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<long> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Xor(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<long> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void Xor(ReadOnlySpan<ulong> input, ulong mask, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<ulong> masks = Vector512.Create(mask);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<ulong> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Xor(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ulong> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }
    #endregion

    #region AndNot(ReadOnlySpan<T> input, T mask, Span<T> output)
    public static void AndNot(ReadOnlySpan<byte> input, byte mask, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @out = output) fixed (byte* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<byte> masks = Vector512.Create(mask);
                for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<byte> masks = Vector256.Create(mask);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.AndNot(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<byte> masks = Vector128.Create(mask);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (byte)(@in[i] & mask);
        }
    }

    public static void AndNot(ReadOnlySpan<sbyte> input, sbyte mask, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<sbyte> masks = Vector512.Create(mask);
                for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<sbyte> masks = Vector256.Create(mask);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.AndNot(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<sbyte> masks = Vector128.Create(mask);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (sbyte)(@in[i] & mask);
        }
    }

    public static void AndNot(ReadOnlySpan<short> input, short mask, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @out = output) fixed (short* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<short> masks = Vector512.Create(mask);
                for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<short> masks = Vector256.Create(mask);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.AndNot(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<short> masks = Vector128.Create(mask);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (short)(@in[i] & mask);
        }
    }

    public static void AndNot(ReadOnlySpan<ushort> input, ushort mask, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<ushort> masks = Vector512.Create(mask);
                for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<ushort> masks = Vector256.Create(mask);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.AndNot(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ushort> masks = Vector128.Create(mask);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = (ushort)(@in[i] & mask);
        }
    }

    public static void AndNot(ReadOnlySpan<int> input, int mask, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @out = output) fixed (int* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<int> masks = Vector512.Create(mask);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<int> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.AndNot(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<int> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void AndNot(ReadOnlySpan<uint> input, uint mask, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @out = output) fixed (uint* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<uint> masks = Vector512.Create(mask);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<uint> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.AndNot(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<uint> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void AndNot(ReadOnlySpan<long> input, long mask, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @out = output) fixed (long* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<long> masks = Vector512.Create(mask);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<long> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.AndNot(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<long> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }

    public static void AndNot(ReadOnlySpan<ulong> input, ulong mask, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<ulong> masks = Vector512.Create(mask);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), masks));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<ulong> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.AndNot(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ulong> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = @in[i] & mask;
        }
    }
    #endregion

    #endregion

    #region Bitwise(ReadOnlySpan<T> input, T mask, Span<T> output)

    #region And(ReadOnlySpan<T> input, ReadOnlySpan<T> mask, Span<T> output)
    public static void And(ReadOnlySpan<byte> input, ReadOnlySpan<byte> mask, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @maskPtr = mask) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (byte)(@in[i] & @maskPtr[i]);
        }
    }

    public static void And(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> mask, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @maskPtr = mask) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)(@in[i] & @maskPtr[i]);
        }
    }

    public static void And(ReadOnlySpan<short> input, ReadOnlySpan<short> mask, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @maskPtr = mask) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (short)(@in[i] & @maskPtr[i]);
        }
    }

    public static void And(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> mask, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @maskPtr = mask) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)(@in[i] & @maskPtr[i]);
        }
    }

    public static void And(ReadOnlySpan<int> input, ReadOnlySpan<int> mask, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @maskPtr = mask) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void And(ReadOnlySpan<uint> input, ReadOnlySpan<uint> mask, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @maskPtr = mask) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void And(ReadOnlySpan<long> input, ReadOnlySpan<long> mask, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @maskPtr = mask) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void And(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> mask, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @maskPtr = mask) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }
    #endregion

    #region Or(ReadOnlySpan<T> input, ReadOnlySpan<T> mask, Span<T> output)
    public static void Or(ReadOnlySpan<byte> input, ReadOnlySpan<byte> mask, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @maskPtr = mask) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (byte)(@in[i] & @maskPtr[i]);
        }
    }

    public static void Or(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> mask, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @maskPtr = mask) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)(@in[i] & @maskPtr[i]);
        }
    }

    public static void Or(ReadOnlySpan<short> input, ReadOnlySpan<short> mask, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @maskPtr = mask) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (short)(@in[i] & @maskPtr[i]);
        }
    }

    public static void Or(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> mask, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @maskPtr = mask) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)(@in[i] & @maskPtr[i]);
        }
    }

    public static void Or(ReadOnlySpan<int> input, ReadOnlySpan<int> mask, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @maskPtr = mask) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void Or(ReadOnlySpan<uint> input, ReadOnlySpan<uint> mask, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @maskPtr = mask) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void Or(ReadOnlySpan<long> input, ReadOnlySpan<long> mask, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @maskPtr = mask) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void Or(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> mask, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @maskPtr = mask) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }
    #endregion

    #region Xor(ReadOnlySpan<T> input, ReadOnlySpan<T> mask, Span<T> output)
    public static void Xor(ReadOnlySpan<byte> input, ReadOnlySpan<byte> mask, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @maskPtr = mask) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (byte)(@in[i] & @maskPtr[i]);
        }
    }

    public static void Xor(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> mask, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @maskPtr = mask) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)(@in[i] & @maskPtr[i]);
        }
    }

    public static void Xor(ReadOnlySpan<short> input, ReadOnlySpan<short> mask, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @maskPtr = mask) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (short)(@in[i] & @maskPtr[i]);
        }
    }

    public static void Xor(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> mask, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @maskPtr = mask) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)(@in[i] & @maskPtr[i]);
        }
    }

    public static void Xor(ReadOnlySpan<int> input, ReadOnlySpan<int> mask, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @maskPtr = mask) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void Xor(ReadOnlySpan<uint> input, ReadOnlySpan<uint> mask, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @maskPtr = mask) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void Xor(ReadOnlySpan<long> input, ReadOnlySpan<long> mask, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @maskPtr = mask) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void Xor(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> mask, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @maskPtr = mask) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }
    #endregion

    #region AndNot(ReadOnlySpan<T> input, ReadOnlySpan<T> mask, Span<T> output)
    public static void AndNot(ReadOnlySpan<byte> input, ReadOnlySpan<byte> mask, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @maskPtr = mask) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (byte)(@in[i] & @maskPtr[i]);
        }
    }

    public static void AndNot(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> mask, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @maskPtr = mask) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)(@in[i] & @maskPtr[i]);
        }
    }

    public static void AndNot(ReadOnlySpan<short> input, ReadOnlySpan<short> mask, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @maskPtr = mask) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (short)(@in[i] & @maskPtr[i]);
        }
    }

    public static void AndNot(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> mask, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @maskPtr = mask) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)(@in[i] & @maskPtr[i]);
        }
    }

    public static void AndNot(ReadOnlySpan<int> input, ReadOnlySpan<int> mask, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @maskPtr = mask) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void AndNot(ReadOnlySpan<uint> input, ReadOnlySpan<uint> mask, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @maskPtr = mask) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void AndNot(ReadOnlySpan<long> input, ReadOnlySpan<long> mask, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @maskPtr = mask) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }

    public static void AndNot(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> mask, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @maskPtr = mask) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@maskPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@maskPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@maskPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @maskPtr[i];
        }
    }
    #endregion

    #endregion

}
#endif
