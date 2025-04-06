using OpenTK.Mathematics;

namespace MobiusStrip.Utilities;

public struct RGBAVertex
{
    public static readonly int Size = 7;
    public static readonly int ColorOffset = 3;

    public readonly Vector3 Position;
    
    public readonly Color4 Color;

    public RGBAVertex(Vector3 position, Color4 color = default)
    {
        Position = position;
        Color = color == default ? Color4.Black : color;
    }
    
    public RGBAVertex(float x, float y, float z, Color4 color = default)
    {
        Position = new Vector3(x, y, z);
        Color = color == default ? Color4.Black : color;
    }

    public float[] ToArray()
    {
        return [Position.X, Position.Y, Position.Z, Color.R, Color.G, Color.B, Color.A];
    }
}