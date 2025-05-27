using OpenTK.Mathematics;

namespace TextureLabyrinth.Utilities;

public struct TexVertex
{
    public const int Size = 8;
    public const int PositionOffset = 0;
    public const int TexCoordOffset = 3;
    public const int NormalOffset = 5;
    
    public readonly Vector3 Position;
    public readonly Vector2 TexCoord;
    public readonly Vector3 Normal;

    public TexVertex(Vector3 position, Vector2 texCoord, Vector3 normal = default)
    {
        Position = position;
        TexCoord = texCoord;
        Normal = normal == default ? Vector3.UnitY : normal;
    }
    
    public TexVertex(float x, float y, float z, float texX, float texY, Vector3 normal = default)
    {
        Position = new Vector3(x, y, z);
        TexCoord = new Vector2(texX, texY);
        Normal = normal == default ? Vector3.UnitY : normal;
    }
    
    public TexVertex(Vector3 position, float texX, float texY, Vector3 normal = default)
    {
        Position = position;
        TexCoord = new Vector2(texX, texY);
        Normal = normal == default ? Vector3.UnitY : normal;
    }

    public float[] ToArray()
    {
        return
        [
            Position.X, Position.Y, Position.Z,
            TexCoord.X, TexCoord.Y,
            Normal.X, Normal.Y, Normal.Z
        ];
    }
}