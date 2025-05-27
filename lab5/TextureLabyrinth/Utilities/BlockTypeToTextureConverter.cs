using TextureLabyrinth.Models;
using TextureLabyrinth.Textures;

namespace TextureLabyrinth.Utilities;

public static class BlockTypeToTextureConverter
{
    public static Texture GetTexture(BlockType blockType)
    {
        return blockType switch
        {
            BlockType.None => throw new ArgumentNullException(nameof(blockType)),
            BlockType.Wall1 => TextureManager.WallTextures[0],
            BlockType.Wall2 => TextureManager.WallTextures[1],
            BlockType.Wall3 => TextureManager.WallTextures[2],
            BlockType.Wall4 => TextureManager.WallTextures[3],
            BlockType.Wall5 => TextureManager.WallTextures[4],
            BlockType.Wall6 => TextureManager.WallTextures[5],
            _ => throw new ArgumentOutOfRangeException(nameof(blockType), blockType, null)
        };
    }
}