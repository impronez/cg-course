using OpenTK.Mathematics;

namespace _3dsScene.Utilities;

public struct Vertex(Vector3 position, Vector3 normal, Color4 color)
{
    public const int Size = 10;
    public const int PositionOffset = 0;
    public const int NormalOffset = 3;
    public const int ColorOffset = 6;
    
    public Vector3 Position = position;
    public Vector3 Normal = normal;
    public Color4 Color = color;

    public float[] ToFloatArray()
    {
        return
        [
            Position.X, Position.Y, Position.Z,
            Normal.X, Normal.Y, Normal.Z,
            Color.R, Color.G, Color.B, Color.A
        ];
    }
}