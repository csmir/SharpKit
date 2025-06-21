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

            foreach (var component in items)
                arr[i++] = component;
        }
    }
}
