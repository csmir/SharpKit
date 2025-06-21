using System.Text;
using System.Text.RegularExpressions;

namespace SharpKit.Extensions;

public static class DateTimeExtensions
{
    private static readonly Dictionary<string, Func<string, TimeSpan>> _callback;
    private static readonly Regex _timeRegex;

    static DateTimeExtensions()
    {
        _timeRegex = new Regex(@"(\d+)\s*([a-zA-Z]+)\s*(?:and|,)?\s*", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        _callback = new()
        {
            ["second"] = Seconds,
            ["seconds"] = Seconds,
            ["sec"] = Seconds,
            ["s"] = Seconds,
            ["minute"] = Minutes,
            ["minutes"] = Minutes,
            ["min"] = Minutes,
            ["m"] = Minutes,
            ["hour"] = Hours,
            ["hours"] = Hours,
            ["h"] = Hours,
            ["day"] = Days,
            ["days"] = Days,
            ["d"] = Days,
            ["week"] = Weeks,
            ["weeks"] = Weeks,
            ["w"] = Weeks,
            ["month"] = Months,
            ["months"] = Months
        };
    }

    extension(TimeSpan span)
    {
        public static bool TryParseFuzzy(string input,
#if NET6_0_OR_GREATER 
            [NotNullWhen(true)]
#endif
            out TimeSpan result)
        {
            result = default;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            if (!TimeSpan.TryParse(input, out result))
            {
                var matches = _timeRegex.Matches(input.ToLower().Trim());

                if (matches.Count != 0)
                {
                    foreach (Match match in matches)
                    {
                        if (_callback.TryGetValue(match.Groups[2].Value, out var callback))
                            result += callback(match.Groups[1].Value);
                    }

                    return true;
                }
                return false;
            }
            return true;
        }

        public string ToFormattedString()
        {
            var sb = new StringBuilder();

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

    private static TimeSpan Seconds(string match)
        => new(0, 0, int.Parse(match));

    private static TimeSpan Minutes(string match)
        => new(0, int.Parse(match), 0);

    private static TimeSpan Hours(string match)
        => new(int.Parse(match), 0, 0);

    private static TimeSpan Days(string match)
        => new(int.Parse(match), 0, 0, 0);

    private static TimeSpan Weeks(string match)
        => new(int.Parse(match) * 7, 0, 0, 0);

    private static TimeSpan Months(string match)
        => new((int)(int.Parse(match) * 30.437), 0, 0, 0);
}
