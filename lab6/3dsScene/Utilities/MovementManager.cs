using _3dsScene.Models;
using OpenTK.Mathematics;

namespace _3dsScene.Utilities;

public class MovementManager
{
    private readonly Dictionary<Model, ModelMover> _modelMovers = new();
    private readonly Queue<Model> _moveOrder = new();

    private ModelMover? _currentMover;

    public bool IsMoving => _currentMover != null;

    public void AddMove(Model model, Vector3 targetPosition)
    {
        if (!_modelMovers.TryGetValue(model, out var mover))
        {
            mover = new ModelMover(model);
            _modelMovers[model] = mover;
        }

        mover.AddTarget(targetPosition);
        _moveOrder.Enqueue(model);
    }

    public void Move()
    {
        if (_currentMover == null && _moveOrder.Count > 0)
        {
            var nextModel = _moveOrder.Dequeue();
            _currentMover = _modelMovers[nextModel];
            _currentMover.StartNextMove();
        }
    }

    public void Update(float deltaTime)
    {
        if (_currentMover != null)
        {
            _currentMover.Update(deltaTime);

            if (_currentMover.IsCurrentMoveComplete)
            {
                _currentMover = null;
            }
        }
    }
}
