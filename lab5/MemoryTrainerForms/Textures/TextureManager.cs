using MemoryTrainer.Models;

namespace MemoryTrainer.Textures;

public static class TextureManager
{
    public static readonly Dictionary<TileType, Texture> TileTypeToTexture = new()
    {
        { TileType.Apple, Texture.LoadFromFile("Images/apple.png") },
        { TileType.Banana, Texture.LoadFromFile("Images/banana.jpg") },
        { TileType.Cherry, Texture.LoadFromFile("Images/cherry.jpg") },
        { TileType.Orange, Texture.LoadFromFile("Images/orange.jpg") },
        { TileType.Grape, Texture.LoadFromFile("Images/grape.png") },
        { TileType.Lemon, Texture.LoadFromFile("Images/lemon.jpeg") },
        { TileType.Pineapple, Texture.LoadFromFile("Images/pineapple.jpg") },
        { TileType.Plum, Texture.LoadFromFile("Images/plum.jpg") },
        { TileType.Strawberry, Texture.LoadFromFile("Images/strawberry.jpg") },
        { TileType.Watermelon, Texture.LoadFromFile("Images/waterlemon.png") },
    };
}