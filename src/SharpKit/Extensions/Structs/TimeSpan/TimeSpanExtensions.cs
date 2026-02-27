using System.Text;

namespace SharpKit;

/// <summary>
///     Extensions for <see cref="TimeSpan"/>
/// </summary>
public static class TimespanExtensions
{

    extension(TimeSpan span)
    {
        /// <summary>
        ///     Attempts to parse the specified input string into a <see cref="TimeSpan"/> value using a flexible parsing strategy.
        /// </summary>
        /// <remarks>
        ///     This method supports parsing time intervals from input strings that may not strictly
        ///     adhere to standard <see cref="TimeSpan"/> formats. It is useful when accepting user input or data from
        ///     sources with inconsistent formatting.
        /// </remarks>
        /// <param name="input">The input string to parse. This string can represent a time interval in various common formats.</param>
        /// <param name="result">When this method returns <see langword="true"/>, contains the parsed <see cref="TimeSpan"/> value that corresponds to the input string; otherwise, contains <see cref="TimeSpan.Zero"/>.</param>
        /// <returns><see langword="true"/> if the input string was successfully parsed; otherwise, <see langword="false"/>.</returns>
        public static bool TryParseFuzzy(string input,
#if NET6_0_OR_GREATER 
            [NotNullWhen(true)]
#endif
            out TimeSpan result) => TimeSpanParser.TryParseFuzzy(input, out result);

        /// <summary>
        ///     Formats the timespan into a human-readable string, such as "2 days, 3 hours, and 15 minutes".
        /// </summary>
        /// <returns>A new <see langword="string"/> containing the formatted span of time.</returns>
        public string ToFormattedString()
        {
#if NET6_0_OR_GREATER
            var sb = new Performance.ValueStringBuilder(stackalloc char[64]);
#else
            var sb = new StringBuilder();
#endif

            if (span.Days > 0)
            {
                sb.Append($"{span.Days} day{(span.Days > 1 ? "s" : "")}");

                if (span.Hours > 0 && (span.Minutes > 0 || span.Seconds > 0))
                    sb.Append(", ");

                else if (span.Hours > 0)
                    sb.Append(", and ");

                else
                    return sb.ToString();
            }

            if (span.Hours > 0)
            {
                sb.Append($"{span.Hours} hour{(span.Hours > 1 ? "s" : "")}");

                if (span.Minutes > 0 && span.Seconds > 0)
                    sb.Append(", ");

                else if (span.Minutes > 0)
                    sb.Append(", and ");

                else
                    return sb.ToString();
            }

            if (span.Minutes > 0)
            {
                sb.Append($"{span.Minutes} minute{(span.Minutes > 1 ? "s" : "")}");

                if (span.Seconds > 0)
                    sb.Append(", and ");

                else
                    return sb.ToString();
            }

            if (span.Seconds > 0)
                sb.Append($"{span.Seconds} second{(span.Seconds > 1 ? "s" : "")}");

            return sb.ToString();
        }
    }
}
