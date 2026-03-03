using System.Drawing;

namespace Nimble.Drawing;

public readonly partial struct Composite
{
    /// <summary>
    ///     Creates a new <see cref="Composite"/> from the specified red, green, blue, and alpha values.
    /// </summary>
    /// <param name="r">The red channel to create this color from.</param>
    /// <param name="g">The green channel to create this color from.</param>
    /// <param name="b">The blue channel to create this color from.</param>
    /// <param name="a">The alpha channel to create this color from. This parameter is optional and defaults to 255 (fully opaque) if not provided.</param>
    /// <returns>A new <see cref="Composite"/> value from the provided values.</returns>
    public static Composite FromRGB(byte r, byte g, byte b, byte a = byte.MaxValue)
        => new(r, g, b, a);

    /// <summary>
    ///     Creates a new <see cref="Composite"/> from the specified hue, saturation, and value (brightness) components.
    /// </summary>
    /// <param name="h">The hue to create this color from, in a range between 0 and 360 degrees.</param>
    /// <param name="s">The saturation to create this color from, in a range between 0 and 1.</param>
    /// <param name="v">The value (brightness) to create this color from, in a range between 0 and 1.</param>
    /// <returns>A new <see cref="Composite"/> value from the provided values.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any of the provided values are less than, or more than the accepted range.</exception>
    public static Composite FromHSV(float h, float s, float v)
    {
        ArgumentOutOfRangeException.ThrowIfOutOfRange(h, 0f, MAX_DEGREES, nameof(h));
        ArgumentOutOfRangeException.ThrowIfOutOfRange(s, 0f, 1f, nameof(s));
        ArgumentOutOfRangeException.ThrowIfOutOfRange(v, 0f, 1f, nameof(v));

        return new(h, s, v);
    }

    /// <summary>
    ///     Creates a new <see cref="Composite"/> from the specified hue, saturation, lightness, and alpha components.
    /// </summary>
    /// <param name="h">The hue to create this color from, in a range between 0 and 360 degrees.</param>
    /// <param name="s">The saturation to create this color from, in a range between 0 and 1.</param>
    /// <param name="l">The lightness to create this color from, in a range between 0 and 1.</param>
    /// <param name="a">The alpha to create this color from, in a range between 0 and 1. This parameter is optional and defaults to 1 (fully opaque) if not provided.</param>
    /// <returns>A new <see cref="Composite"/> value from the provided values.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any of the provided values are less than, or more than the accepted range.</exception>
    public static Composite FromHSL(float h, float s, float l, float a = 1f)
    {
        ArgumentOutOfRangeException.ThrowIfOutOfRange(h, 0f, MAX_DEGREES, nameof(h));
        ArgumentOutOfRangeException.ThrowIfOutOfRange(s, 0f, 1f, nameof(s));
        ArgumentOutOfRangeException.ThrowIfOutOfRange(l, 0f, 1f, nameof(l));
        ArgumentOutOfRangeException.ThrowIfOutOfRange(a, 0f, 1f, nameof(a));

        return new(h, s, l, a);
    }

    /// <summary>
    ///     Creates a new <see cref="Composite"/> instance that represents the specified <see cref="Color"/> value.
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert to a <see cref="Composite"/>.</param>
    /// <returns>A <see cref="Composite"/> instance that encapsulates the value of the specified <see cref="Color"/>.</returns>
    public static Composite FromColor(Color color)
    {
#if NET6_0_OR_GREATER
        var value = unchecked((uint)GetValue(color));
#else
        var value = unchecked((uint)(long)typeof(Color)
            .GetProperty("Value", BindingFlags.NonPublic | BindingFlags.Instance)!
            .GetValue(color));
#endif

        return new(value);
    }
}
