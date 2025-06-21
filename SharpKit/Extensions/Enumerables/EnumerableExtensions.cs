namespace SharpKit;

public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        // Extending general LINQ operations.

        public IEnumerable<T> ForEach<TImpl>(Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }

        public IEnumerable<T> TakeReverseWhile(Func<T, bool> predicate, bool yieldReversed = false)
        {
            IEnumerable<T> Take()
            {
                foreach (var value in source.Reverse())
                {
                    if (!predicate(value))
                        break;
                    yield return value;
                }
            }

            var result = Take();

            return yieldReversed ? result : result.Reverse();
        }
    }

    extension(IEnumerable source)
    {
        // Extending generic constraint filtering for IEnumerable

        public IEnumerable<T> OfType<T>(Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (item is T tItem && predicate(tItem))
                    yield return tItem;
            }
        }

        public bool Contains<T>()
        {
            foreach (var item in source)
            {
                if (item is T)
                    return true;
            }

            return false;
        }

        public bool Contains<T>(Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (item is T tImpl && predicate(tImpl))
                    return true;
            }
            return false;
        }

        public T? FirstOrDefault<T>(T? defaultValue = default)
        {
            foreach (var item in source)
            {
                if (item is T tImpl)
                    return tImpl;
            }

            return defaultValue;
        }

        public T? FirstOrDefault<T>(Func<T, bool> predicate, T? defaultValue = default)
        {
            foreach (var item in source)
            {
                if (item is T tImpl && (predicate == null || predicate(tImpl)))
                    return tImpl;
            }

            return defaultValue;
        }

        public T? LastOrDefault<T>(T? defaultValue = default)
        {
            var last = defaultValue;

            foreach (var item in source)
            {
                if (item is T tImpl)
                    last = tImpl;
            }

            return last;
        }

        public T? LastOrDefault<T>(Func<T, bool> predicate, T? defaultValue = default)
        {
            var last = defaultValue;

            foreach (var item in source)
            {
                if (item is T tImpl && (predicate == null || predicate(tImpl)))
                    last = tImpl;
            }

            return last;
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
