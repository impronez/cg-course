using OpenTK.Graphics.OpenGL4;
using TextureLabyrinth.Shaders;

namespace TextureLabyrinth.Textures;

public static class TextureManager
{
    public static readonly Texture[] WallTextures =
    [
        Texture.LoadFromFile("Images/wall1.jpg"),
        Texture.LoadFromFile("Images/wall2.jpg"),
        Texture.LoadFromFile("Images/wall3.jpg"),
        Texture.LoadFromFile("Images/wall4.jpg"),
        Texture.LoadFromFile("Images/wall5.jpg"),
        Texture.LoadFromFile("Images/wall6.jpg")
    ];
    
    public static readonly Texture FloorTexture = Texture.LoadFromFile("Images/floor.jpg");
    public static readonly Texture CeilingTexture = Texture.LoadFromFile("Images/up.jpg");
}