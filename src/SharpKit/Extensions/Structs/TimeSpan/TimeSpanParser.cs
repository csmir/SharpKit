using System.Text.RegularExpressions;

namespace SharpKit;

internal static class TimeSpanParser
{
    private static readonly Dictionary<string, Func<string, TimeSpan>> _callback;
    private static readonly Regex _timeRegex;

    static TimeSpanParser()
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

    public static bool TryParseFuzzy(string input, out TimeSpan result)
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
