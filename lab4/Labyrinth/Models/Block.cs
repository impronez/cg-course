using Labyrinth.Utilities;
using OpenTK.Mathematics;

namespace Labyrinth.Models;

public static class Block
{
    public const float Size = 1f;
    
    public static readonly int[] SideIndices =
    [
        0, 1, 2, 3, // Front face
        4, 5, 6, 7, // Back face
        8, 9, 10, 11, // Left face
        12, 13, 14, 15, // Right face
        16, 17, 18, 19, // Bottom face
        20, 21, 22, 23 // Top face
    ];

    public static readonly int[] EdgeIndices =
    [
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,
        13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23
    ];

    public static List<RGBAVertex> GetEdgeVerticesList(Color4 color)
    {
        return new List<RGBAVertex>
        {
            new (0f, 1f, 1f, color),
            new (0f, 1f, 0f, color),

            new (0f, 1f, 0f, color),
            new (1f, 1f, 0f, color),
            
            new (1f, 1f, 0f, color),
            new (1f, 1f, 1f, color),
            
            new (0f, 1f, 1f, color),
            new (1f, 1f, 1f, color),
            
            new (0f, 0f, 1f, color),
            new (0f, 0f, 0f, color),
            
            new (0f, 0f, 0f, color),
            new (1f, 0f, 0f, color),
            
            new (1f, 0f, 0f, color),
            new (1f, 0f, 1f, color),
            
            new (1f, 0f, 1f, color),
            new (0f, 0f, 1f, color),
            
            new (0f, 1f, 1f, color),
            new (0f, 0f, 1f, color),
            
            new (0f, 1f, 0f, color),
            new (0f, 0f, 0f, color),
            
            new (1f, 1f, 0f, color),
            new (1f, 0f, 0f, color),
            
            new (1f, 1f, 1f, color),
            new (1f, 0f, 1f, color)
        };
    }

    public static List<RGBAVertex> GetVerticesList(Color4 color)
    {
        return new List<RGBAVertex>()
        {
            // Спереди 
            new (0f, 0f, 0f, color, -Vector3.UnitZ),
            new (0f, 1f, 0f, color, -Vector3.UnitZ),
            new (1f, 1f, 0f, color, -Vector3.UnitZ),
            new (1f, 0f, 0f, color, -Vector3.UnitZ),
            // Сзади
            new (0f, 0f, 1f, color, Vector3.UnitZ),
            new (1f, 0f, 1f, color, Vector3.UnitZ),
            new (1f, 1f, 1f, color, Vector3.UnitZ),
            new (0f, 1f, 1f, color, Vector3.UnitZ),
            // Слева
            new (0f, 0f, 0f, color, -Vector3.UnitX),
            new (0f, 0f, 1f, color, -Vector3.UnitX),
            new (0f, 1f, 1f, color, -Vector3.UnitX),
            new (0f, 1f, 0f, color, -Vector3.UnitX),
            // Справа
            new (1f, 0f, 0f, color, Vector3.UnitX),
            new (1f, 1f, 0f, color, Vector3.UnitX),
            new (1f, 1f, 1f, color, Vector3.UnitX),
            new (1f, 0f, 1f, color, Vector3.UnitX),
            // Снизу
            new (0f, 0f, 0f, color, -Vector3.UnitY),
            new (1f, 0f, 0f, color, -Vector3.UnitY),
            new (1f, 0f, 1f, color, -Vector3.UnitY),
            new (0f, 0f, 1f, color, -Vector3.UnitY),
            // Сверху
            new (0f, 1f, 0f, color, Vector3.UnitY),
            new (0f, 1f, 1f, color, Vector3.UnitY),
            new (1f, 1f, 1f, color, Vector3.UnitY),
            new (1f, 1f, 0f, color, Vector3.UnitY)
        };
    }
}