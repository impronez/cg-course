using OpenTK.Mathematics;
using TextureLabyrinth.Textures;
using TextureLabyrinth.Utilities;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace TextureLabyrinth.Models;

public class Labyrinth
{
    public readonly (Vector3 Position, BlockType Type)[] Blocks;
    private readonly Vector3[] _emptyPositions;

    public Labyrinth()
    {
        Blocks = LabyrinthMap.GetBlocksSorteddByBlockType();
        _emptyPositions = LabyrinthMap.GetEmptyPositions();
    }

    public void Draw(Renderer renderer)
    {
        DrawBlocks(renderer);

        DrawFloor(renderer);

        DrawCeiling(renderer);
    }

    private void DrawBlocks(Renderer renderer)
    {
        BlockType? previousBlockType = null;
    
        foreach (var block in Blocks)
        {
            if (previousBlockType != block.Type)
            {
                BlockTypeToTextureConverter.GetTexture(block.Type).Use();
                previousBlockType = block.Type;
            }
        
            renderer.DrawElements(
                PrimitiveType.Quads,
                Block.SidesVertices,
                Block.SideIndices,
                block.Position
            );
        }
    }

    private void DrawFloor(Renderer renderer)
    {
        TextureManager.FloorTexture.Use();
        foreach (var position in _emptyPositions)
        {
            renderer.DrawElements(PrimitiveType.Quads, Block.BottomSideVertices, Block.InnerBottomSideIndices, position);
        }
    }

    private void DrawCeiling(Renderer renderer)
    {
        TextureManager.CeilingTexture.Use();
        foreach (var position in _emptyPositions)
        {
            renderer.DrawElements(PrimitiveType.Quads, Block.UpSideVertices, Block.InnerUpSideIndices, position);
        }
    }
}