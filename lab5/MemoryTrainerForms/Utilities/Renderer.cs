using MemoryTrainer.Textures;
using MemoryTrainer.ViewModels;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace MemoryTrainer.Utilities;

public class Renderer(GameViewModel gameViewModel)
{
    public const float TileWidth = 0.8f;
    public const float TileHeight = 0.8f;
    public const float TileDepth = 0.05f;
    private static readonly Color4 TileColor = new(0.6f, 0.8f, 1f, 1f);

    public void Draw()
    {
        GL.PushMatrix();
        
        foreach (var tile in gameViewModel.TileViewModels)
        {
            if (!tile.IsSelected && tile.IsGuessed) continue;
            
            DrawTile(tile);
        }

        GL.PopMatrix();
    }

    private void DrawTile(TileViewModel tile)
    {
        GL.PushMatrix();

        GL.Translate(tile.Position);
        GL.Rotate(tile.FlipAngle, 0f, 1f, 0f);
        
        var minX = -TileWidth / 2f;
        var maxX = TileWidth / 2f;
        var minY = -TileHeight / 2f;
        var maxY = TileHeight / 2f;
        var minZ = -TileDepth / 2f;
        var maxZ = TileDepth / 2f;

        DrawColorFaces(minX, maxX, minY, maxY, minZ, maxZ);

        DrawTexturedFace(tile, minX, maxX, minY, maxY, maxZ);

        GL.PopMatrix();
    }

    private void DrawTexturedFace(TileViewModel tile, float minX, float maxX, float minY, float maxY, float maxZ)
    {
        GL.Enable(EnableCap.Texture2D);
        
        TextureManager.TileTypeToTexture[tile.Type].Use();

        GL.Begin(PrimitiveType.Quads);
        GL.Color3(1.0f, 1.0f, 1.0f);

        GL.Normal3(0.0f, 0.0f, 1.0f);

        GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(minX, minY, maxZ);
        GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(maxX, minY, maxZ);
        GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(maxX, maxY, maxZ);
        GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(minX, maxY, maxZ);
        GL.End();
        
        GL.Disable(EnableCap.Texture2D);
    }

    private void DrawColorFaces(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        GL.Begin(PrimitiveType.Quads);

        GL.Color4(TileColor);

        // Задняя грань
        GL.Normal3(0.0f, 0.0f, -1.0f);
        GL.Vertex3(maxX, minY, minZ);
        GL.Vertex3(minX, minY, minZ);
        GL.Vertex3(minX, maxY, minZ);
        GL.Vertex3(maxX, maxY, minZ);

        // Левая грань
        GL.Normal3(-1.0f, 0.0f, 0.0f);
        GL.Vertex3(minX, minY, minZ);
        GL.Vertex3(minX, minY, maxZ);
        GL.Vertex3(minX, maxY, maxZ);
        GL.Vertex3(minX, maxY, minZ);

        // Правая грань
        GL.Normal3(1.0f, 0.0f, 0.0f);
        GL.Vertex3(maxX, minY, maxZ);
        GL.Vertex3(maxX, minY, minZ);
        GL.Vertex3(maxX, maxY, minZ);
        GL.Vertex3(maxX, maxY, maxZ);

        // Верхняя грань
        GL.Normal3(0.0f, 1.0f, 0.0f);
        GL.Vertex3(minX, maxY, maxZ);
        GL.Vertex3(maxX, maxY, maxZ);
        GL.Vertex3(maxX, maxY, minZ);
        GL.Vertex3(minX, maxY, minZ);

        // Нижняя грань
        GL.Normal3(0.0f, -1.0f, 0.0f);
        GL.Vertex3(minX, minY, minZ);
        GL.Vertex3(maxX, minY, minZ);
        GL.Vertex3(maxX, minY, maxZ);
        GL.Vertex3(minX, minY, maxZ);

        GL.End();
    }
}