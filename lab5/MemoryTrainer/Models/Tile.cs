namespace MemoryTrainer.Models;

public enum TileType
{
    Apple,
    Banana,
    Cherry,
    Grape,
    Lemon,
    Orange,
    Plum,
    Pineapple,
    Strawberry,
    Watermelon
}

public class Tile(TileType type)
{
    private bool _isSelected;

    public bool IsGuessed { get; set; }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;
            _isSelected = value;
            OnSelectedChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public TileType Type { get; } = type;

    public event EventHandler? OnSelectedChanged;
}