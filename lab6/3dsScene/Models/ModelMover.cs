using OpenTK.Mathematics;

namespace _3dsScene.Models;

public class ModelMover
{
    private const float MoveDuration = 1.0f;

    private Model Model { get; }
    
    private readonly Queue<Vector3> _targetPositions = new();

    private Vector3 _startPosition;
    private Vector3 _currentTarget;
    private float _elapsedTime;
    private bool _isMoving;

    public ModelMover(Model model)
    {
        Model = model;
    }
    
    public bool IsCurrentMoveComplete => !_isMoving;

    public void AddTarget(Vector3 target)
    {
        _targetPositions.Enqueue(target);
    }

    public void StartNextMove()
    {
        if (_targetPositions.Count > 0)
        {
            _startPosition = Model.Position;
            _currentTarget = _targetPositions.Dequeue();
            _elapsedTime = 0f;
            _isMoving = true;
        }
    }

    public void Update(float deltaTime)
    {
        if (!_isMoving) return;

        _elapsedTime += deltaTime;
        float t = Math.Clamp(_elapsedTime / MoveDuration, 0f, 1f);
        Model.Position = Vector3.Lerp(_startPosition, _currentTarget, t);

        if (_elapsedTime >= MoveDuration)
        {
            _isMoving = false;
            Model.Position = _currentTarget;
        }
    }
}
