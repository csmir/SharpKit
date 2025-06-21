using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        var (r, g, b) = ((float)color.R, (float)color.G, (float)color.B);
        var lum = Math.Sqrt(_rTarget * r + _gTarget * g + _bTarget * b);
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
