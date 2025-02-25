using OpenTK.Mathematics;

namespace Parabola
{
    public struct ParabolaArgs
    {
        public float MinValue;
        public float MaxValue;

        public Func<float, float> Function;

        public Color4 Color;
    }

    public static class Parabola
    {
        private static readonly float Step = 0.01f;

        public static List<float> GetVertices(Func<float, float> function,
            float minValue, float maxValue, Color4 color)
        {
            List<float> vertices = new List<float>();

            for (float x = minValue; x < maxValue; x += Step)
            {
                vertices.AddRange([x / 10, function(x) / 10, 0f, color.R, color.G, color.B ]);
            }

            return vertices;
        }
    }
}
