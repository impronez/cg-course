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
        var noBlockCollision = _labyrinth.BlockPositions.All(block =>
            !(newPosition.X < block.X + Block.Size + CollisionSize &&
              newPosition.X > block.X - CollisionSize &&
              newPosition.Z < block.Z + Block.Size + CollisionSize &&
              newPosition.Z > block.Z - CollisionSize));

        var noBoxCollision = !(newPosition.X < LabyrinthMap.MinBoundaryX + CollisionSize ||
                               newPosition.X > LabyrinthMap.MaxBoundaryX - CollisionSize ||
                               newPosition.Z < LabyrinthMap.MinBoundaryZ + CollisionSize ||
                               newPosition.Z > LabyrinthMap.MaxBoundaryZ - CollisionSize);

        return noBlockCollision && noBoxCollision;
    }
}