using Labyrinth.Utilities;
using OpenTK.Mathematics;

namespace Labyrinth.Models;

public static class LabyrinthLayout
{
    public static List<RGBAVertex> GetLabyrinthBoundaryVertices(Color4 sideColor, Color4 upSideColor,
        Color4 bottomSideColor)
    {
        var halfSizeX = LabyrinthMap.Map.GetLength(0) / 2f;
        var halfSizeZ = LabyrinthMap.Map.GetLength(1) / 2f;

        return
        [
            // Верхняя грань
            new(-halfSizeX, 1f, halfSizeZ, upSideColor, -Vector3.UnitY),
            new(halfSizeX, 1f, halfSizeZ, upSideColor, -Vector3.UnitY),
            new(halfSizeX, 1f, -halfSizeZ, upSideColor, -Vector3.UnitY),
            new(-halfSizeX, 1f, -halfSizeZ, upSideColor, -Vector3.UnitY),

            // Нижняя грань
            new(-halfSizeX, 0f, halfSizeZ, bottomSideColor, Vector3.UnitY),
            new(halfSizeX, 0f, halfSizeZ, bottomSideColor, Vector3.UnitY),
            new(halfSizeX, 0f, -halfSizeZ, bottomSideColor, Vector3.UnitY),
            new(-halfSizeX, 0f, -halfSizeZ, bottomSideColor, Vector3.UnitY),

            // Задняя грань (Z+)
            new(-halfSizeX, 0f, halfSizeZ, sideColor, -Vector3.UnitZ),
            new(halfSizeX, 0f, halfSizeZ, sideColor, -Vector3.UnitZ),
            new(halfSizeX, 1f, halfSizeZ, sideColor, -Vector3.UnitZ),
            new(-halfSizeX, 1f, halfSizeZ, sideColor, -Vector3.UnitZ),

            // Левая грань (X-)
            new(-halfSizeX, 0f, halfSizeZ, sideColor, Vector3.UnitX),
            new(-halfSizeX, 0f, -halfSizeZ, sideColor, Vector3.UnitX),
            new(-halfSizeX, 1f, -halfSizeZ, sideColor, Vector3.UnitX),
            new(-halfSizeX, 1f, halfSizeZ, sideColor, Vector3.UnitX),

            // Передняя грань (Z-)
            new(-halfSizeX, 0f, -halfSizeZ, sideColor, Vector3.UnitZ),
            new(halfSizeX, 0f, -halfSizeZ, sideColor, Vector3.UnitZ),
            new(halfSizeX, 1f, -halfSizeZ, sideColor, Vector3.UnitZ),
            new(-halfSizeX, 1f, -halfSizeZ, sideColor, Vector3.UnitZ),

            // Правая грань (X+)
            new(halfSizeX, 0f, halfSizeZ, sideColor, -Vector3.UnitX),
            new(halfSizeX, 0f, -halfSizeZ, sideColor, -Vector3.UnitX),
            new(halfSizeX, 1f, -halfSizeZ, sideColor, -Vector3.UnitX),
            new(halfSizeX, 1f, halfSizeZ, sideColor, -Vector3.UnitX)
        ];
    }

    public static readonly int[] BoxIndices =
    [
        3, 2, 1, 0, 4, 5, 6, 7, 11, 10, 9, 8, 12, 13, 14, 15, 16, 17, 18, 19, 23, 22, 21, 20
    ];
}