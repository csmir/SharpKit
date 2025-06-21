namespace SharpKit;

public static class LinqExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public bool Contains<TImpl>()
        {
            foreach (var item in source)
            {
                if (item is TImpl)
                    return true;
            }

            return false;
        }

        public bool Contains<TImpl>(Func<TImpl, bool> predicate)
        {
            foreach (var item in source)
            {
                if (item is TImpl tImpl && predicate(tImpl))
                    return true;
            }
            return false;
        }

        public TImpl? FirstOrDefault<TImpl>(TImpl? defaultValue = default)
        {
            foreach (var item in source)
            {
                if (item is TImpl tImpl)
                    return tImpl;
            }

            return defaultValue;
        }

        public TImpl? FirstOrDefault<TImpl>(Func<TImpl, bool> predicate, TImpl? defaultValue = default)
        {
            foreach (var item in source)
            {
                if (item is TImpl tImpl && (predicate == null || predicate(tImpl)))
                    return tImpl;
            }

            return defaultValue;
        }

        public TImpl? LastOrDefault<TImpl>(TImpl? defaultValue = default)
        {
            var last = defaultValue;

            foreach (var item in source)
            {
                if (item is TImpl tImpl)
                    last = tImpl;
            }

            return last;
        }

        public TImpl? LastOrDefault<TImpl>(Func<TImpl, bool> predicate, TImpl? defaultValue = default)
        {
            var last = defaultValue;

            foreach (var item in source)
            {
                if (item is TImpl tImpl && (predicate == null || predicate(tImpl)))
                    last = tImpl;
            }

            return last;
        }

        public IEnumerable<T> ForEach<TImpl>(Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }
    }
}
