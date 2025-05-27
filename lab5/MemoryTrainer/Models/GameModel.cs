using System.Timers;
using Timer = System.Timers.Timer;

namespace MemoryTrainer.Models;

public enum GameState
{
    None,
    Playing,
    Win
}

public class GameModel
{
    private (int row, int column)? _selectedTilePosition;
    public GameField Field;
    private Timer? _deselectTimer;
    private bool _isCanSelect = true;

    public GameModel(int rows, int columns)
    {
        Field = new GameField(rows, columns);
        State = GameState.None;
    }

    public GameState State { get; set; }

    public void Start()
    {
        Field.Fill();
        State = GameState.Playing;
    }

    public void SelectTile(int row, int column)
    {
        if (!Field.IsContains(row, column))
            throw new ArgumentException($"Row {row}:{column} is not contained in field");
        var tile = Field.Tiles[row, column];
        if (tile.IsGuessed || !_isCanSelect) return;
        
        if (tile.IsSelected)
        {
            tile.IsSelected = false;
            _selectedTilePosition = null;
            return;
        }

        tile.IsSelected = true;
        if (_selectedTilePosition == null)
        {
            _selectedTilePosition = (row, column);
            return;
        }

        _isCanSelect = false;
        
        var otherTile = Field.Tiles[_selectedTilePosition.Value.row, _selectedTilePosition.Value.column];
        if (tile.Type == otherTile.Type)
        {
            tile.IsGuessed = true;
            otherTile.IsGuessed = true;

            UpdateState();
        }

        ResetSelectedTiles(tile, otherTile);
    }

    private void ResetSelectedTiles(Tile tile, Tile otherTile)
    {
        _deselectTimer = new Timer(1200);
        _deselectTimer.Elapsed += (_, _) =>
        {
            _deselectTimer!.Stop();
            _deselectTimer.Dispose();
            _deselectTimer = null;

            tile.IsSelected = false;
            otherTile.IsSelected = false;

            _selectedTilePosition = null;

            _isCanSelect = true;
        };
        _deselectTimer.AutoReset = false;
        _deselectTimer.Start();
    }

    private void UpdateState()
    {
        if (Field.IsEmpty())
        {
            State = GameState.Win;
        }
    }
}