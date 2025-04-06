using OpenTK.Mathematics;

namespace TextureLabyrinth.Utilities;

public struct RGBAVertex
{
    public const int Size = 10;
    public const int ColorOffset = 3;
    public const int NormalOffset = 7;

    public readonly Vector3 Position;
    public readonly Color4 Color;
    public readonly Vector3 Normal;

    public RGBAVertex(Vector3 position, Color4 color = default, Vector3 normal = default)
    {
        Position = position;
        Color = color == default ? Color4.Black : color;
        Normal = normal == default ? Vector3.UnitY : normal;
    }
    
    public RGBAVertex(float x, float y, float z, Color4 color = default, Vector3 normal = default)
    {
        Position = new Vector3(x, y, z);
        Color = color == default ? Color4.Black : color;
        Normal = normal == default ? Vector3.UnitY : normal;
    }

    public float[] ToArray()
    {
        return [
            Position.X, Position.Y, Position.Z, 
            Color.R, Color.G, Color.B, Color.A,
            Normal.X, Normal.Y, Normal.Z
        ];
    }
}