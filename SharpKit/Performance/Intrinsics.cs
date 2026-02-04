#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace SharpKit.Performance;

public static unsafe class Intrinsics
{
    private const int v512x1 = 64, v256x1 = 32, v128x1 = 08, v512x2 = 32, v256x2 = 16, v128x2 = 06, v512x4 = 16, v256x4 = 08, v128x4 = 04, v512x8 = 08, v256x8 = 04, v128x8 = 02;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static TTo BitCast<TFrom, TTo>(TFrom value) where TFrom : unmanaged where TTo : unmanaged => *(TTo*)(void*)&value;

#if !NET6_0_OR_GREATER
    // This attribute does nothing.
    private class ConstantExpectedAttribute : Attribute;
#endif

    #region Add(T* input, T value, T* output, int length)
    public static void Add(byte* input, [ConstantExpected] byte value, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && length >= v256x1)
        {
            Vector256<byte> values = Vector256.Create(value);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<byte> values = Vector128.Create(value);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] + value);
    }

    public static void Add(sbyte* input, [ConstantExpected] sbyte value, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<sbyte> values = Vector256.Create(value);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<sbyte> values = Vector128.Create(value);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] + value);
    }

    public static void Add(short* input, [ConstantExpected] short value, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<short> values = Vector256.Create(value);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<short> values = Vector128.Create(value);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (short)(input[i] + value);
    }

    public static void Add(ushort* input, [ConstantExpected] ushort value, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<ushort> values = Vector256.Create(value);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<ushort> values = Vector128.Create(value);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] + value);
    }

    public static void Add(int* input, [ConstantExpected] int value, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<int> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<int> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<int> values = Vector128.Create(value);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] + value;
    }

    public static void Add(uint* input, [ConstantExpected] uint value, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<uint> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<uint> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<uint> values = Vector128.Create(value);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] + value;
    }

    public static void Add(long* input, [ConstantExpected] long value, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<long> values = Vector512.Create(value);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<long> values = Vector256.Create(value);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<long> values = Vector128.Create(value);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] + value;
    }

    public static void Add(ulong* input, [ConstantExpected] ulong value, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<ulong> values = Vector512.Create(value);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<ulong> values = Vector256.Create(value);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<ulong> values = Vector128.Create(value);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] + value;
    }

    public static void Add(float* input, [ConstantExpected] float value, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<float> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx.IsSupported && (length - i) >= v256x4)
        {
            Vector256<float> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.Add(Avx.LoadVector256(input + i), values));
        }

        if (Sse.IsSupported && (length - i) >= v128x4)
        {
            Vector128<float> values = Vector128.Create(value);
            for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.Add(Sse.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] + value;
    }

    public static void Add(double* input, [ConstantExpected] double value, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<double> values = Vector512.Create(value);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<double> values = Vector256.Create(value);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.Add(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<double> values = Vector128.Create(value);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] + value;
    }
    #endregion

    #region Add(T* input, T* values, T* output, int length)
    public static void Add(byte* input, byte* values, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] + values[i]);
    }

    public static void Add(sbyte* input, sbyte* values, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] + values[i]);
    }

    public static void Add(short* input, short* values, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (short)(input[i] + values[i]);
    }

    public static void Add(ushort* input, ushort* values, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] + values[i]);
    }

    public static void Add(int* input, int* values, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] + values[i];
    }

    public static void Add(uint* input, uint* values, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] + values[i];
    }

    public static void Add(long* input, long* values, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] + values[i];
    }

    public static void Add(ulong* input, ulong* values, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Add(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] + values[i];
    }

    public static void Add(float* input, float* values, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.Add(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.Add(Sse.LoadVector128(input + i), Sse.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] + values[i];
    }

    public static void Add(double* input, double* values, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Add(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.Add(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Add(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] + values[i];
    }
    #endregion


    #region AddSaturate(T* input, T value, T* output, int length)
    public static void AddSaturate(byte* input, [ConstantExpected] byte value, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<byte> values = Vector256.Create(value);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.AddSaturate(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<byte> values = Vector128.Create(value);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.AddSaturate(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (byte)Math.Max(byte.MaxValue, input[i] + value);
    }

    public static void AddSaturate(sbyte* input, [ConstantExpected] sbyte value, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<sbyte> values = Vector256.Create(value);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.AddSaturate(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<sbyte> values = Vector128.Create(value);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.AddSaturate(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (sbyte)Math.Max(sbyte.MaxValue, input[i] + value);
    }

    public static void AddSaturate(short* input, [ConstantExpected] short value, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<short> values = Vector256.Create(value);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.AddSaturate(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<short> values = Vector128.Create(value);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.AddSaturate(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (short)Math.Max(short.MaxValue, input[i] + value);
    }

    public static void AddSaturate(ushort* input, [ConstantExpected] ushort value, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<ushort> values = Vector256.Create(value);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.AddSaturate(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<ushort> values = Vector128.Create(value);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.AddSaturate(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (ushort)Math.Max(ushort.MaxValue, input[i] + value);
    }
    #endregion

    #region AddSaturate(T* input, T* values, T* output, int length)
    public static void AddSaturate(byte* input, byte* values, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.AddSaturate(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.AddSaturate(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (byte)Math.Max(byte.MaxValue, input[i] + values[i]);
    }

    public static void AddSaturate(sbyte* input, sbyte* values, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.AddSaturate(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.AddSaturate(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (sbyte)Math.Max(sbyte.MaxValue, input[i] + values[i]);
    }

    public static void AddSaturate(short* input, short* values, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.AddSaturate(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.AddSaturate(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (short)Math.Max(short.MaxValue, input[i] + values[i]);
    }

    public static void AddSaturate(ushort* input, ushort* values, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.AddSaturate(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.AddSaturate(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (ushort)Math.Max(ushort.MaxValue, input[i] + values[i]);
    }
    #endregion


    #region Multiply(T* input, T value, T* output, int length)
    public static void Multiply(int* input, [ConstantExpected] int value, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<int> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Multiply(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<int> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Multiply(Avx.LoadVector256(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (long)input[i] * value;
    }

    public static void Multiply(uint* input, [ConstantExpected] uint value, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<uint> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Multiply(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<uint> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Multiply(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<uint> values = Vector128.Create(value);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Multiply(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (ulong)input[i] * value;
    }

    public static void Multiply(float* input, [ConstantExpected] float value, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<float> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Multiply(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx.IsSupported && (length - i) >= v256x4)
        {
            Vector256<float> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.Multiply(Avx.LoadVector256(input + i), values));
        }

        if (Sse.IsSupported && (length - i) >= v128x4)
        {
            Vector128<float> values = Vector128.Create(value);
            for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.Multiply(Sse.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] * value;
    }

    public static void Multiply(double* input, [ConstantExpected] double value, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<double> values = Vector512.Create(value);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Multiply(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx.IsSupported && (length - i) >= v256x8)
        {
            Vector256<double> values = Vector256.Create(value);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.Multiply(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<double> values = Vector128.Create(value);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Multiply(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] * value;
    }
    #endregion

    #region Multiply(T* input, T* values, T* output, int length)
    public static void Multiply(int* input, int* values, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Multiply(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Multiply(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));
#endif

        for (; i < length; i++) output[i] = (long)input[i] * values[i];
    }

    public static void Multiply(uint* input, uint* values, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Multiply(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Multiply(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Multiply(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (ulong)input[i] * values[i];
    }

    public static void Multiply(float* input, float* values, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Multiply(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.Multiply(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.Multiply(Sse.LoadVector128(input + i), Sse.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] * values[i];
    }

    public static void Multiply(double* input, double* values, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Multiply(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.Multiply(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Multiply(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] * values[i];
    }
    #endregion


    #region MultiplyLow(T* input, T value, T* output, int length)
    public static void MultiplyLow(int* input, [ConstantExpected] int value, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<int> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.MultiplyLow(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<int> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.MultiplyLow(Avx.LoadVector256(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (int)((long)input[i] * value);
    }

    public static void MultiplyLow(uint* input, [ConstantExpected] uint value, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<uint> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.MultiplyLow(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<uint> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.MultiplyLow(Avx.LoadVector256(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (uint)((ulong)input[i] * value);
    }
    #endregion

    #region MultiplyLow(T* input, T* values, T* output, int length)
    public static void MultiplyLow(int* input, int* values, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.MultiplyLow(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.MultiplyLow(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));
#endif

        for (; i < length; i++) output[i] = (int)((long)input[i] * values[i]);
    }

    public static void MultiplyLow(uint* input, uint* values, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.MultiplyLow(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.MultiplyLow(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));
#endif

        for (; i < length; i++) output[i] = (uint)((ulong)input[i] * values[i]);
    }
    #endregion


    #region Subtract(T* input, T value, T* output, int length)
    public static void Subtract(byte* input, [ConstantExpected] byte value, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<byte> values = Vector256.Create(value);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<byte> values = Vector128.Create(value);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] - value);
    }

    public static void Subtract(sbyte* input, [ConstantExpected] sbyte value, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<sbyte> values = Vector256.Create(value);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<sbyte> values = Vector128.Create(value);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] - value);
    }

    public static void Subtract(short* input, [ConstantExpected] short value, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<short> values = Vector256.Create(value);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<short> values = Vector128.Create(value);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (short)(input[i] - value);
    }

    public static void Subtract(ushort* input, [ConstantExpected] ushort value, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<ushort> values = Vector256.Create(value);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<ushort> values = Vector128.Create(value);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] - value);
    }

    public static void Subtract(int* input, [ConstantExpected] int value, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<int> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<int> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<int> values = Vector128.Create(value);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] - value;
    }

    public static void Subtract(uint* input, [ConstantExpected] uint value, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<uint> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<uint> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<uint> values = Vector128.Create(value);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] - value;
    }

    public static void Subtract(long* input, [ConstantExpected] long value, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<long> values = Vector512.Create(value);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<long> values = Vector256.Create(value);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<long> values = Vector128.Create(value);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] - value;
    }

    public static void Subtract(ulong* input, [ConstantExpected] ulong value, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<ulong> values = Vector512.Create(value);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<ulong> values = Vector256.Create(value);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<ulong> values = Vector128.Create(value);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] - value;
    }

    public static void Subtract(float* input, [ConstantExpected] float value, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<float> values = Vector512.Create(value);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx.IsSupported && (length - i) >= v256x4)
        {
            Vector256<float> values = Vector256.Create(value);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.Subtract(Avx.LoadVector256(input + i), values));
        }

        if (Sse.IsSupported && (length - i) >= v128x4)
        {
            Vector128<float> values = Vector128.Create(value);
            for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.Subtract(Sse.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] + value;
    }

    public static void Subtract(double* input, [ConstantExpected] double value, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<double> values = Vector512.Create(value);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), values));
        }

        if (Avx.IsSupported && (length - i) >= v256x8)
        {
            Vector256<double> values = Vector256.Create(value);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.Subtract(Avx.LoadVector256(input + i), values));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<double> values = Vector128.Create(value);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), values));
        }
#endif

        for (; i < length; i++) output[i] = input[i] + value;
    }
    #endregion

    #region Subtract(T* input, T* values, T* output, int length)
    public static void Subtract(byte* input, byte* values, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] - values[i]);
    }

    public static void Subtract(sbyte* input, sbyte* values, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] - values[i]);
    }

    public static void Subtract(short* input, short* values, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (short)(input[i] - values[i]);
    }

    public static void Subtract(ushort* input, ushort* values, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] - values[i]);
    }

    public static void Subtract(int* input, int* values, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] - values[i];
    }

    public static void Subtract(uint* input, uint* values, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] - values[i];
    }

    public static void Subtract(long* input, long* values, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] - values[i];
    }

    public static void Subtract(ulong* input, ulong* values, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Subtract(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] - values[i];
    }

    public static void Subtract(float* input, float* values, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.Subtract(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.Subtract(Sse.LoadVector128(input + i), Sse.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] - values[i];
    }

    public static void Subtract(double* input, double* values, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Subtract(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(values + i)));

        if (Avx.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.Subtract(Avx.LoadVector256(input + i), Avx.LoadVector256(values + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Subtract(Sse2.LoadVector128(input + i), Sse2.LoadVector128(values + i)));
#endif

        for (; i < length; i++) output[i] = input[i] - values[i];
    }
    #endregion


    #region And(T* input, T mask, T* output, int length)
    public static void And(byte* input, [ConstantExpected] byte mask, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<byte> masks = Vector512.Create(mask);
            for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<byte> masks = Vector256.Create(mask);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<byte> masks = Vector128.Create(mask);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] & mask);
    }

    public static void And(sbyte* input, [ConstantExpected] sbyte mask, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<sbyte> masks = Vector512.Create(mask);
            for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<sbyte> masks = Vector256.Create(mask);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<sbyte> masks = Vector128.Create(mask);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] & mask);
    }

    public static void And(short* input, [ConstantExpected] short mask, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<short> masks = Vector512.Create(mask);
            for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<short> masks = Vector256.Create(mask);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<short> masks = Vector128.Create(mask);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (short)(input[i] & mask);
    }

    public static void And(ushort* input, [ConstantExpected] ushort mask, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<ushort> masks = Vector512.Create(mask);
            for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<ushort> masks = Vector256.Create(mask);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<ushort> masks = Vector128.Create(mask);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] & mask);
    }

    public static void And(int* input, [ConstantExpected] int mask, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<int> masks = Vector512.Create(mask);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<int> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<int> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] & mask;
    }

    public static void And(uint* input, [ConstantExpected] uint mask, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<uint> masks = Vector512.Create(mask);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<uint> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<uint> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] & mask;
    }

    public static void And(long* input, [ConstantExpected] long mask, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<long> masks = Vector512.Create(mask);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<long> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<long> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] & mask;
    }

    public static void And(ulong* input, [ConstantExpected] ulong mask, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<ulong> masks = Vector512.Create(mask);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<ulong> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<ulong> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] & mask;
    }

    public static void And(float* input, [ConstantExpected] float mask, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported && (length - i) >= v256x4)
        {
            Vector256<float> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.And(Avx.LoadVector256(input + i), masks));
        }

        if (Sse.IsSupported && (length - i) >= v128x4)
        {
            Vector128<float> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.And(Sse.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = BitCast<uint, float>(BitCast<float, uint>(input[i]) & BitCast<float, uint>(mask));
    }

    public static void And(double* input, [ConstantExpected] double mask, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported && (length - i) >= v256x8)
        {
            Vector256<double> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.And(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<double> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = BitCast<ulong, double>(BitCast<double, ulong>(input[i]) & BitCast<double, ulong>(mask));
    }
    #endregion

    #region And(T* input, T* masks, T* output, int length)
    public static void And(byte* input, byte* masks, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] & masks[i]);
    }

    public static void And(sbyte* input, sbyte* masks, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] & masks[i]);
    }

    public static void And(short* input, short* masks, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (short)(input[i] & masks[i]);
    }

    public static void And(ushort* input, ushort* masks, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] & masks[i]);
    }

    public static void And(int* input, int* masks, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] & masks[i];
    }

    public static void And(uint* input, uint* masks, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] & masks[i];
    }

    public static void And(long* input, long* masks, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] & masks[i];
    }

    public static void And(ulong* input, ulong* masks, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.And(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.And(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] & masks[i];
    }

    public static void And(float* input, float* masks, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.And(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.And(Sse2.LoadVector128(input + i), Sse.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = BitCast<uint, float>(BitCast<float, uint>(input[i]) & BitCast<float, uint>(masks[i]));
    }

    public static void And(double* input, double* masks, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.And(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.And(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = BitCast<ulong, double>(BitCast<double, ulong>(input[i]) & BitCast<double, ulong>(masks[i]));
    }
    #endregion


    #region Or(T* input, T mask, T* output, int length)
    public static void Or(byte* input, [ConstantExpected] byte mask, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<byte> masks = Vector512.Create(mask);
            for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<byte> masks = Vector256.Create(mask);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<byte> masks = Vector128.Create(mask);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] | mask);
    }

    public static void Or(sbyte* input, [ConstantExpected] sbyte mask, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<sbyte> masks = Vector512.Create(mask);
            for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<sbyte> masks = Vector256.Create(mask);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<sbyte> masks = Vector128.Create(mask);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] | mask);
    }

    public static void Or(short* input, [ConstantExpected] short mask, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<short> masks = Vector512.Create(mask);
            for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<short> masks = Vector256.Create(mask);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<short> masks = Vector128.Create(mask);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (short)(input[i] | mask);
    }

    public static void Or(ushort* input, [ConstantExpected] ushort mask, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<ushort> masks = Vector512.Create(mask);
            for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<ushort> masks = Vector256.Create(mask);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<ushort> masks = Vector128.Create(mask);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] | mask);
    }

    public static void Or(int* input, [ConstantExpected] int mask, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<int> masks = Vector512.Create(mask);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<int> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<int> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] | mask;
    }

    public static void Or(uint* input, [ConstantExpected] uint mask, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<uint> masks = Vector512.Create(mask);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<uint> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<uint> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] | mask;
    }

    public static void Or(long* input, [ConstantExpected] long mask, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<long> masks = Vector512.Create(mask);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<long> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<long> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] | mask;
    }

    public static void Or(ulong* input, [ConstantExpected] ulong mask, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<ulong> masks = Vector512.Create(mask);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<ulong> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<ulong> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] | mask;
    }

    public static void Or(float* input, [ConstantExpected] float mask, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported && (length - i) >= v256x4)
        {
            Vector256<float> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.Or(Avx.LoadVector256(input + i), masks));
        }

        if (Sse.IsSupported && (length - i) >= v128x4)
        {
            Vector128<float> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.Or(Sse.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = BitCast<uint, float>(BitCast<float, uint>(input[i]) | BitCast<float, uint>(mask));
    }

    public static void Or(double* input, [ConstantExpected] double mask, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported && (length - i) >= v256x8)
        {
            Vector256<double> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.Or(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<double> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = BitCast<ulong, double>(BitCast<double, ulong>(input[i]) | BitCast<double, ulong>(mask));
    }
    #endregion

    #region Or(T* input, T* masks, T* output, int length)
    public static void Or(byte* input, byte* masks, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] | masks[i]);
    }

    public static void Or(sbyte* input, sbyte* masks, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] | masks[i]);
    }

    public static void Or(short* input, short* masks, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (short)(input[i] | masks[i]);
    }

    public static void Or(ushort* input, ushort* masks, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] | masks[i]);
    }

    public static void Or(int* input, int* masks, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] | masks[i];
    }

    public static void Or(uint* input, uint* masks, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] | masks[i];
    }

    public static void Or(long* input, long* masks, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] | masks[i];
    }

    public static void Or(ulong* input, ulong* masks, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Or(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Or(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] | masks[i];
    }

    public static void Or(float* input, float* masks, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.Or(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.Or(Sse.LoadVector128(input + i), Sse.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = BitCast<uint, float>(BitCast<float, uint>(input[i]) | BitCast<float, uint>(masks[i]));
    }

    public static void Or(double* input, double* masks, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.Or(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Or(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = BitCast<ulong, double>(BitCast<double, ulong>(input[i]) | BitCast<double, ulong>(masks[i]));
    }
    #endregion


    #region Xor(T* input, T mask, T* output, int length)
    public static void Xor(byte* input, [ConstantExpected] byte mask, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<byte> masks = Vector512.Create(mask);
            for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<byte> masks = Vector256.Create(mask);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<byte> masks = Vector128.Create(mask);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] ^ mask);
    }

    public static void Xor(sbyte* input, [ConstantExpected] sbyte mask, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<sbyte> masks = Vector512.Create(mask);
            for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<sbyte> masks = Vector256.Create(mask);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<sbyte> masks = Vector128.Create(mask);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] ^ mask);
    }

    public static void Xor(short* input, [ConstantExpected] short mask, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<short> masks = Vector512.Create(mask);
            for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<short> masks = Vector256.Create(mask);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<short> masks = Vector128.Create(mask);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (short)(input[i] ^ mask);
    }

    public static void Xor(ushort* input, [ConstantExpected] ushort mask, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<ushort> masks = Vector512.Create(mask);
            for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<ushort> masks = Vector256.Create(mask);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<ushort> masks = Vector128.Create(mask);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] ^ mask);
    }

    public static void Xor(int* input, [ConstantExpected] int mask, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<int> masks = Vector512.Create(mask);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<int> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<int> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] ^ mask;
    }

    public static void Xor(uint* input, [ConstantExpected] uint mask, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<uint> masks = Vector512.Create(mask);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<uint> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<uint> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] ^ mask;
    }

    public static void Xor(long* input, [ConstantExpected] long mask, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<long> masks = Vector512.Create(mask);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<long> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<long> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] ^ mask;
    }

    public static void Xor(ulong* input, [ConstantExpected] ulong mask, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<ulong> masks = Vector512.Create(mask);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<ulong> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<ulong> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] ^ mask;
    }

    public static void Xor(float* input, [ConstantExpected] float mask, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported && (length - i) >= v256x4)
        {
            Vector256<float> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.Xor(Avx.LoadVector256(input + i), masks));
        }

        if (Sse.IsSupported && (length - i) >= v128x4)
        {
            Vector128<float> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.Xor(Sse.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = BitCast<uint, float>(BitCast<float, uint>(input[i]) ^ BitCast<float, uint>(mask));
    }

    public static void Xor(double* input, [ConstantExpected] double mask, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported && (length - i) >= v256x8)
        {
            Vector256<double> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.Xor(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<double> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = BitCast<ulong, double>(BitCast<double, ulong>(input[i]) ^ BitCast<double, ulong>(mask));
    }
    #endregion

    #region Xor(T* input, T* masks, T* output, int length)
    public static void Xor(byte* input, byte* masks, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] ^ masks[i]);
    }

    public static void Xor(sbyte* input, sbyte* masks, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] ^ masks[i]);
    }

    public static void Xor(short* input, short* masks, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (short)(input[i] ^ masks[i]);
    }

    public static void Xor(ushort* input, ushort* masks, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] ^ masks[i]);
    }

    public static void Xor(int* input, int* masks, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] ^ masks[i];
    }

    public static void Xor(uint* input, uint* masks, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] ^ masks[i];
    }

    public static void Xor(long* input, long* masks, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] ^ masks[i];
    }

    public static void Xor(ulong* input, ulong* masks, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.Xor(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.Xor(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] ^ masks[i];
    }

    public static void Xor(float* input, float* masks, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.Xor(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.Xor(Sse.LoadVector128(input + i), Sse.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = BitCast<uint, float>(BitCast<float, uint>(input[i]) ^ BitCast<float, uint>(masks[i]));
    }

    public static void Xor(double* input, double* masks, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.Xor(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.Xor(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = BitCast<ulong, double>(BitCast<double, ulong>(input[i]) ^ BitCast<double, ulong>(masks[i]));
    }
    #endregion


    #region AndNot(T* input, T mask, T* output, int length)
    public static void AndNot(byte* input, [ConstantExpected] byte mask, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<byte> masks = Vector512.Create(mask);
            for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<byte> masks = Vector256.Create(mask);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<byte> masks = Vector128.Create(mask);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] & ~mask);
    }

    public static void AndNot(sbyte* input, [ConstantExpected] sbyte mask, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<sbyte> masks = Vector512.Create(mask);
            for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x1)
        {
            Vector256<sbyte> masks = Vector256.Create(mask);
            for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x1)
        {
            Vector128<sbyte> masks = Vector128.Create(mask);
            for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] & ~mask);
    }

    public static void AndNot(short* input, [ConstantExpected] short mask, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<short> masks = Vector512.Create(mask);
            for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<short> masks = Vector256.Create(mask);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<short> masks = Vector128.Create(mask);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (short)(input[i] & ~mask);
    }

    public static void AndNot(ushort* input, [ConstantExpected] ushort mask, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<ushort> masks = Vector512.Create(mask);
            for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x2)
        {
            Vector256<ushort> masks = Vector256.Create(mask);
            for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x2)
        {
            Vector128<ushort> masks = Vector128.Create(mask);
            for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] & ~mask);
    }

    public static void AndNot(int* input, [ConstantExpected] int mask, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<int> masks = Vector512.Create(mask);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<int> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<int> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] & ~mask;
    }

    public static void AndNot(uint* input, [ConstantExpected] uint mask, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<uint> masks = Vector512.Create(mask);
            for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x4)
        {
            Vector256<uint> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x4)
        {
            Vector128<uint> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] & ~mask;
    }

    public static void AndNot(long* input, [ConstantExpected] long mask, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<long> masks = Vector512.Create(mask);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<long> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<long> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] & ~mask;
    }

    public static void AndNot(ulong* input, [ConstantExpected] ulong mask, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported)
        {
            Vector512<ulong> masks = Vector512.Create(mask);
            for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), masks));
        }

        if (Avx2.IsSupported && (length - i) >= v256x8)
        {
            Vector256<ulong> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<ulong> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = input[i] & ~mask;
    }

    public static void AndNot(float* input, [ConstantExpected] float mask, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported && (length - i) >= v256x4)
        {
            Vector256<float> masks = Vector256.Create(mask);
            for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.AndNot(Avx.LoadVector256(input + i), masks));
        }

        if (Sse.IsSupported && (length - i) >= v128x4)
        {
            Vector128<float> masks = Vector128.Create(mask);
            for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.AndNot(Sse.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = BitCast<uint, float>(BitCast<float, uint>(input[i]) & ~BitCast<float, uint>(mask));
    }

    public static void AndNot(double* input, [ConstantExpected] double mask, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported && (length - i) >= v256x8)
        {
            Vector256<double> masks = Vector256.Create(mask);
            for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.AndNot(Avx.LoadVector256(input + i), masks));
        }

        if (Sse2.IsSupported && (length - i) >= v128x8)
        {
            Vector128<double> masks = Vector128.Create(mask);
            for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), masks));
        }
#endif

        for (; i < length; i++) output[i] = BitCast<ulong, double>(BitCast<double, ulong>(input[i]) & ~BitCast<double, ulong>(mask));
    }
    #endregion

    #region AndNot(T* input, T* masks, T* output, int length)
    public static void AndNot(byte* input, byte* masks, byte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (byte)(input[i] & ~masks[i]);
    }

    public static void AndNot(sbyte* input, sbyte* masks, sbyte* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x1; i += v512x1) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x1; i += v256x1) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x1; i += v128x1) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (sbyte)(input[i] & ~masks[i]);
    }

    public static void AndNot(short* input, short* masks, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (short)(input[i] & ~masks[i]);
    }

    public static void AndNot(ushort* input, ushort* masks, ushort* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x2; i += v512x2) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = (ushort)(input[i] & ~masks[i]);
    }

    public static void AndNot(int* input, int* masks, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] & ~masks[i];
    }

    public static void AndNot(uint* input, uint* masks, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] & ~masks[i];
    }

    public static void AndNot(long* input, long* masks, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] & ~masks[i];
    }

    public static void AndNot(ulong* input, ulong* masks, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.AndNot(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(masks + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.AndNot(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = input[i] & ~masks[i];
    }

    public static void AndNot(float* input, float* masks, float* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx.AndNot(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse.Store(output + i, Sse.AndNot(Sse.LoadVector128(input + i), Sse.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = BitCast<uint, float>(BitCast<float, uint>(input[i]) & ~BitCast<float, uint>(masks[i]));
    }

    public static void AndNot(double* input, double* masks, double* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx.AndNot(Avx.LoadVector256(input + i), Avx.LoadVector256(masks + i)));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.AndNot(Sse2.LoadVector128(input + i), Sse2.LoadVector128(masks + i)));
#endif

        for (; i < length; i++) output[i] = BitCast<ulong, double>(BitCast<double, ulong>(input[i]) & ~BitCast<double, ulong>(masks[i]));
    }
    #endregion


    #region ShiftLeftLogical(T* input, T count, T* output, int length)
    public static void ShiftLeftLogical(int* input, [ConstantExpected] byte count, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftLeftLogical(Avx512F.LoadVector512(input + i), count));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftLeftLogical(Avx.LoadVector256(input + i), count));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.ShiftLeftLogical(Sse2.LoadVector128(input + i), count));
#endif

        for (; i < length; i++) output[i] = input[i] << count;
    }

    public static void ShiftLeftLogical(uint* input, [ConstantExpected] byte count, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftLeftLogical(Avx512F.LoadVector512(input + i), count));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftLeftLogical(Avx.LoadVector256(input + i), count));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.ShiftLeftLogical(Sse2.LoadVector128(input + i), count));
#endif

        for (; i < length; i++) output[i] = input[i] << count;
    }

    public static void ShiftLeftLogical(long* input, [ConstantExpected] byte count, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.ShiftLeftLogical(Avx512F.LoadVector512(input + i), count));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.ShiftLeftLogical(Avx.LoadVector256(input + i), count));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.ShiftLeftLogical(Sse2.LoadVector128(input + i), count));
#endif

        for (; i < length; i++) output[i] = input[i] << count;
    }

    public static void ShiftLeftLogical(ulong* input, [ConstantExpected] byte count, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.ShiftLeftLogical(Avx512F.LoadVector512(input + i), count));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.ShiftLeftLogical(Avx.LoadVector256(input + i), count));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.ShiftLeftLogical(Sse2.LoadVector128(input + i), count));
#endif

        for (; i < length; i++) output[i] = input[i] << count;
    }
    #endregion

    #region ShiftLeftLogical(T* input, T* counts, T* output, int length)
    public static void ShiftLeftLogical(int* input, uint* counts, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftLeftLogicalVariable(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(counts + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftLeftLogicalVariable(Avx.LoadVector256(input + i), Avx.LoadVector256(counts + i)));
#endif

        for (; i < length; i++) output[i] = input[i] << (int)counts[i];
    }

    public static void ShiftLeftLogical(uint* input, uint* counts, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftLeftLogicalVariable(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(counts + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftLeftLogicalVariable(Avx.LoadVector256(input + i), Avx.LoadVector256(counts + i)));
#endif

        for (; i < length; i++) output[i] = input[i] << (int)counts[i];
    }

    public static void ShiftLeftLogical(long* input, ulong* counts, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftLeftLogicalVariable(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(counts + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftLeftLogicalVariable(Avx.LoadVector256(input + i), Avx.LoadVector256(counts + i)));
#endif

        for (; i < length; i++) output[i] = input[i] << (int)counts[i];
    }

    public static void ShiftLeftLogical(ulong* input, ulong* counts, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftLeftLogicalVariable(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(counts + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftLeftLogicalVariable(Avx.LoadVector256(input + i), Avx.LoadVector256(counts + i)));
#endif

        for (; i < length; i++) output[i] = input[i] << (int)counts[i];
    }
    #endregion


    #region ShiftRightLogical(T* input, T count, T* output, int length)
    public static void ShiftRightLogical(int* input, [ConstantExpected] byte count, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftRightLogical(Avx512F.LoadVector512(input + i), count));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftRightLogical(Avx.LoadVector256(input + i), count));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.ShiftRightLogical(Sse2.LoadVector128(input + i), count));
#endif

        for (; i < length; i++) output[i] = input[i] >>> count;
    }

    public static void ShiftRightLogical(uint* input, [ConstantExpected] byte count, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftRightLogical(Avx512F.LoadVector512(input + i), count));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftRightLogical(Avx.LoadVector256(input + i), count));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.ShiftRightLogical(Sse2.LoadVector128(input + i), count));
#endif

        for (; i < length; i++) output[i] = input[i] >>> count;
    }

    public static void ShiftRightLogical(long* input, [ConstantExpected] byte count, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.ShiftRightLogical(Avx512F.LoadVector512(input + i), count));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.ShiftRightLogical(Avx.LoadVector256(input + i), count));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.ShiftRightLogical(Sse2.LoadVector128(input + i), count));
#endif

        for (; i < length; i++) output[i] = input[i] >>> count;
    }

    public static void ShiftRightLogical(ulong* input, [ConstantExpected] byte count, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.ShiftRightLogical(Avx512F.LoadVector512(input + i), count));

        if (Avx2.IsSupported) for (; i <= length - v256x8; i += v256x8) Avx.Store(output + i, Avx2.ShiftRightLogical(Avx.LoadVector256(input + i), count));

        if (Sse2.IsSupported) for (; i <= length - v128x8; i += v128x8) Sse2.Store(output + i, Sse2.ShiftRightLogical(Sse2.LoadVector128(input + i), count));
#endif

        for (; i < length; i++) output[i] = input[i] >>> count;
    }
    #endregion

    #region ShiftRightLogical(T* input, T* counts, T* output, int length)
    public static void ShiftRightLogical(int* input, uint* counts, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftRightLogicalVariable(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(counts + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftRightLogicalVariable(Avx.LoadVector256(input + i), Avx.LoadVector256(counts + i)));
#endif

        for (; i < length; i++) output[i] = input[i] >>> (int)counts[i];
    }

    public static void ShiftRightLogical(uint* input, uint* counts, uint* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftRightLogicalVariable(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(counts + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftRightLogicalVariable(Avx.LoadVector256(input + i), Avx.LoadVector256(counts + i)));
#endif

        for (; i < length; i++) output[i] = input[i] >>> (int)counts[i];
    }

    public static void ShiftRightLogical(long* input, ulong* counts, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftRightLogicalVariable(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(counts + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftRightLogicalVariable(Avx.LoadVector256(input + i), Avx.LoadVector256(counts + i)));
#endif

        for (; i < length; i++) output[i] = input[i] >>> (int)counts[i];
    }

    public static void ShiftRightLogical(ulong* input, ulong* counts, ulong* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftRightLogicalVariable(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(counts + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftRightLogicalVariable(Avx.LoadVector256(input + i), Avx.LoadVector256(counts + i)));
#endif

        for (; i < length; i++) output[i] = input[i] >>> (int)counts[i];
    }
    #endregion


    #region ShiftRightArithmetic(T* input, T count, T* output, int length)
    public static void ShiftRightArithmetic(short* input, [ConstantExpected] byte count, short* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx2.IsSupported) for (; i <= length - v256x2; i += v256x2) Avx.Store(output + i, Avx2.ShiftRightArithmetic(Avx.LoadVector256(input + i), count));

        if (Sse2.IsSupported) for (; i <= length - v128x2; i += v128x2) Sse2.Store(output + i, Sse2.ShiftRightArithmetic(Sse2.LoadVector128(input + i), count));
#endif

        for (; i < length; i++) output[i] = (short)(input[i] >> count);
    }

    public static void ShiftRightArithmetic(int* input, [ConstantExpected] byte count, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftRightArithmetic(Avx512F.LoadVector512(input + i), count));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftRightArithmetic(Avx.LoadVector256(input + i), count));

        if (Sse2.IsSupported) for (; i <= length - v128x4; i += v128x4) Sse2.Store(output + i, Sse2.ShiftRightArithmetic(Sse2.LoadVector128(input + i), count));
#endif

        for (; i < length; i++) output[i] = input[i] >> count;
    }

    public static void ShiftRightArithmetic(long* input, [ConstantExpected] byte count, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.ShiftRightArithmetic(Avx512F.LoadVector512(input + i), count));
#endif

        for (; i < length; i++) output[i] = input[i] >> count;
    }
    #endregion

    #region ShiftRightArithmetic(T* input, T* counts, T* output, int length)
    public static void ShiftRightArithmetic(int* input, uint* counts, int* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x4; i += v512x4) Avx512F.Store(output + i, Avx512F.ShiftRightArithmeticVariable(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(counts + i)));

        if (Avx2.IsSupported) for (; i <= length - v256x4; i += v256x4) Avx.Store(output + i, Avx2.ShiftRightArithmeticVariable(Avx.LoadVector256(input + i), Avx.LoadVector256(counts + i)));
#endif

        for (; i < length; i++) output[i] = input[i] >> (int)counts[i];
    }

    public static void ShiftRightArithmetic(long* input, ulong* counts, long* output, int length)
    {
        int i = 0;

#if NET6_0_OR_GREATER
        if (Avx512F.IsSupported) for (; i <= length - v512x8; i += v512x8) Avx512F.Store(output + i, Avx512F.ShiftRightArithmeticVariable(Avx512F.LoadVector512(input + i), Avx512F.LoadVector512(counts + i)));
#endif

        for (; i < length; i++) output[i] = input[i] >> (int)counts[i];
    }
    #endregion
}
