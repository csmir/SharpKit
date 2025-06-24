#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace SharpKit.Performance;

public static unsafe class Intrinsics
{
    internal const int rb = 64, eb = 32, sb = 08, rw = 32, ew = 16, sw = 06, rd = 16, ed = 08, sd = 04, rq = 08, eq = 04, sq = 02;

    #region Arithmetic Operations

    #region Arithmetic(ReadOnlySpan<T> input, T value, Span<T> output)

    #region Add(ReadOnlySpan<T> input, T value, Span<T> output)
    public static void Add(ReadOnlySpan<byte> input, [ConstantExpected] byte value, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @out = output) fixed (byte* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<byte> values = Vector256.Create(value);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<byte> values = Vector128.Create(value);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (byte)(@in[i] + value);
        }
    }

    public static void Add(ReadOnlySpan<sbyte> input, [ConstantExpected] sbyte value, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<sbyte> values = Vector256.Create(value);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<sbyte> values = Vector128.Create(value);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (sbyte)(@in[i] + value);
        }
    }

    public static void Add(ReadOnlySpan<short> input, [ConstantExpected] short value, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @out = output) fixed (short* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<short> values = Vector256.Create(value);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<short> values = Vector128.Create(value);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (short)(@in[i] + value);
        }
    }

    public static void Add(ReadOnlySpan<ushort> input, [ConstantExpected] ushort value, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<ushort> values = Vector256.Create(value);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ushort> values = Vector128.Create(value);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (ushort)(@in[i] + value);
        }
    }

    public static void Add(ReadOnlySpan<int> input, [ConstantExpected] int value, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @out = output) fixed (int* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<int> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<int> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<int> values = Vector128.Create(value);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] + value;
        }
    }

    public static void Add(ReadOnlySpan<uint> input, [ConstantExpected] uint value, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @out = output) fixed (uint* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<uint> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<uint> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<uint> values = Vector128.Create(value);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] + value;
        }
    }

    public static void Add(ReadOnlySpan<long> input, [ConstantExpected] long value, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @out = output) fixed (long* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<long> values = Vector512.Create(value);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<long> values = Vector256.Create(value);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<long> values = Vector128.Create(value);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] + value;
        }
    }

    public static void Add(ReadOnlySpan<ulong> input, [ConstantExpected] ulong value, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<ulong> values = Vector512.Create(value);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<ulong> values = Vector256.Create(value);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ulong> values = Vector128.Create(value);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] + value;
        }
    }

    public static void Add(ReadOnlySpan<float> input, [ConstantExpected] float value, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @out = output) fixed (float* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<float> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<float> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<float> values = Vector128.Create(value);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] + value;
        }
    }

    public static void Add(ReadOnlySpan<double> input, [ConstantExpected] double value, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @out = output) fixed (double* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<double> values = Vector512.Create(value);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<double> values = Vector256.Create(value);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<double> values = Vector128.Create(value);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] + value;
        }
    }
    #endregion

    #region AddSaturate(ReadOnlySpan<T> input, T value, Span<T> output)
    public static void AddSaturate(ReadOnlySpan<byte> input, [ConstantExpected] byte value, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @out = output) fixed (byte* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<byte> values = Vector256.Create(value);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.AddSaturate(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<byte> values = Vector128.Create(value);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.AddSaturate(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (byte)Math.Max(byte.MaxValue, @in[i] + value);
        }
    }

    public static void AddSaturate(ReadOnlySpan<sbyte> input, [ConstantExpected] sbyte value, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<sbyte> values = Vector256.Create(value);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.AddSaturate(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<sbyte> values = Vector128.Create(value);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.AddSaturate(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (sbyte)Math.Max(sbyte.MaxValue, @in[i] + value);
        }
    }

    public static void AddSaturate(ReadOnlySpan<short> input, [ConstantExpected] short value, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @out = output) fixed (short* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<short> values = Vector256.Create(value);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.AddSaturate(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<short> values = Vector128.Create(value);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.AddSaturate(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (short)Math.Max(short.MaxValue, @in[i] + value);
        }
    }

    public static void AddSaturate(ReadOnlySpan<ushort> input, [ConstantExpected] ushort value, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<ushort> values = Vector256.Create(value);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.AddSaturate(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ushort> values = Vector128.Create(value);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.AddSaturate(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (ushort)Math.Max(ushort.MaxValue, @in[i] + value);
        }
    }
    #endregion

    #region Multiply(ReadOnlySpan<T> input, T value, Span<T> output)
    public static void Multiply(ReadOnlySpan<int> input, [ConstantExpected] int value, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @out = output) fixed (int* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<int> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Multiply(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<int> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Multiply(Avx.LoadVector256(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (long)@in[i] * value;
        }
    }

    public static void Multiply(ReadOnlySpan<uint> input, [ConstantExpected] uint value, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @out = output) fixed (uint* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<uint> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Multiply(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<uint> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Multiply(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<uint> values = Vector128.Create(value);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Multiply(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (ulong)@in[i] * value;
        }
    }

    public static void Multiply(ReadOnlySpan<float> input, [ConstantExpected] float value, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @out = output) fixed (float* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<float> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Multiply(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<float> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Multiply(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<float> values = Vector128.Create(value);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Multiply(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] * value;
        }
    }

    public static void Multiply(ReadOnlySpan<double> input, [ConstantExpected] double value, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @out = output) fixed (double* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<double> values = Vector512.Create(value);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Multiply(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<double> values = Vector256.Create(value);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Multiply(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<double> values = Vector128.Create(value);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Multiply(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] * value;
        }
    }
    #endregion

    #region MultiplyLow(ReadOnlySpan<T> input, T value, Span<T> output)
    public static void MultiplyLow(ReadOnlySpan<int> input, [ConstantExpected] int value, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @out = output) fixed (int* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<int> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.MultiplyLow(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<int> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.MultiplyLow(Avx.LoadVector256(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (int)((long)@in[i] * value);
        }
    }

    public static void MultiplyLow(ReadOnlySpan<uint> input, [ConstantExpected] uint value, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @out = output) fixed (uint* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<uint> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.MultiplyLow(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<uint> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.MultiplyLow(Avx.LoadVector256(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (uint)((ulong)@in[i] * value);
        }
    }
    #endregion

    #region Subtract(ReadOnlySpan<T> input, T value, Span<T> output)
    public static void Subtract(ReadOnlySpan<byte> input, [ConstantExpected] byte value, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @out = output) fixed (byte* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<byte> values = Vector256.Create(value);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.Subtract(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<byte> values = Vector128.Create(value);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (byte)(@in[i] - value);
        }
    }

    public static void Subtract(ReadOnlySpan<sbyte> input, [ConstantExpected] sbyte value, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<sbyte> values = Vector256.Create(value);
                for (; i <= length - eb; i += eb) Avx.Store(@out + i, Avx2.Subtract(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<sbyte> values = Vector128.Create(value);
                for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (sbyte)(@in[i] - value);
        }
    }

    public static void Subtract(ReadOnlySpan<short> input, [ConstantExpected] short value, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @out = output) fixed (short* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<short> values = Vector256.Create(value);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.Subtract(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<short> values = Vector128.Create(value);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (short)(@in[i] - value);
        }
    }

    public static void Subtract(ReadOnlySpan<ushort> input, [ConstantExpected] ushort value, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<ushort> values = Vector256.Create(value);
                for (; i <= length - ew; i += ew) Avx.Store(@out + i, Avx2.Subtract(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ushort> values = Vector128.Create(value);
                for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = (ushort)(@in[i] - value);
        }
    }

    public static void Subtract(ReadOnlySpan<int> input, [ConstantExpected] int value, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @out = output) fixed (int* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<int> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<int> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Subtract(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<int> values = Vector128.Create(value);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] - value;
        }
    }

    public static void Subtract(ReadOnlySpan<uint> input, [ConstantExpected] uint value, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @out = output) fixed (uint* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<uint> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<uint> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Subtract(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<uint> values = Vector128.Create(value);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] - value;
        }
    }

    public static void Subtract(ReadOnlySpan<long> input, [ConstantExpected] long value, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @out = output) fixed (long* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<long> values = Vector512.Create(value);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<long> values = Vector256.Create(value);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Subtract(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<long> values = Vector128.Create(value);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] - value;
        }
    }

    public static void Subtract(ReadOnlySpan<ulong> input, [ConstantExpected] ulong value, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<ulong> values = Vector512.Create(value);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<ulong> values = Vector256.Create(value);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Subtract(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<ulong> values = Vector128.Create(value);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] - value;
        }
    }

    public static void Subtract(ReadOnlySpan<float> input, [ConstantExpected] float value, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @out = output) fixed (float* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<float> values = Vector512.Create(value);
                for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<float> values = Vector256.Create(value);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Subtract(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<float> values = Vector128.Create(value);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] + value;
        }
    }

    public static void Subtract(ReadOnlySpan<double> input, [ConstantExpected] double value, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @out = output) fixed (double* @in = input)
        {
            if (Avx512F.IsSupported)
            {
                Vector512<double> values = Vector512.Create(value);
                for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), values));
            }
            else if (Avx2.IsSupported)
            {
                Vector256<double> values = Vector256.Create(value);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Subtract(Avx.LoadVector256(@in + i), values));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<double> values = Vector128.Create(value);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), values));
            }
            for (; i < length; i++) @out[i] = @in[i] + value;
        }
    }
    #endregion

    #endregion

    #region Arithmetic(ReadOnlySpan<T> input, ReadOnlySpan<T> values, Span<T> output)

    #region Add(ReadOnlySpan<T> input, ReadOnlySpan<T> values, Span<T> output)
    public static void Add(ReadOnlySpan<byte> input, ReadOnlySpan<byte> values, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @valuesPtr = values) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - eb; i += eb)  Avx.Store(@out + i, Avx2.Add(Avx.LoadVector256(@in + i),   Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (byte)(@in[i] + @valuesPtr[i]);
        }
    }

    public static void Add(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> values, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @valuesPtr = values) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - eb; i += eb)  Avx.Store(@out + i, Avx2.Add( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)(@in[i] + @valuesPtr[i]);
        }
    }

    public static void Add(ReadOnlySpan<short> input, ReadOnlySpan<short> values, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @valuesPtr = values) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ew; i += ew)  Avx.Store(@out + i, Avx2.Add( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (short)(@in[i] + @valuesPtr[i]);
        }
    }

    public static void Add(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> values, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @valuesPtr = values) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ew; i += ew)  Avx.Store(@out + i, Avx2.Add( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.Add(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)(@in[i] + @valuesPtr[i]);
        }
    }

    public static void Add(ReadOnlySpan<int> input, ReadOnlySpan<int> values, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @valuesPtr = values) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Add(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Add(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] + @valuesPtr[i];
        }
    }

    public static void Add(ReadOnlySpan<uint> input, ReadOnlySpan<uint> values, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @valuesPtr = values) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Add(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Add(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] + @valuesPtr[i];
        }
    }

    public static void Add(ReadOnlySpan<long> input, ReadOnlySpan<long> values, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @valuesPtr = values) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Add(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Add(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] + @valuesPtr[i];
        }
    }

    public static void Add(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> values, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @valuesPtr = values) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Add(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Add(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] + @valuesPtr[i];
        }
    }

    public static void Add(ReadOnlySpan<float> input, ReadOnlySpan<float> values, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @valuesPtr = values) fixed (float* @out = output) fixed (float* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Add(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Add(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] + @valuesPtr[i];
        }
    }

    public static void Add(ReadOnlySpan<double> input, ReadOnlySpan<double> values, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @valuesPtr = values) fixed (double* @out = output) fixed (double* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Add(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Add(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Add(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] + @valuesPtr[i];
        }
    }
    #endregion

    #region AddSaturate(ReadOnlySpan<T> input, ReadOnlySpan<T> values, Span<T> output)
    public static void AddSaturate(ReadOnlySpan<byte> input, ReadOnlySpan<byte> values, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @valuesPtr = values) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - eb; i += eb)  Avx.Store(@out + i, Avx2.AddSaturate( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.AddSaturate(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (byte)Math.Max(byte.MaxValue, @in[i] + valuesPtr[i]);
        }
    }

    public static void AddSaturate(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> values, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @valuesPtr = values) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - eb; i += eb)  Avx.Store(@out + i, Avx2.AddSaturate( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.AddSaturate(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)Math.Max(sbyte.MaxValue, @in[i] + valuesPtr[i]);
        }
    }

    public static void AddSaturate(ReadOnlySpan<short> input, ReadOnlySpan<short> values, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @valuesPtr = values) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ew; i += ew)  Avx.Store(@out + i, Avx2.AddSaturate( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.AddSaturate(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (short)Math.Max(short.MaxValue, @in[i] + valuesPtr[i]);
        }
    }

    public static void AddSaturate(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> values, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @valuesPtr = values) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ew; i += ew)  Avx.Store(@out + i, Avx2.AddSaturate( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.AddSaturate(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)Math.Max(ushort.MaxValue, @in[i] + valuesPtr[i]);
        }
    }
    #endregion

    #region Multiply(ReadOnlySpan<T> input, ReadOnlySpan<T> values, Span<T> output)
    public static void Multiply(ReadOnlySpan<int> input, ReadOnlySpan<int> values, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @valuesPtr = values) fixed (long* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Multiply(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Multiply(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (long)@in[i] * @valuesPtr[i];
        }
    }

    public static void Multiply(ReadOnlySpan<uint> input, ReadOnlySpan<uint> values, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @valuesPtr = values) fixed (ulong* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Multiply(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Multiply(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Multiply(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (ulong)@in[i] * @valuesPtr[i];
        }
    }
 
    public static void Multiply(ReadOnlySpan<float> input, ReadOnlySpan<float> values, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @valuesPtr = values) fixed (float* @out = output) fixed (float* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Multiply(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Multiply(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Multiply(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] * @valuesPtr[i];
        }
    }

    public static void Multiply(ReadOnlySpan<double> input, ReadOnlySpan<double> values, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @valuesPtr = values) fixed (double* @out = output) fixed (double* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Multiply(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Multiply(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Multiply(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] * @valuesPtr[i];
        }
    }
   #endregion

    #region MultiplyLow(ReadOnlySpan<T> input, ReadOnlySpan<T> values, Span<T> output)
    public static void MultiplyLow(ReadOnlySpan<int> input, ReadOnlySpan<int> values, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @valuesPtr = values) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.MultiplyLow(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.MultiplyLow(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (int)((long)@in[i] * @valuesPtr[i]);
        }
    }

    public static void MultiplyLow(ReadOnlySpan<uint> input, ReadOnlySpan<uint> values, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @valuesPtr = values) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.MultiplyLow(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.MultiplyLow(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (uint)((ulong)@in[i] * @valuesPtr[i]);
        }
    }
    #endregion

    #region Subtract(ReadOnlySpan<T> input, ReadOnlySpan<T> values, Span<T> output)
    public static void Subtract(ReadOnlySpan<byte> input, ReadOnlySpan<byte> values, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @valuesPtr = values) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - eb; i += eb)  Avx.Store(@out + i, Avx2.Subtract( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (byte)(@in[i] - @valuesPtr[i]);
        }
    }

    public static void Subtract(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> values, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @valuesPtr = values) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - eb; i += eb)  Avx.Store(@out + i, Avx2.Subtract( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sb; i += sb) Sse2.Store(@out + i, Sse2.Subtract(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)(@in[i] - @valuesPtr[i]);
        }
    }

    public static void Subtract(ReadOnlySpan<short> input, ReadOnlySpan<short> values, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @valuesPtr = values) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.Subtract(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.Subtract(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (short)(@in[i] - @valuesPtr[i]);
        }
    }

    public static void Subtract(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> values, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @valuesPtr = values) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.Subtract(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.Subtract(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)(@in[i] - @valuesPtr[i]);
        }
    }

    public static void Subtract(ReadOnlySpan<int> input, ReadOnlySpan<int> values, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @valuesPtr = values) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Subtract(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Subtract(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] - @valuesPtr[i];
        }
    }

    public static void Subtract(ReadOnlySpan<uint> input, ReadOnlySpan<uint> values, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @valuesPtr = values) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Subtract(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Subtract(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] - @valuesPtr[i];
        }
    }

    public static void Subtract(ReadOnlySpan<long> input, ReadOnlySpan<long> values, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @valuesPtr = values) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Subtract(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Subtract(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] - @valuesPtr[i];
        }
    }

    public static void Subtract(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> values, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @valuesPtr = values) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Subtract(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Subtract(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] - @valuesPtr[i];
        }
    }

    public static void Subtract(ReadOnlySpan<float> input, ReadOnlySpan<float> values, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @valuesPtr = values) fixed (float* @out = output) fixed (float* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Subtract(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Subtract(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] - @valuesPtr[i];
        }
    }

    public static void Subtract(ReadOnlySpan<double> input, ReadOnlySpan<double> values, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @valuesPtr = values) fixed (double* @out = output) fixed (double* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Subtract(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@valuesPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Subtract(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@valuesPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Subtract(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@valuesPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] - @valuesPtr[i];
        }
    }
    #endregion

    #endregion

    #endregion Arithmetic

    #region Bitwise Operations

    #region Bitwise(ReadOnlySpan<T> input, T mask, Span<T> output)

    #region And(ReadOnlySpan<T> input, T mask, Span<T> output)
    public static void And(ReadOnlySpan<byte> input, [ConstantExpected] byte mask, Span<byte> output)
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

    public static void And(ReadOnlySpan<sbyte> input, [ConstantExpected] sbyte mask, Span<sbyte> output)
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

    public static void And(ReadOnlySpan<short> input, [ConstantExpected] short mask, Span<short> output)
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

    public static void And(ReadOnlySpan<ushort> input, [ConstantExpected] ushort mask, Span<ushort> output)
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

    public static void And(ReadOnlySpan<int> input, [ConstantExpected] int mask, Span<int> output)
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

    public static void And(ReadOnlySpan<uint> input, [ConstantExpected] uint mask, Span<uint> output)
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

    public static void And(ReadOnlySpan<long> input, [ConstantExpected] long mask, Span<long> output)
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

    public static void And(ReadOnlySpan<ulong> input, [ConstantExpected] ulong mask, Span<ulong> output)
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

    public static void And(ReadOnlySpan<float> input, [ConstantExpected] float mask, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @out = output) fixed (float* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<float> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.And(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<float> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = Unsafe.BitCast<uint, float>(Unsafe.BitCast<float, uint>(@in[i]) & Unsafe.BitCast<float, uint>(mask));
        }
    }

    public static void And(ReadOnlySpan<double> input, [ConstantExpected] double mask, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @out = output) fixed (double* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<double> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.And(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<double> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = Unsafe.BitCast<ulong, double>(Unsafe.BitCast<double, ulong>(@in[i]) & Unsafe.BitCast<double, ulong>(mask));
        }
    }
    #endregion

    #region Or(ReadOnlySpan<T> input, T mask, Span<T> output)
    public static void Or(ReadOnlySpan<byte> input, [ConstantExpected] byte mask, Span<byte> output)
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
            for (; i < length; i++) @out[i] = (byte)(@in[i] | mask);
        }
    }

    public static void Or(ReadOnlySpan<sbyte> input, [ConstantExpected] sbyte mask, Span<sbyte> output)
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
            for (; i < length; i++) @out[i] = (sbyte)(@in[i] | mask);
        }
    }

    public static void Or(ReadOnlySpan<short> input, [ConstantExpected] short mask, Span<short> output)
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
            for (; i < length; i++) @out[i] = (short)(@in[i] | mask);
        }
    }

    public static void Or(ReadOnlySpan<ushort> input, [ConstantExpected] ushort mask, Span<ushort> output)
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
            for (; i < length; i++) @out[i] = (ushort)(@in[i] | mask);
        }
    }

    public static void Or(ReadOnlySpan<int> input, [ConstantExpected] int mask, Span<int> output)
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
            for (; i < length; i++) @out[i] = @in[i] | mask;
        }
    }

    public static void Or(ReadOnlySpan<uint> input, [ConstantExpected] uint mask, Span<uint> output)
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
            for (; i < length; i++) @out[i] = @in[i] | mask;
        }
    }

    public static void Or(ReadOnlySpan<long> input, [ConstantExpected] long mask, Span<long> output)
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
            for (; i < length; i++) @out[i] = @in[i] | mask;
        }
    }

    public static void Or(ReadOnlySpan<ulong> input, [ConstantExpected] ulong mask, Span<ulong> output)
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
            for (; i < length; i++) @out[i] = @in[i] | mask;
        }
    }

    public static void Or(ReadOnlySpan<float> input, [ConstantExpected] float mask, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @out = output) fixed (float* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<float> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Or(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<float> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = Unsafe.BitCast<uint, float>(Unsafe.BitCast<float, uint>(@in[i]) | Unsafe.BitCast<float, uint>(mask));
        }
    }

    public static void Or(ReadOnlySpan<double> input, [ConstantExpected] double mask, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @out = output) fixed (double* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<double> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Or(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<double> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = Unsafe.BitCast<ulong, double>(Unsafe.BitCast<double, ulong>(@in[i]) | Unsafe.BitCast<double, ulong>(mask));
        }
    }
    #endregion

    #region Xor(ReadOnlySpan<T> input, T mask, Span<T> output)
    public static void Xor(ReadOnlySpan<byte> input, [ConstantExpected] byte mask, Span<byte> output)
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
            for (; i < length; i++) @out[i] = (byte)(@in[i] ^ mask);
        }
    }

    public static void Xor(ReadOnlySpan<sbyte> input, [ConstantExpected] sbyte mask, Span<sbyte> output)
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
            for (; i < length; i++) @out[i] = (sbyte)(@in[i] ^ mask);
        }
    }

    public static void Xor(ReadOnlySpan<short> input, [ConstantExpected] short mask, Span<short> output)
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
            for (; i < length; i++) @out[i] = (short)(@in[i] ^ mask);
        }
    }

    public static void Xor(ReadOnlySpan<ushort> input, [ConstantExpected] ushort mask, Span<ushort> output)
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
            for (; i < length; i++) @out[i] = (ushort)(@in[i] ^ mask);
        }
    }

    public static void Xor(ReadOnlySpan<int> input, [ConstantExpected] int mask, Span<int> output)
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
            for (; i < length; i++) @out[i] = @in[i] ^ mask;
        }
    }

    public static void Xor(ReadOnlySpan<uint> input, [ConstantExpected] uint mask, Span<uint> output)
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
            for (; i < length; i++) @out[i] = @in[i] ^ mask;
        }
    }

    public static void Xor(ReadOnlySpan<long> input, [ConstantExpected] long mask, Span<long> output)
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
            for (; i < length; i++) @out[i] = @in[i] ^ mask;
        }
    }

    public static void Xor(ReadOnlySpan<ulong> input, [ConstantExpected] ulong mask, Span<ulong> output)
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
            for (; i < length; i++) @out[i] = @in[i] ^ mask;
        }
    }

    public static void Xor(ReadOnlySpan<float> input, [ConstantExpected] float mask, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @out = output) fixed (float* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<float> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.Xor(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<float> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = Unsafe.BitCast<uint, float>(Unsafe.BitCast<float, uint>(@in[i]) ^ Unsafe.BitCast<float, uint>(mask));
        }
    }

    public static void Xor(ReadOnlySpan<double> input, [ConstantExpected] double mask, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @out = output) fixed (double* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<double> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.Xor(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<double> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = Unsafe.BitCast<ulong, double>(Unsafe.BitCast<double, ulong>(@in[i]) ^ Unsafe.BitCast<double, ulong>(mask));
        }
    }
    #endregion

    #region AndNot(ReadOnlySpan<T> input, T mask, Span<T> output)
    public static void AndNot(ReadOnlySpan<byte> input, [ConstantExpected] byte mask, Span<byte> output)
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
            for (; i < length; i++) @out[i] = (byte)(@in[i] &~ mask);
        }
    }

    public static void AndNot(ReadOnlySpan<sbyte> input, [ConstantExpected] sbyte mask, Span<sbyte> output)
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
            for (; i < length; i++) @out[i] = (sbyte)(@in[i] &~ mask);
        }
    }

    public static void AndNot(ReadOnlySpan<short> input, [ConstantExpected] short mask, Span<short> output)
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
            for (; i < length; i++) @out[i] = (short)(@in[i] &~ mask);
        }
    }

    public static void AndNot(ReadOnlySpan<ushort> input, [ConstantExpected] ushort mask, Span<ushort> output)
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
            for (; i < length; i++) @out[i] = (ushort)(@in[i] &~ mask);
        }
    }

    public static void AndNot(ReadOnlySpan<int> input, [ConstantExpected] int mask, Span<int> output)
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
            for (; i < length; i++) @out[i] = @in[i] &~ mask;
        }
    }

    public static void AndNot(ReadOnlySpan<uint> input, [ConstantExpected] uint mask, Span<uint> output)
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
            for (; i < length; i++) @out[i] = @in[i] &~ mask;
        }
    }

    public static void AndNot(ReadOnlySpan<long> input, [ConstantExpected] long mask, Span<long> output)
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
            for (; i < length; i++) @out[i] = @in[i] &~ mask;
        }
    }

    public static void AndNot(ReadOnlySpan<ulong> input, [ConstantExpected] ulong mask, Span<ulong> output)
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
            for (; i < length; i++) @out[i] = @in[i] &~ mask;
        }
    }

    public static void AndNot(ReadOnlySpan<float> input, [ConstantExpected] float mask, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @out = output) fixed (float* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<float> masks = Vector256.Create(mask);
                for (; i <= length - ed; i += ed) Avx.Store(@out + i, Avx2.AndNot(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<float> masks = Vector128.Create(mask);
                for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = Unsafe.BitCast<uint, float>(Unsafe.BitCast<float, uint>(@in[i]) &~ Unsafe.BitCast<float, uint>(mask));
        }
    }

    public static void AndNot(ReadOnlySpan<double> input, [ConstantExpected] double mask, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @out = output) fixed (double* @in = input)
        {
            if (Avx2.IsSupported)
            {
                Vector256<double> masks = Vector256.Create(mask);
                for (; i <= length - eq; i += eq) Avx.Store(@out + i, Avx2.AndNot(Avx.LoadVector256(@in + i), masks));
            }
            else if (Sse2.IsSupported)
            {
                Vector128<double> masks = Vector128.Create(mask);
                for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), masks));
            }
            for (; i < length; i++) @out[i] = Unsafe.BitCast<ulong, double>(Unsafe.BitCast<double, ulong>(@in[i]) &~ Unsafe.BitCast<double, ulong>(mask));
        }
    }
    #endregion

    #endregion

    #region Bitwise(ReadOnlySpan<T> input, ReadOnlySpan<T> masks, Span<T> output)

    #region And(ReadOnlySpan<T> input, ReadOnlySpan<T> masks, Span<T> output)
    public static void And(ReadOnlySpan<byte> input, ReadOnlySpan<byte> masks, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @masksPtr = masks) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (byte)(@in[i] & @masksPtr[i]);
        }
    }

    public static void And(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> masks, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @masksPtr = masks) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)(@in[i] & @masksPtr[i]);
        }
    }

    public static void And(ReadOnlySpan<short> input, ReadOnlySpan<short> masks, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @masksPtr = masks) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (short)(@in[i] & @masksPtr[i]);
        }
    }

    public static void And(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> masks, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @masksPtr = masks) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)(@in[i] & @masksPtr[i]);
        }
    }

    public static void And(ReadOnlySpan<int> input, ReadOnlySpan<int> masks, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @masksPtr = masks) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @masksPtr[i];
        }
    }

    public static void And(ReadOnlySpan<uint> input, ReadOnlySpan<uint> masks, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @masksPtr = masks) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @masksPtr[i];
        }
    }

    public static void And(ReadOnlySpan<long> input, ReadOnlySpan<long> masks, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @masksPtr = masks) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @masksPtr[i];
        }
    }

    public static void And(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> masks, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @masksPtr = masks) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.And(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.And(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.And(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] & @masksPtr[i];
        }
    }

    public static void And(ReadOnlySpan<float> input, ReadOnlySpan<float> masks, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @masksPtr = masks) fixed (float* @out = output) fixed (float* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ed; i += ed)  Avx.Store(@out + i, Avx2.And( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@masksPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = Unsafe.BitCast<uint, float>(Unsafe.BitCast<float, uint>(@in[i]) & Unsafe.BitCast<float, uint>(@masksPtr[i]));
        }
    }

    public static void And(ReadOnlySpan<double> input, ReadOnlySpan<double> masks, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @masksPtr = masks) fixed (double* @out = output) fixed (double* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - eq; i += eq)  Avx.Store(@out + i, Avx2.And( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@masksPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.And(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = Unsafe.BitCast<ulong, double>(Unsafe.BitCast<double, ulong>(@in[i]) & Unsafe.BitCast<double, ulong>(@masksPtr[i]));
        }
    }
    #endregion

    #region Or(ReadOnlySpan<T> input, ReadOnlySpan<T> masks, Span<T> output)
    public static void Or(ReadOnlySpan<byte> input, ReadOnlySpan<byte> masks, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @masksPtr = masks) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (byte)(@in[i] | @masksPtr[i]);
        }
    }

    public static void Or(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> masks, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @masksPtr = masks) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)(@in[i] | @masksPtr[i]);
        }
    }

    public static void Or(ReadOnlySpan<short> input, ReadOnlySpan<short> masks, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @masksPtr = masks) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (short)(@in[i] | @masksPtr[i]);
        }
    }

    public static void Or(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> masks, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @masksPtr = masks) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)(@in[i] | @masksPtr[i]);
        }
    }

    public static void Or(ReadOnlySpan<int> input, ReadOnlySpan<int> masks, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @masksPtr = masks) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] | @masksPtr[i];
        }
    }

    public static void Or(ReadOnlySpan<uint> input, ReadOnlySpan<uint> masks, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @masksPtr = masks) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] | @masksPtr[i];
        }
    }

    public static void Or(ReadOnlySpan<long> input, ReadOnlySpan<long> masks, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @masksPtr = masks) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] | @masksPtr[i];
        }
    }

    public static void Or(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> masks, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @masksPtr = masks) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Or(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Or(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Or(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] | @masksPtr[i];
        }
    }

    public static void Or(ReadOnlySpan<float> input, ReadOnlySpan<float> masks, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @masksPtr = masks) fixed (float* @out = output) fixed (float* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ed; i += ed)  Avx.Store(@out + i, Avx2.Or( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@masksPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = Unsafe.BitCast<uint, float>(Unsafe.BitCast<float, uint>(@in[i]) | Unsafe.BitCast<float, uint>(@masksPtr[i]));
        }
    }

    public static void Or(ReadOnlySpan<double> input, ReadOnlySpan<double> masks, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @masksPtr = masks) fixed (double* @out = output) fixed (double* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - eq; i += eq)  Avx.Store(@out + i, Avx2.Or( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@masksPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Or(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = Unsafe.BitCast<ulong, double>(Unsafe.BitCast<double, ulong>(@in[i]) | Unsafe.BitCast<double, ulong>(@masksPtr[i]));
        }
    }
    #endregion

    #region Xor(ReadOnlySpan<T> input, ReadOnlySpan<T> masks, Span<T> output)
    public static void Xor(ReadOnlySpan<byte> input, ReadOnlySpan<byte> masks, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @masksPtr = masks) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (byte)(@in[i] ^ @masksPtr[i]);
        }
    }

    public static void Xor(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> masks, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @masksPtr = masks) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)(@in[i] ^ @masksPtr[i]);
        }
    }

    public static void Xor(ReadOnlySpan<short> input, ReadOnlySpan<short> masks, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @masksPtr = masks) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (short)(@in[i] ^ @masksPtr[i]);
        }
    }

    public static void Xor(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> masks, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @masksPtr = masks) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)(@in[i] ^ @masksPtr[i]);
        }
    }

    public static void Xor(ReadOnlySpan<int> input, ReadOnlySpan<int> masks, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @masksPtr = masks) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] ^ @masksPtr[i];
        }
    }

    public static void Xor(ReadOnlySpan<uint> input, ReadOnlySpan<uint> masks, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @masksPtr = masks) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] ^ @masksPtr[i];
        }
    }

    public static void Xor(ReadOnlySpan<long> input, ReadOnlySpan<long> masks, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @masksPtr = masks) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] ^ @masksPtr[i];
        }
    }

    public static void Xor(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> masks, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @masksPtr = masks) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.Xor(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.Xor(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.Xor(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] ^ @masksPtr[i];
        }
    }

    public static void Xor(ReadOnlySpan<float> input, ReadOnlySpan<float> masks, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @masksPtr = masks) fixed (float* @out = output) fixed (float* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ed; i += ed)  Avx.Store(@out + i, Avx2.Xor( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@masksPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = Unsafe.BitCast<uint, float>(Unsafe.BitCast<float, uint>(@in[i]) ^ Unsafe.BitCast<float, uint>(@masksPtr[i]));
        }
    }

    public static void Xor(ReadOnlySpan<double> input, ReadOnlySpan<double> masks, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @masksPtr = masks) fixed (double* @out = output) fixed (double* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - eq; i += eq)  Avx.Store(@out + i, Avx2.Xor( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@masksPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.Xor(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = Unsafe.BitCast<ulong, double>(Unsafe.BitCast<double, ulong>(@in[i]) ^ Unsafe.BitCast<double, ulong>(@masksPtr[i]));
        }
    }
    #endregion

    #region AndNot(ReadOnlySpan<T> input, ReadOnlySpan<T> masks, Span<T> output)
    public static void AndNot(ReadOnlySpan<byte> input, ReadOnlySpan<byte> masks, Span<byte> output)
    {
        int length = input.Length, i = 0;
        fixed (byte* @masksPtr = masks) fixed (byte* @out = output) fixed (byte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (byte)(@in[i] &~ @masksPtr[i]);
        }
    }

    public static void AndNot(ReadOnlySpan<sbyte> input, ReadOnlySpan<sbyte> masks, Span<sbyte> output)
    {
        int length = input.Length, i = 0;
        fixed (sbyte* @masksPtr = masks) fixed (sbyte* @out = output) fixed (sbyte* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rb; i += rb) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eb; i += eb)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sb; i += sb)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (sbyte)(@in[i] &~ @masksPtr[i]);
        }
    }

    public static void AndNot(ReadOnlySpan<short> input, ReadOnlySpan<short> masks, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @masksPtr = masks) fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (short)(@in[i] &~ @masksPtr[i]);
        }
    }

    public static void AndNot(ReadOnlySpan<ushort> input, ReadOnlySpan<ushort> masks, Span<ushort> output)
    {
        int length = input.Length, i = 0;
        fixed (ushort* @masksPtr = masks) fixed (ushort* @out = output) fixed (ushort* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rw; i += rw) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ew; i += ew)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sw; i += sw)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = (ushort)(@in[i] &~ @masksPtr[i]);
        }
    }

    public static void AndNot(ReadOnlySpan<int> input, ReadOnlySpan<int> masks, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @masksPtr = masks) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] &~ @masksPtr[i];
        }
    }

    public static void AndNot(ReadOnlySpan<uint> input, ReadOnlySpan<uint> masks, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @masksPtr = masks) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] &~ @masksPtr[i];
        }
    }

    public static void AndNot(ReadOnlySpan<long> input, ReadOnlySpan<long> masks, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @masksPtr = masks) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] &~ @masksPtr[i];
        }
    }

    public static void AndNot(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> masks, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @masksPtr = masks) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.AndNot(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@masksPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.AndNot(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@masksPtr + i)));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.AndNot(   Sse2.LoadVector128(@in + i),    Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] &~ @masksPtr[i];
        }
    }

    public static void AndNot(ReadOnlySpan<float> input, ReadOnlySpan<float> masks, Span<float> output)
    {
        int length = input.Length, i = 0;
        fixed (float* @masksPtr = masks) fixed (float* @out = output) fixed (float* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ed; i += ed)  Avx.Store(@out + i, Avx2.AndNot( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@masksPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sd; i += sd) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = Unsafe.BitCast<uint, float>(Unsafe.BitCast<float, uint>(@in[i]) &~ Unsafe.BitCast<float, uint>(@masksPtr[i]));
        }
    }

    public static void AndNot(ReadOnlySpan<double> input, ReadOnlySpan<double> masks, Span<double> output)
    {
        int length = input.Length, i = 0;
        fixed (double* @masksPtr = masks) fixed (double* @out = output) fixed (double* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - eq; i += eq)  Avx.Store(@out + i, Avx2.AndNot( Avx.LoadVector256(@in + i),  Avx.LoadVector256(@masksPtr + i)));
            else if (Sse2.IsSupported) for (; i <= length - sq; i += sq) Sse2.Store(@out + i, Sse2.AndNot(Sse2.LoadVector128(@in + i), Sse2.LoadVector128(@masksPtr + i)));

            for (; i < length; i++) @out[i] = Unsafe.BitCast<ulong, double>(Unsafe.BitCast<double, ulong>(@in[i]) &~ Unsafe.BitCast<double, ulong>(@masksPtr[i]));
        }
    }
    #endregion

    #endregion

    #endregion

    #region Shift Operations

    #region ShiftLeftLogical(ReadOnlySpan<T> input, T count, Span<T> output)
    public static void ShiftLeftLogical(ReadOnlySpan<int> input, [ConstantExpected] byte count, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftLeftLogical(Avx512F.LoadVector512(@in + i), count));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftLeftLogical(    Avx.LoadVector256(@in + i), count));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.ShiftLeftLogical(   Sse2.LoadVector128(@in + i), count));
            for (; i < length; i++) @out[i] = @in[i] << count;
        }
    }

    public static void ShiftLeftLogical(ReadOnlySpan<uint> input, [ConstantExpected] byte count, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftLeftLogical(Avx512F.LoadVector512(@in + i), count));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftLeftLogical(    Avx.LoadVector256(@in + i), count));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.ShiftLeftLogical(   Sse2.LoadVector128(@in + i), count));
            for (; i < length; i++) @out[i] = @in[i] << count;
        }
    }

    public static void ShiftLeftLogical(ReadOnlySpan<long> input, [ConstantExpected] byte count, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.ShiftLeftLogical(Avx512F.LoadVector512(@in + i), count));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.ShiftLeftLogical(    Avx.LoadVector256(@in + i), count));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.ShiftLeftLogical(   Sse2.LoadVector128(@in + i), count));
            for (; i < length; i++) @out[i] = @in[i] << count;
        }
    }

    public static void ShiftLeftLogical(ReadOnlySpan<ulong> input, [ConstantExpected] byte count, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.ShiftLeftLogical(Avx512F.LoadVector512(@in + i), count));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.ShiftLeftLogical(    Avx.LoadVector256(@in + i), count));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.ShiftLeftLogical(   Sse2.LoadVector128(@in + i), count));
            for (; i < length; i++) @out[i] = @in[i] << count;
        }
    }
    #endregion

    #region ShiftLeftLogicalVariable(ReadOnlySpan<T> input, ReadOnlySpan<T> counts, Span<T> output)
    public static void ShiftLeftLogicalVariable(ReadOnlySpan<int> input, ReadOnlySpan<uint> counts, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @countsPtr = counts) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftLeftLogicalVariable(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@countsPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftLeftLogicalVariable(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@countsPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] << (int)@countsPtr[i];
        }
    }

    public static void ShiftLeftLogicalVariable(ReadOnlySpan<uint> input, ReadOnlySpan<uint> counts, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @countsPtr = counts) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftLeftLogicalVariable(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@countsPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftLeftLogicalVariable(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@countsPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] << (int)@countsPtr[i];
        }
    }

    public static void ShiftLeftLogicalVariable(ReadOnlySpan<long> input, ReadOnlySpan<ulong> counts, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @countsPtr = counts) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftLeftLogicalVariable(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@countsPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftLeftLogicalVariable(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@countsPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] << (int)@countsPtr[i];
        }
    }

    public static void ShiftLeftLogicalVariable(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> counts, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @countsPtr = counts) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftLeftLogicalVariable(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@countsPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftLeftLogicalVariable(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@countsPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] << (int)@countsPtr[i];
        }
    }
    #endregion

    #region ShiftRightLogical(ReadOnlySpan<T> input, T count, Span<T> output)
    public static void ShiftRightLogical(ReadOnlySpan<int> input, [ConstantExpected] byte count, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftRightLogical(Avx512F.LoadVector512(@in + i), count));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftRightLogical(    Avx.LoadVector256(@in + i), count));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.ShiftRightLogical(   Sse2.LoadVector128(@in + i), count));
            for (; i < length; i++) @out[i] = @in[i] >>> count;
        }
    }

    public static void ShiftRightLogical(ReadOnlySpan<uint> input, [ConstantExpected] byte count, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftRightLogical(Avx512F.LoadVector512(@in + i), count));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftRightLogical(    Avx.LoadVector256(@in + i), count));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.ShiftRightLogical(   Sse2.LoadVector128(@in + i), count));
            for (; i < length; i++) @out[i] = @in[i] >>> count;
        }
    }

    public static void ShiftRightLogical(ReadOnlySpan<long> input, [ConstantExpected] byte count, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.ShiftRightLogical(Avx512F.LoadVector512(@in + i), count));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.ShiftRightLogical(    Avx.LoadVector256(@in + i), count));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.ShiftRightLogical(   Sse2.LoadVector128(@in + i), count));
            for (; i < length; i++) @out[i] = @in[i] >>> count;
        }
    }

    public static void ShiftRightLogical(ReadOnlySpan<ulong> input, [ConstantExpected] byte count, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.ShiftRightLogical(Avx512F.LoadVector512(@in + i), count));
            else if    (Avx2.IsSupported) for (; i <= length - eq; i += eq)     Avx.Store(@out + i,    Avx2.ShiftRightLogical(    Avx.LoadVector256(@in + i), count));
            else if    (Sse2.IsSupported) for (; i <= length - sq; i += sq)    Sse2.Store(@out + i,    Sse2.ShiftRightLogical(   Sse2.LoadVector128(@in + i), count));
            for (; i < length; i++) @out[i] = @in[i] >>> count;
        }
    }
    #endregion

    #region ShiftRightLogicalVariable(ReadOnlySpan<T> input, ReadOnlySpan<T> counts, Span<T> output)
    public static void ShiftRightLogicalVariable(ReadOnlySpan<int> input, ReadOnlySpan<uint> counts, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @countsPtr = counts) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftRightLogicalVariable(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@countsPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftRightLogicalVariable(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@countsPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] >>> (int)@countsPtr[i];
        }
    }

    public static void ShiftRightLogicalVariable(ReadOnlySpan<uint> input, ReadOnlySpan<uint> counts, Span<uint> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @countsPtr = counts) fixed (uint* @out = output) fixed (uint* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftRightLogicalVariable(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@countsPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftRightLogicalVariable(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@countsPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] >>> (int)@countsPtr[i];
        }
    }

    public static void ShiftRightLogicalVariable(ReadOnlySpan<long> input, ReadOnlySpan<ulong> counts, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @countsPtr = counts) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftRightLogicalVariable(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@countsPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftRightLogicalVariable(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@countsPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] >>> (int)@countsPtr[i];
        }
    }

    public static void ShiftRightLogicalVariable(ReadOnlySpan<ulong> input, ReadOnlySpan<ulong> counts, Span<ulong> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @countsPtr = counts) fixed (ulong* @out = output) fixed (ulong* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftRightLogicalVariable(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@countsPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftRightLogicalVariable(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@countsPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] >>> (int)@countsPtr[i];
        }
    }
    #endregion

    #region ShiftRightArithmetic(ReadOnlySpan<T> input, T count, Span<T> output)
    public static void ShiftRightArithmetic(ReadOnlySpan<short> input, [ConstantExpected] byte count, Span<short> output)
    {
        int length = input.Length, i = 0;
        fixed (short* @out = output) fixed (short* @in = input)
        {
                 if (Avx2.IsSupported) for (; i <= length - ew; i += ew)  Avx.Store(@out + i, Avx2.ShiftRightArithmetic( Avx.LoadVector256(@in + i), count));
            else if (Sse2.IsSupported) for (; i <= length - sw; i += sw) Sse2.Store(@out + i, Sse2.ShiftRightArithmetic(Sse2.LoadVector128(@in + i), count));
            for (; i < length; i++) @out[i] = (short)(@in[i] >> count);
        }
    }

    public static void ShiftRightArithmetic(ReadOnlySpan<int> input, [ConstantExpected] byte count, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftRightArithmetic(Avx512F.LoadVector512(@in + i), count));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftRightArithmetic(    Avx.LoadVector256(@in + i), count));
            else if    (Sse2.IsSupported) for (; i <= length - sd; i += sd)    Sse2.Store(@out + i,    Sse2.ShiftRightArithmetic(   Sse2.LoadVector128(@in + i), count));
            for (; i < length; i++) @out[i] = @in[i] >> count;
        }
    }

    public static void ShiftRightArithmetic(ReadOnlySpan<long> input, [ConstantExpected] byte count, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (long* @out = output) fixed (long* @in = input)
        {
            if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.ShiftRightArithmetic(Avx512F.LoadVector512(@in + i), count));
            for (; i < length; i++) @out[i] = @in[i] >> count;
        }
    }
    #endregion

    #region ShiftRightArithmeticVariable(ReadOnlySpan<T> input, ReadOnlySpan<T> counts, Span<T> output)
    public static void ShiftRightArithmeticVariable(ReadOnlySpan<int> input, ReadOnlySpan<uint> counts, Span<int> output)
    {
        int length = input.Length, i = 0;
        fixed (uint* @countsPtr = counts) fixed (int* @out = output) fixed (int* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rd; i += rd) Avx512F.Store(@out + i, Avx512F.ShiftRightArithmeticVariable(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@countsPtr + i)));
            else if    (Avx2.IsSupported) for (; i <= length - ed; i += ed)     Avx.Store(@out + i,    Avx2.ShiftRightArithmeticVariable(    Avx.LoadVector256(@in + i),     Avx.LoadVector256(@countsPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] >> (int)@countsPtr[i];
        }
    }

    public static void ShiftRightArithmeticVariable(ReadOnlySpan<long> input, ReadOnlySpan<ulong> counts, Span<long> output)
    {
        int length = input.Length, i = 0;
        fixed (ulong* @countsPtr = counts) fixed (long* @out = output) fixed (long* @in = input)
        {
                 if (Avx512F.IsSupported) for (; i <= length - rq; i += rq) Avx512F.Store(@out + i, Avx512F.ShiftRightArithmeticVariable(Avx512F.LoadVector512(@in + i), Avx512F.LoadVector512(@countsPtr + i)));

            for (; i < length; i++) @out[i] = @in[i] >> (int)@countsPtr[i];
        }
    }
    #endregion

    #endregion
}
#endif
