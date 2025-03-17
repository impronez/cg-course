using OpenTK.Mathematics;

namespace Cuboctahedron.Utilities;

public struct RGBVertex
{
    public const int Size = 6;
    public const int ColorIndex = 3;

    public readonly Vector3 Position;
    
    public readonly Color4 Color;

    public RGBVertex(Vector3 position, Color4 color = default)
    {
        Position = position;
        Color = color == default ? Color4.Black : color;
    }

    public float[] ToArray()
    {
        return [Position.X, Position.Y, Position.Z, Color.R, Color.G, Color.B];
    }
}