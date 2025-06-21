using System;

namespace SharpKit
{
    public static class ArrayExtensions
    {
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
                if (items.Length == 0)
                    return;

                var i = arr.Length;

                Array.Resize(ref arr, arr.Length + items.Length);

                foreach (var component in items)
                    arr[i++] = component;
            }
        }
    }
}
