using OpenTK.Mathematics;
using System.Drawing;

namespace Tetris.Utilities
{
    static class ColorToColor4Converter
    {
        public static Color4 Convert(Color? color)
        {
            if (color == null)
                return Color4.Black;

            float r = color.Value.R / 255.0f;
            float g = color.Value.G / 255.0f;
            float b = color.Value.B / 255.0f;
            float a = color.Value.A / 255.0f;

            return new Color4(r, g, b, a);
        }
    }
}