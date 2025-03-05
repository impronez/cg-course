using OpenTK.Mathematics;

namespace Tetris.Utilities
{
    public class RGBVertex
    {
        public const int ColorIndex = 3;
        public const int VertexSize = 6;

        public Vector3 Position;
        public Color4 Color;

        public RGBVertex(Vector3 position, Color4 color)
        {
            Position = position;
            Color = color;
        }

        public RGBVertex(Vector2 position, Color4 color)
        {
            Position = new Vector3(position.X, position.Y, 0f);
            Color = color;
        }

        public RGBVertex(float x, float y, Color4 color)
        {
            Position = new Vector3(x, y, 0f);
            Color = color;
        }

        public RGBVertex(float x, float y, float z, Color4 color)
        {
            Position = new Vector3(x, y, z);
            Color = color;
        }

        public static float[] ToFloatArray(RGBVertex vertex)
        {
            return
            [
                vertex.Position.X,
                vertex.Position.Y,
                vertex.Position.Z,
                vertex.Color.R,
                vertex.Color.G,
                vertex.Color.B
            ];
        }
    }
}
