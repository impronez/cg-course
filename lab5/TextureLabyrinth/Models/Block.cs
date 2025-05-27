using OpenTK.Mathematics;
using TextureLabyrinth.Utilities;

namespace TextureLabyrinth.Models;

public static class Block
{
    public const float Size = 1f;

    public static readonly int[] SideIndices =
    [
        0, 1, 2, 3, // Front face
        4, 5, 6, 7, // Back face
        8, 9, 10, 11, // Left face
        12, 13, 14, 15 // Right face
    ];

    public static readonly int[] InnerBottomSideIndices =
    [
        0, 1, 2, 3
    ];

    public static readonly int[] InnerUpSideIndices =
    [
        3, 2, 1, 0
    ];

    public static readonly List<TexVertex> BottomSideVerticesList = new()
    {
        new(0f, 0f, 0f, 0f, 0f, Vector3.UnitY),
        new(0f, 0f, 1f, 0f, 1f, Vector3.UnitY),
        new(1f, 0f, 1f, 1f, 1f, Vector3.UnitY),
        new(1f, 0f, 0f, 1f, 0f, Vector3.UnitY)
    };

    public static readonly List<TexVertex> UpSideVerticesList = new()
    {
        new(0f, 1f, 0f, 0f, 0f, -Vector3.UnitY),
        new(0f, 1f, 1f, 0f, 1f, -Vector3.UnitY),
        new(1f, 1f, 1f, 1f, 1f, -Vector3.UnitY),
        new(1f, 1f, 0f, 1f, 0f, -Vector3.UnitY)
    };

    public static readonly List<TexVertex> SidesVerticesList = new()
    {
        // Front
        new(0f, 0f, 0f, 0f, 0f, -Vector3.UnitZ), // bottom-left
        new(0f, 1f, 0f, 0f, 1f, -Vector3.UnitZ), // top-left
        new(1f, 1f, 0f, 1f, 1f, -Vector3.UnitZ), // top-right
        new(1f, 0f, 0f, 1f, 0f, -Vector3.UnitZ), // bottom-right

        // Back
        new(0f, 0f, 1f, 1f, 0f, Vector3.UnitZ),
        new(1f, 0f, 1f, 0f, 0f, Vector3.UnitZ),
        new(1f, 1f, 1f, 0f, 1f, Vector3.UnitZ),
        new(0f, 1f, 1f, 1f, 1f, Vector3.UnitZ),

        // Left
        new(0f, 0f, 0f, 1f, 0f, -Vector3.UnitX),
        new(0f, 0f, 1f, 0f, 0f, -Vector3.UnitX),
        new(0f, 1f, 1f, 0f, 1f, -Vector3.UnitX),
        new(0f, 1f, 0f, 1f, 1f, -Vector3.UnitX),

        // Right
        new(1f, 0f, 0f, 0f, 0f, Vector3.UnitX),
        new(1f, 1f, 0f, 0f, 1f, Vector3.UnitX),
        new(1f, 1f, 1f, 1f, 1f, Vector3.UnitX),
        new(1f, 0f, 1f, 1f, 0f, Vector3.UnitX)
    };

    public static readonly float[] SidesVertices = SidesVerticesList.SelectMany(v => v.ToArray()).ToArray();
    public static readonly float[] UpSideVertices = UpSideVerticesList.SelectMany(v => v.ToArray()).ToArray();
    public static readonly float[] BottomSideVertices = BottomSideVerticesList.SelectMany(v => v.ToArray()).ToArray();
}