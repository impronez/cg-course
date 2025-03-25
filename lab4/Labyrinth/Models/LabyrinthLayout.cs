using Labyrinth.Utilities;
using OpenTK.Mathematics;

namespace Labyrinth.Models;

public static class LabyrinthLayout
{
    public static float MinBoundaryX = -8.5f;
    public static float MaxBoundaryX = 8.5f;
    public static float MinBoundaryZ = -8.5f;
    public static float MaxBoundaryZ = 8.5f;
    public static Vector3[] GetBlockPositions()
    {
        return
        [
            new (-7f, 0f, -8f),
            new (3f, 0f, -8f),
            
            new (-5f, 0f, -7f),
            new (-3f, 0f, -7f),
            new (-2f, 0f, -7f),
            new (-1f, 0f, -7f),
            new (0f, 0f, -7f),
            new (1f, 0f, -7f),
            new (3f, 0f, -7f),
            new (5f, 0f, -7f),
            new (6f, 0f, -7f),
            new (7f, 0f, -7f),
            
            new (-7f, 0f, -6f),
            new (-6f, 0f, -6f),
            new (-5f, 0f, -6f),
            new (-3f, 0f, -6f),
            new (-1f, 0f, -6f),
            new (3f, 0f, -6f),
            new (7f, 0f, -6f),
            
            new (-7f, 0f, -5f),
            new (-3f, 0f, -5f),
            new (-1f, 0f, -5f),
            new (1f, 0f, -5f),
            new (2f, 0f, -5f),
            new (3f, 0f, -5f),
            new (4f, 0f, -5f),
            new (5f, 0f, -5f),
            new (7f, 0f, -5f),
            
            new (-7f, 0f, -4f),
            new (-5f, 0f, -4f),
            new (-4f, 0f, -4f),
            new (-3f, 0f, -4f),
            new (-1f, 0f, -4f),
            new (1f, 0f, -4f),
            new (7f, 0f, -4f),
            
            new (-7f, 0f, -3f),
            new (-3f, 0f, -3f),
            new (-1f, 0f, -3f),
            new (1f, 0f, -3f),
            new (2f, 0f, -3f),
            new (3f, 0f, -3f),
            new (5f, 0f, -3f),
            new (7f, 0f, -3f),
            
            new (-8f, 0f, -2f),
            new (-7f, 0f, -2f),
            new (-6f, 0f, -2f),
            new (-5f, 0f, -2f),
            new (-3f, 0f, -2f),
            new (-1f, 0f, -2f),
            new (3f, 0f, -2f),
            new (4f, 0f, -2f),
            new (5f, 0f, -2f),
            
            new (-3f, 0f, -1f),
            new (-1f, 0f, -1f),
            new (0f, 0f, -1f),
            new (1f, 0f, -1f),
            new (3f, 0f, -1f),
            new (7f, 0f, -1f),
            
            new (-7f, 0f, 0f),
            new (-6f, 0f, 0f),
            new (-5f, 0f, 0f),
            new (-4f, 0f, 0f),
            new (-3f, 0f, 0f),
            new (1f, 0f, 0f),
            new (3f, 0f, 0f),
            new (5f, 0f, 0f),
            new (7f, 0f, 0f),
            
            new (-7f, 0f, 1f),
            new (-5f, 0f, 1f),
            new (-1f, 0f, 1f),
            new (1f, 0f, 1f),
            new (5f, 0f, 1f),
            new (7f, 0f, 1f),
            
            new (-7f, 0f, 2f),
            new (-5f, 0f, 2f),
            new (-3f, 0f, 2f),
            new (-1f, 0f, 2f),
            new (0f, 0f, 2f),
            new (1f, 0f, 2f),
            new (2f, 0f, 2f),
            new (3f, 0f, 2f),
            new (4f, 0f, 2f),
            new (5f, 0f, 2f),
            new (7f, 0f, 2f),
            
            new (-7f, 0f, 3f),
            new (-3f, 0f, 3f),
            new (5f, 0f, 3f),
            new (7f, 0f, 3f),
            
            new (-7f, 0f, 4f),
            new (-6f, 0f, 4f),
            new (-5f, 0f, 4f),
            new (-4f, 0f, 4f),
            new (-3f, 0f, 4f),
            new (-1f, 0f, 4f),
            new (0f, 0f, 4f),
            new (1f, 0f, 4f),
            new (3f, 0f, 4f),
            new (5f, 0f, 4f),
            new (7f, 0f, 4f),
            
            new (-7f, 0f, 5f),
            new (-3f, 0f, 5f),
            new (-1f, 0f, 5f),
            new (1f, 0f, 5f),
            new (3f, 0f, 5f),
            new (5f, 0f, 5f),
            
            new (-7f, 0f, 6f),
            new (-5f, 0f, 6f),
            new (-3f, 0f, 6f),
            new (-2f, 0f, 6f),
            new (-1f, 0f, 6f),
            new (1f, 0f, 6f),
            new (3f, 0f, 6f),
            new (5f, 0f, 6f),
            new (6f, 0f, 6f),
            
            new (-5f, 0f, 7f),
            new (1f, 0f, 7f),
            new (3f, 0f, 7f),
        ];
    }

    public static List<RGBAVertex> GetBoxVertices(Color4 sideColor, Color4 upSideColor, Color4 bottomSideColor)
    {
        return
        [
            // Верх
            new (-8.5f, 0.5f, 8.5f, upSideColor, -Vector3.UnitY),
            new (8.5f, 0.5f, 8.5f, upSideColor, -Vector3.UnitY),
            new (8.5f, 0.5f, -8.5f, upSideColor, -Vector3.UnitY),
            new (-8.5f, 0.5f, -8.5f, upSideColor, -Vector3.UnitY),
            // Низ
            new (-8.5f, -0.5f, 8.5f, bottomSideColor, Vector3.UnitY),
            new (8.5f, -0.5f, 8.5f, bottomSideColor, Vector3.UnitY),
            new (8.5f, -0.5f, -8.5f, bottomSideColor, Vector3.UnitY),
            new (-8.5f, -0.5f, -8.5f, bottomSideColor, Vector3.UnitY),
            // Сзади
            new (-8.5f, -0.5f, 8.5f, sideColor, -Vector3.UnitZ),
            new (8.5f, -0.5f, 8.5f, sideColor, -Vector3.UnitZ),
            new (8.5f, 0.5f, 8.5f, sideColor, -Vector3.UnitZ),
            new (-8.5f, 0.5f, 8.5f, sideColor, -Vector3.UnitZ),
            // Слева
            new (-8.5f, -0.5f, 8.5f, sideColor, Vector3.UnitX),
            new (-8.5f, -0.5f, -8.5f, sideColor, Vector3.UnitX),
            new (-8.5f, 0.5f, -8.5f, sideColor, Vector3.UnitX),
            new (-8.5f, 0.5f, 8.5f, sideColor, Vector3.UnitX),
            // Спереди
            new (-8.5f, -0.5f, -8.5f, sideColor, Vector3.UnitZ),
            new (8.5f, -0.5f, -8.5f, sideColor, Vector3.UnitZ),
            new (8.5f, 0.5f, -8.5f, sideColor, Vector3.UnitZ),
            new (-8.5f, 0.5f, -8.5f, sideColor, Vector3.UnitZ),
            // Справа
            new (8.5f, -0.5f, 8.5f, sideColor, -Vector3.UnitX),
            new (8.5f, -0.5f, -8.5f, sideColor, -Vector3.UnitX),
            new (8.5f, 0.5f, -8.5f, sideColor, -Vector3.UnitX),
            new (8.5f, 0.5f, 8.5f, sideColor, -Vector3.UnitX),
        ];
    }

    public static readonly int[] BoxIndices =
    [
        3, 2, 1, 0, 4, 5, 6, 7, 11, 10, 9, 8, 12, 13, 14, 15, 16, 17, 18, 19, 23, 22, 21, 20
    ];
}