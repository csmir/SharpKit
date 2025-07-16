#if NET8_0_OR_GREATER
using SharpKit.Internals;
using SharpKit.Arrays;
#endif

using System.Runtime.InteropServices;

namespace SharpKit;

public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        // Extending general LINQ operations.

        public IEnumerable<T> ForEach<TImpl>(Action<T> action)
        {
            foreach (var item in source)
                action(item);

            return source;
        }
    }

    extension(Array array)
    {
        public static void Push<T>(ref T[] arr, T item)
        {
            var i = arr.Length;

            Array.Resize(ref arr, arr.Length + 1);

            arr[i] = item;
        }

        public static void Push<T>(ref T[] arr, params T[] items)
        {
            ArgumentNullException.ThrowIfNull(items, nameof(items));

            if (items.Length == 0)
                return;

            var i = arr.Length;

            Array.Resize(ref arr, arr.Length + items.Length);

            Array.Copy(items, 0, arr, i, items.Length);
        }

        #region Multidimension LINQ

        public unsafe bool MxAll<T>(Func<T, bool> predicate)
        {
            int length = array.Length;

            if (array is T[] typedArray) return typedArray.All(predicate);

            if (typeof(T).IsValueType && array.GetType().GetElementType() == typeof(T))
            {
                GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);

#pragma warning disable CS8500 // Compiler can't recognize this is a value type.
                try
                {
                    byte* basePtr = (byte*)handle.AddrOfPinnedObject();
                    int size = sizeof(T);

                    for (int i = 0; i < length; i++) if (!predicate(*(T*)(basePtr + i * size))) return false;
                }
                finally
                {
                    handle.Free();
                }
#pragma warning restore CS8500

                return true;
            }

#if NET8_0_OR_GREATER
            return ((Func<Array, Func<T, bool>, bool>)LinqGenerator.GetMethod<T>(LinqMethod.All, array.Rank))(array, predicate);
#else
            for (int i = 0; i < length; i++) if (!predicate((T)array.GetValue(i)!)) return false;

            return true;
#endif
        }

        public bool MxAny() => array.Length != 0;

        public unsafe bool MxAny<T>(Func<T, bool> predicate)
        {
            int length = array.Length;

            if (array is T[] typedArray) return typedArray.Any(predicate);

            if (typeof(T).IsValueType && array.GetType().GetElementType() == typeof(T))
            {
                GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);

#pragma warning disable CS8500 // Compiler can't recognize this is a value type.
                try
                {
                    byte* basePtr = (byte*)handle.AddrOfPinnedObject();
                    int size = sizeof(T);

                    for (int i = 0; i < length; i++) if (predicate(*(T*)(basePtr + i * size))) return true;
                }
                finally
                {
                    handle.Free();
                }
#pragma warning restore CS8500

                return false;
            }

#if NET8_0_OR_GREATER
            return ((Func<Array, Func<T, bool>, bool>)LinqGenerator.GetMethod<T>(LinqMethod.Any, array.Rank))(array, predicate);
#else
            for (int i = 0; i < length; i++) if (predicate((T)array.GetValue(i)!)) return true;

            return false;
#endif
        }

        public int MxCount() => array.Length;

        public unsafe int MxCount<T>(Func<T, bool> predicate)
        {
            int length = array.Length, count = 0;

            if (array is T[] typedArray) return typedArray.Count(predicate);

            if (typeof(T).IsValueType && array.GetType().GetElementType() == typeof(T))
            {
                GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);

#pragma warning disable CS8500 // Compiler can't recognize this is a value type.
                try
                {
                    byte* basePtr = (byte*)handle.AddrOfPinnedObject();
                    int size = sizeof(T);

                    for (int i = 0; i < length; i++) if (predicate(*(T*)(basePtr + i * size))) count++;
                }
                finally
                {
                    handle.Free();
                }
#pragma warning restore CS8500

                return count;
            }

#if NET8_0_OR_GREATER
            return ((Func<Array, Func<T, bool>, int>)LinqGenerator.GetMethod<T>(LinqMethod.Count, array.Rank))(array, predicate);
#else
            for (int i = 0; i < length; i++) if (predicate((T)array.GetValue(i)!)) count++;

            return count;
#endif
        }

        #endregion
    }
}
