using System.Text;

namespace SharpKit;

public static class StructExtensions
{
    extension(TimeSpan span)
    {
        public static bool TryParseFuzzy(string input,
#if NET6_0_OR_GREATER 
            [NotNullWhen(true)]
#endif
            out TimeSpan result) => TimeSpanParser.TryParseFuzzy(input, out result);

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
