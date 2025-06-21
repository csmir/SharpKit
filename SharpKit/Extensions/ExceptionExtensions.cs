using System.Text.RegularExpressions;

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
                throw new ArgumentException($"Argument '{argumentExpression}' cannot be null or empty.", argumentExpression);
        }

        public static void ThrowIfNullOrWhiteSpace(string? argument, string argumentExpression)
        {
            if (string.IsNullOrWhiteSpace(argument))
                throw new ArgumentException($"Argument '{argumentExpression}' cannot be null, empty, or consist only of white-space characters.", argumentExpression);
        }
#endif
        public static void ThrowIfNoMatch(string? argument, Regex checkExpression,
#if NET6_0_OR_GREATER
            [CallerArgumentExpression(nameof(argument))]
#endif
            string? argumentExpression = null)
        {
            ArgumentNullException.ThrowIfNull(argument, argumentExpression!);

            if (!checkExpression.IsMatch(argument))
                throw new ArgumentException($"Argument '{argumentExpression}' does not match the required pattern.", argumentExpression);
        }

        public static void ThrowIfMatch(string? argument, Regex checkExpression,
#if NET6_0_OR_GREATER
            [CallerArgumentExpression(nameof(argument))]
#endif
            string? argumentExpression = null)
        {
            ArgumentNullException.ThrowIfNull(argument, argumentExpression!);

            if (argument != null && checkExpression.IsMatch(argument))
                throw new ArgumentException($"Argument '{argumentExpression}' matches the forbidden pattern.", argumentExpression);
        }

        public static void ThrowIfEmpty<T>(IEnumerable<T>? argument,
#if NET6_0_OR_GREATER
            [CallerArgumentExpression(nameof(argument))]
#endif
            string? argumentExpression = null)
        {
            ArgumentNullException.ThrowIfNull(argument, argumentExpression!);

            if (argument is ICollection<T> collection)
            {
                if (collection.Count == 0)
                    throw new ArgumentException($"Argument '{argumentExpression}' cannot be empty.", argumentExpression);
            }
            else if (!argument.Any())
                throw new ArgumentException($"Argument '{argumentExpression}' cannot be empty.", argumentExpression);
        }
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
