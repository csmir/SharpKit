using System.Drawing;
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

    extension(Color color)
    {
        public (double H, double S, double L) ToHSL()
            => (color.GetHue(), color.GetSaturation(), color.GetBrightness());

        public (double H, double S, double V) ToHSV()
        {
            double max = Math.Max(color.R, Math.Max(color.G, color.B));
            double min = Math.Min(color.R, Math.Min(color.G, color.B));

            return (color.GetHue(), max == 0 ? 0 : 1d - 1d * min / max, max / 255d);
        }

        public static IEnumerable<Color> Gradient(Color start, Color target, int steps)
        {
            var (rMin, gMin, bMin) = (start.R,  start.G,  start.B);
            var (rMax, gMax, bMax) = (target.R, target.G, target.B);

            for (int i = 0; i < steps; i++)
            {
                var r = rMin + (rMax - rMin) * i / steps;
                var g = gMin + (gMax - gMin) * i / steps;
                var b = bMin + (bMax - bMin) * i / steps;

                yield return Color.FromArgb(r, g, b);
            }

            yield return target;
        }

        public static IEnumerable<Color> Sort(IEnumerable<Color> colors, bool ascending = true)
        {
            var comparer = new ColorComparer();

            return ascending ? colors.OrderBy(c => c, comparer) : colors.OrderByDescending(c => c, comparer);
        }
    }
}
