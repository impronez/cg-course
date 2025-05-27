using OpenTK.Mathematics;
using TextureLabyrinth.Models;

namespace TextureLabyrinth.Utilities;

public class CollisionHandler
{
    private const float CollisionSize = 0.05f;

    private readonly Labyrinth _labyrinth;

    public CollisionHandler(Labyrinth labyrinth)
    {
        _labyrinth = labyrinth;
    }

    public bool CanMove(Vector3 newPosition)
    {
        return _labyrinth.Blocks.All(block =>
            !(newPosition.X < block.Position.X + Block.Size + CollisionSize &&
              newPosition.X > block.Position.X - CollisionSize &&
              newPosition.Z < block.Position.Z + Block.Size + CollisionSize &&
              newPosition.Z > block.Position.Z - CollisionSize));
    }
}