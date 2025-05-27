using MemoryTrainer.Models;
using OpenTK.Mathematics;

namespace MemoryTrainer.ViewModels;

public class GameViewModel
{
    private readonly GameModel _gameModel;
    public List<TileViewModel> TileViewModels;

    public int Rows => _gameModel.Field.Tiles.GetLength(0);
    public int Columns => _gameModel.Field.Tiles.GetLength(1);

    public GameState State => _gameModel.State;
    public event EventHandler<GameState> GameStateChanged;

    public GameViewModel(GameModel gameModel)
    {
        _gameModel = gameModel;
    }

    public void Start(int rows, int columns)
    {
        _gameModel.Start(rows, columns);
        TileViewModels = new List<TileViewModel>(rows * columns);
        InitializeTileViewModels();
    }

    public void Update(float deltaTime)
    {
        foreach (var tileViewModel in TileViewModels)
        {
            tileViewModel.Update(deltaTime);
        }
    }

    public void SelectTile(int row, int column)
    {
        _gameModel.SelectTile(row, column);

        OnGameStateChanged(_gameModel.State);
    }

    private void InitializeTileViewModels()
    {
        var index = 0;
        
        var offsetX = _gameModel.Field.Tiles.GetLength(0) / 2f - 0.5f;
        var offsetY = _gameModel.Field.Tiles.GetLength(1) / 2f - 0.5f;
        
        for (int i = 0; i < _gameModel.Field.Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _gameModel.Field.Tiles.GetLength(1); j++)
            {
                var tile = _gameModel.Field.Tiles[i, j];
                TileViewModels.Insert(index++, new TileViewModel(tile, new Vector3(i - offsetX, j - offsetY, 0f)));
            }
        }
    }

    private void OnGameStateChanged(GameState newState)
    {
        GameStateChanged?.Invoke(this, newState);
    }
}