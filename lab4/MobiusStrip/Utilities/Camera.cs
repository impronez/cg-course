using OpenTK.Mathematics;

namespace MobiusStrip.Utilities;

public class Camera
{
    public float X { get; private set; }
    public float Y { get; private set; }
    
    public Vector3 Position { get; set; }
    
    private Vector2 _lastPosition;
    
    private const float Sensitivity = 0.1f;

    private bool _isAllowedRotate;

    public Camera(Vector3 position)
    {
        _lastPosition = new Vector2(position.X, position.Y);
        Position = position;
    }

    public void Rotate(Vector2 newPosition)
    {
        if (_isAllowedRotate)
        {
            float deltaX = newPosition.X - _lastPosition.X;
            float deltaY = newPosition.Y - _lastPosition.Y;

            Y += deltaX * Sensitivity;
            X -= deltaY * Sensitivity;

            X = MathHelper.Clamp(X, -89f, 89f);
        }

        _lastPosition = newPosition;
    }

    public void AllowRotate(bool value)
    {
        _isAllowedRotate = value;
    }

    public Matrix4 GetViewMatrix()
    {
        var rotationMatrixX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(X));
        var rotationMatrixY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Y));

        var viewMatrix = rotationMatrixY * rotationMatrixX * Matrix4.LookAt(Position, Vector3.Zero, Vector3.UnitY);

        return viewMatrix;
    }

    public Matrix4 GetProjectionMatrix()
    {
        return Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1f, 0.1f, 100f);
    }
}