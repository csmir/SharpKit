namespace SharpKit;

public static class ExceptionExtensions
{
    extension(ArgumentException ex)
    {
        // Polyfill for .NET versions prior to .NET 6.0
#if !NET6_0_OR_GREATER
        public static void ThrowIfNullOrEmpty(string? argument, string argumentExpression)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentException($"Argument '{argumentExpression}' cannot be null or empty.", argumentExpression);
            }
        }

        public static void ThrowIfNullOrWhiteSpace(string? argument, string argumentExpression)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException($"Argument '{argumentExpression}' cannot be null, empty, or consist only of white-space characters.", argumentExpression);
            }
        }
#endif
    }

    extension(ArgumentNullException ex)
    {
        // Polyfill for .NET versions prior to .NET 6.0
#if !NET6_0_OR_GREATER
        public static void ThrowIfNull(object? argument, string argumentExpression)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentExpression, $"Argument '{argumentExpression}' cannot be null.");
            }
        }
#endif
    }
}
