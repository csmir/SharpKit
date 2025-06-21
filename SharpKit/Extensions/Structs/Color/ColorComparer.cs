using System.Drawing;

namespace SharpKit;

internal sealed class ColorComparer : IComparer<Color>
{
    private const double _rTarget = .241;
    private const double _gTarget = .691;
    private const double _bTarget = .068;

    public int Compare(Color x, Color y) 
        => GetCoding(x).CompareTo(GetCoding(y));

    private double GetCoding(Color color, int repetitions = 8)
    {
        var (r, g, b) = (color.R, color.G, color.B);
        var lum = Math.Sqrt((r * _rTarget) + (g * _gTarget) + (b * _bTarget));
        var (h, s, v) = color.ToHSV();

        h *= repetitions;
        lum *= repetitions;
        v *= repetitions;

        if (h % 2 is 1)
        {
            v = repetitions - v;
            lum = repetitions - lum;
        }

        return h + lum + v + s;
    }
}
