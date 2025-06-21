namespace SharpKit;

public static class PrimitiveExtensions
{
    extension(string str)
    {
        public string Reduce(int maxLength, bool killAtWhitespace = false, string finalizer = "...")
        {
            ArgumentNullException.ThrowIfNull(str, nameof(str));

            if (str.Length > maxLength)
            {
                maxLength -= finalizer.Length + 1; // reduce the length of the finalizer + a single integer to convert to valid range.

                ArgumentOutOfRangeException.ThrowIfNegative(maxLength, nameof(maxLength));

                if (killAtWhitespace)
                {
                    var range = str.Split(' ');

                    for (int i = 2; str.Length + finalizer.Length > maxLength; i++) // set i as 2, 1 for index reduction, 1 for initial word removal, then increment.
#if NET6_0_OR_GREATER
                        str = string.Join(' ', range[..(range.Length - i)]);
#else
                        str = string.Join(" ", range.Skip(range.Length - i));
#endif

                    str += finalizer;
                }

#if NET6_0_OR_GREATER
                return str[..maxLength] + finalizer;
#else
                return str.Substring(0, maxLength) + finalizer;
#endif
            }

            return str;
        }

        public bool IsAlphaNumeric()
        {
            ArgumentException.ThrowIfNullOrEmpty(str, nameof(str));

            foreach (var c in str)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
