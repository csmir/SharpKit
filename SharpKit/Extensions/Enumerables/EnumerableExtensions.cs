using System.Runtime.InteropServices;
using SharpKit.Collections;

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
        #pragma warning disable CS8500

        /// <inheritdoc cref="Enumerable.All{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
        public unsafe bool MxAll<T>(Func<T, bool> predicate)
        {
            if (array is T[] flat) return flat.All(predicate);

            if (typeof(T).IsValueType)
            {
#if NET8_0_OR_GREATER
                foreach (T item in (ValueArray<T>)array) if (!predicate(item)) return false;
#else
                GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                try
                {
                    int length = array.Length;
                    T* basePtr = (T*)handle.AddrOfPinnedObject();
                    for (int i = 0; i < length; i++) if (!predicate(basePtr[i])) return false;
                }
                finally { handle.Free(); }
#endif
            }

            else foreach (T item in array) if (!predicate(item)) return false;

            return true;
        }

        /// <inheritdoc cref="Enumerable.Any{TSource}(IEnumerable{TSource})"/>
        public bool MxAny() => array.Length != 0;

        /// <inheritdoc cref="Enumerable.Any{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
        public unsafe bool MxAny<T>(Func<T, bool> predicate)
        {
            if (array is T[] flat) return flat.Any(predicate);

            if (typeof(T).IsValueType)
            {
#if NET8_0_OR_GREATER
                foreach (T item in (ValueArray<T>)array) if (predicate(item)) return true;
#else
                GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                try
                {
                    int length = array.Length;
                    T* basePtr = (T*)handle.AddrOfPinnedObject();
                    for (int i = 0; i < length; i++) if (predicate(basePtr[i])) return true;
                }
                finally { handle.Free(); }
#endif
            }

            else foreach (T item in array) if (predicate(item)) return true;

            return false;
        }

        /// <inheritdoc cref="Enumerable.Count{TSource}(IEnumerable{TSource})"/>
        public int MxCount() => array.Length;

        /// <inheritdoc cref="Enumerable.Count{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
        public unsafe int MxCount<T>(Func<T, bool> predicate)
        {
            if (array is T[] flat) return flat.Count(predicate);

            int count = 0;
            if (typeof(T).IsValueType)
            {
#if NET8_0_OR_GREATER
                foreach (T item in (ValueArray<T>)array) if (predicate(item)) count++;
#else
                GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                try
                {
                    int length = array.Length;
                    T* basePtr = (T*)handle.AddrOfPinnedObject();
                    for (int i = 0; i < length; i++) if (predicate(basePtr[i])) count++;
                }
                finally { handle.Free(); }
#endif
            }

            else foreach (T item in array) if (predicate(item)) count++;

            return count;
        }

        #pragma warning restore CS8500
        #endregion
    }
}
