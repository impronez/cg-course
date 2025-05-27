namespace MemoryTrainer.Models;

public class GameField
{
    public readonly Tile[,] Tiles;

    public GameField(int rows, int columns)
    {
        if (rows * columns % 2 != 0)
            throw new ArgumentException("Rows and columns must be odd");
        
        Tiles = new Tile[rows, columns];
    }

    public bool IsEmpty()
    {
        return Tiles.Cast<Tile?>().All(tile => tile!.IsGuessed);
    }

    public void Fill()
    {
        var totalTiles = Tiles.GetLength(0) * Tiles.GetLength(1);

        if (totalTiles % 2 != 0)
            throw new InvalidOperationException("Tiles count must be odd");

        var allTypes = Enum.GetValues(typeof(TileType)).Cast<TileType>().ToList();

        if (totalTiles / 2 > allTypes.Count)
            throw new InvalidOperationException("Tile types count is small then tiles count");

        var selectedTypes = allTypes.Take(totalTiles / 2).ToList();

        var tilePairs = selectedTypes
            .SelectMany(type => new[] { type, type })
            .OrderBy(_ => Guid.NewGuid())
            .ToList();

        var index = 0;
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                Tiles[i, j] = new Tile(tilePairs[index++]);
            }
        }
    }

    public bool IsContains(int row, int column)
    {
        return row < Tiles.GetLength(0) && column < Tiles.GetLength(1);
    }
}