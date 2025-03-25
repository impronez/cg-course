using Labyrinth.Models;
using OpenTK.Mathematics;

namespace Labyrinth.Utilities;

public class CollisionHandler
{
    private const float BlockCollisionSize = 0.55f;
    private const float BoxCollisionSize = 0.1f;
    
    private readonly Models.Labyrinth _labyrinth;

    public CollisionHandler(Models.Labyrinth labyrinth)
    {
        _labyrinth = labyrinth;
    }

    public bool CanMove(Vector3 newPosition)
    {
        var noBlockCollision = _labyrinth.BlockPositions.All(block => 
            !(Math.Abs(newPosition.X - block.X) < BlockCollisionSize && 
              Math.Abs(newPosition.Z - block.Z) < BlockCollisionSize));

        var noBoxCollision = !(newPosition.X < LabyrinthLayout.MinBoundaryX + BoxCollisionSize || 
                                newPosition.X > LabyrinthLayout.MaxBoundaryX - BoxCollisionSize || 
                                newPosition.Z < LabyrinthLayout.MinBoundaryZ + BoxCollisionSize || 
                                newPosition.Z > LabyrinthLayout.MaxBoundaryZ - BoxCollisionSize);

        return noBlockCollision && noBoxCollision;
    }
}