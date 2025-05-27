using OpenTK.Mathematics;

namespace TextureLabyrinth.Models;

public static class LabyrinthMap
{
    private static readonly int[,] Map = new int[16, 16]
    {
        {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
        {4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
        {4, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 0, 4},
        {4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
        {4, 0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 0, 5, 5, 4},
        {4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
        {4, 0, 2, 0, 2, 0, 2, 0, 2, 2, 0, 2, 0, 2, 0, 4},
        {4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
        {4, 0, 5, 0, 5, 0, 5, 5, 5, 5, 5, 5, 0, 1, 0, 4},
        {4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
        {4, 0, 6, 0, 6, 6, 6, 6, 6, 6, 6, 6, 0, 6, 0, 4},
        {4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
        {4, 0, 5, 5, 5, 5, 5, 5, 5, 5, 0, 5, 0, 5, 0, 4},
        {4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
        {4, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 4},
        {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4}
    };

    public static (Vector3 Position, BlockType Type)[] GetBlocksSorteddByBlockType()
    {
        var blockPositionsList = new List<(Vector3, BlockType)>();
        var centerX = Map.GetLength(0) / 2f;
        var centerZ = Map.GetLength(1) / 2f;
        
        for (int row = 0; row < Map.GetLength(0); row++)
        {
            for (int column = 0; column < Map.GetLength(1); column++)
            {
                var blockType = (BlockType)Map[row, column];
                if (blockType == BlockType.None) continue;

                var position = new Vector3(row - centerX, 0, column - centerZ);
                
                blockPositionsList.Add((position, blockType));
            }
        }
        
        
        return blockPositionsList.OrderBy(x => x.Item2).ToArray();
    }
    
    public static Vector3[] GetEmptyPositions()
    {
        var blockPositionsList = new List<Vector3>();
        var centerX = Map.GetLength(0) / 2f;
        var centerZ = Map.GetLength(1) / 2f;
        
        for (int row = 0; row < Map.GetLength(0); row++)
        {
            for (int column = 0; column < Map.GetLength(1); column++)
            {
                var block = Map[row, column];
                if (block > 0) continue;

                var position = new Vector3(row - centerX, 0, column - centerZ);
                
                blockPositionsList.Add(position);
            }
        }
        
        return blockPositionsList.ToArray();
    }
}