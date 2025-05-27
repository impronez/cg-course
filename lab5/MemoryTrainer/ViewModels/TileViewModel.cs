using MemoryTrainer.Models;
using OpenTK.Mathematics;

namespace MemoryTrainer.ViewModels;

public class TileViewModel
{
    private const float FlipSpeed = 200f;
    
    private readonly Tile _tile;
    
    public readonly Vector3 Position;
    
    public TileType Type => _tile.Type;
    public bool IsGuessed => _tile.IsGuessed;
    public bool IsSelected => _tile.IsSelected;
    public float FlipAngle { get; private set; }
    private bool IsFlipping { get; set; }
    
    public TileViewModel(Tile tile, Vector3 position)
    {
        _tile = tile;
        FlipAngle = _tile.IsSelected ? 0f : 180f;
        Position = position;

        _tile.OnSelectedChanged += (_, _) => StartFlip();
    }

    private void StartFlip()
    {
        IsFlipping = true;
    }

    public void Update(float deltaTime)
    {
        if (!IsFlipping) return;

        if (_tile.IsSelected)
            FlipAngle -= FlipSpeed * deltaTime;
        else
            FlipAngle += FlipSpeed * deltaTime;

        if ((_tile.IsSelected && FlipAngle <= 0f) || (!_tile.IsSelected && FlipAngle >= 180f))
        {
            FlipAngle = _tile.IsSelected ? 0f : 180f;
            IsFlipping = false;
        }
    }
}
