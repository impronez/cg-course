using _3dsScene.Shaders;
using _3dsScene.Utilities;
using OpenTK.Mathematics;

namespace _3dsScene.Models;

public class Scene
{
    private readonly MovementManager _movementManager = new();
    // добавить падение короля при проигрыше
    private static readonly Model ChessBoard = new(new Vector3(0f, 0f, 0f), 4f, "../../../Resources/tabuleiroUV1.3ds");

    private static readonly Model WhiteKing = new(new Vector3(-2.4f, -4.0f, 0.2f), 0.09f, "../../../Resources/King.3ds");
    private static readonly Model WhiteQueen = new(new Vector3(-0.8f, -2.4f, 0.2f), 0.09f, "../../../Resources/Queen.3ds");
    private static readonly Model WhiteRook = new(new Vector3(-4.0f, -2.4f, 0.2f), 0.09f, "../../../Resources/Rook.3ds");

    private static readonly Model BlackKing = new(new Vector3(0.8f, 2.4f, 0.2f), 0.09f, "../../../Resources/King.3ds", true);
    private static readonly Model BlackPawn1 = new(new Vector3(0.8f, -2.4f, 0.2f), 0.09f, "../../../Resources/Pawn.3ds", true);
    private static readonly Model BlackPawn2 = new(new Vector3(2.4f, -0.8f, 0.2f), 0.09f, "../../../Resources/Pawn.3ds", true);

    private static readonly Model[] Models = [ChessBoard, WhiteKing, WhiteQueen, WhiteRook, BlackKing, BlackPawn1, BlackPawn2];

    public Scene()
    {
        AddMotions();
    }

    public void Render(Shader shader, float deltaTime)
    {
        _movementManager.Update(deltaTime);

        foreach (var model in Models)
        {
            model.Render(shader);
        }
    }

    public void Move()
    {
        if (!_movementManager.IsMoving)
        {
            _movementManager.Move();
        }
    }

    private void AddMotions()
    {
        _movementManager.AddMove(WhiteQueen, new Vector3(-0.8f, 0.8f, 0.2f));
        _movementManager.AddMove(BlackKing, new Vector3(2.4f, 2.4f, 0.2f));
        _movementManager.AddMove(WhiteRook, new Vector3(-4.0f, 2.4f, 0.2f));
        _movementManager.AddMove(BlackKing, new Vector3(4.0f, 4.0f, 0.2f));
        _movementManager.AddMove(WhiteQueen, new Vector3(4.0f, 0.8f, 0.2f));
        _movementManager.AddMove(BlackKing, new Vector3(2.4f, 5.6f, 0.2f));
        _movementManager.AddMove(WhiteRook, new Vector3(-4.0f, 4.0f, 0.2f));
        _movementManager.AddMove(BlackPawn1, new Vector3(0.8f, -4.0f, 0.2f));
        _movementManager.AddMove(WhiteQueen, new Vector3(4.0f, 4.0f, 0.2f));
        _movementManager.AddMove(BlackKing, new Vector3(0.8f, 5.6f, 0.2f));
        _movementManager.AddMove(WhiteQueen, new Vector3(4.0f, 5.6f, 0.2f));
    }
}