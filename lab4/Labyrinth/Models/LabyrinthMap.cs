using OpenTK.Mathematics;

namespace Labyrinth.Models;

public static class LabyrinthMap
{
    public static readonly float[,] Map = new float[16, 16]
    {
        {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0},
        {0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
        {0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0},
        {0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0},
        {0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0},
        {0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0},
        {0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0},
        {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0},
        {0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0},
        {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0},
        {0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0},
        {0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0},
        {0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0},
        {0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };

    public static readonly float MinBoundaryX = -(Map.GetLength(0) / 2f);
    public static readonly float MaxBoundaryX = Map.GetLength(0) / 2f;
    public static readonly float MinBoundaryZ = -(Map.GetLength(1) / 2f);
    public static readonly float MaxBoundaryZ = Map.GetLength(1) / 2f;

    public static Vector3[] GetBlockPositions()
    {
        var blockPositionsList = new List<Vector3>();
        var centerX = Map.GetLength(0) / 2f;
        var centerZ = Map.GetLength(1) / 2f;
        
        for (int row = 0; row < Map.GetLength(0); row++)
        {
            for (int column = 0; column < Map.GetLength(1); column++)
            {
                var block = Map[row, column];
                if (block == 0) continue;

                var position = new Vector3(row - centerX, 0, column - centerZ);
                
                blockPositionsList.Add(position);
            }
        }
        
        return blockPositionsList.ToArray();
    }
}