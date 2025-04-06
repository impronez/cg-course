using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace TextureLabyrinth.Utilities;

public class InputHandler
{
    private readonly Camera _camera;
    private readonly CollisionHandler _collisionHandler;

    public InputHandler(Camera camera, CollisionHandler collisionHandler)
    {
        _camera = camera;
        _collisionHandler = collisionHandler;
    }

    public bool ProcessKeyboard(KeyboardState keyboardState, float deltaTime)
    {
        if (keyboardState.IsKeyDown(Keys.Escape)) return false;
        
        var forward = new Vector3(_camera.Front.X, 0, _camera.Front.Z).Normalized();
        var right = new Vector3(_camera.Right.X, 0, _camera.Right.Z).Normalized();
        //var up = new Vector3(_camera.Up.X, _camera.Up.Y, _camera.Up.Z).Normalized();

        if (keyboardState.IsKeyDown(Keys.W))
        {
            var newPosition = _camera.Position + forward * Camera.Speed * deltaTime;
            if (_collisionHandler.CanMove(newPosition))
                _camera.Position = newPosition;
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            var newPosition = _camera.Position - forward * Camera.Speed * deltaTime;
            if (_collisionHandler.CanMove(newPosition))
                _camera.Position = newPosition;
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            var newPosition = _camera.Position - right * Camera.Speed * deltaTime;
            if (_collisionHandler.CanMove(newPosition))
                _camera.Position = newPosition;
        }
        if (keyboardState.IsKeyDown(Keys.D))
        {
            var newPosition = _camera.Position + right * Camera.Speed * deltaTime;
            if (_collisionHandler.CanMove(newPosition))
                _camera.Position = newPosition;
        }

        /*if (keyboardState.IsKeyDown(Keys.LeftShift))
        {
            _camera.Position += up * Camera.Speed * deltaTime;
        }*/

        return true;
    }

    public void ProcessMouse(MouseState mouseState, ref Vector2 lastPos, ref bool firstMove)
    {
        if (firstMove)
        {
            lastPos = new Vector2(mouseState.X, mouseState.Y);
            firstMove = false;
        }
        else
        {
            var deltaX = mouseState.X - lastPos.X;
            var deltaY = mouseState.Y - lastPos.Y;
            lastPos = new Vector2(mouseState.X, mouseState.Y);

            _camera.Yaw += deltaX * Camera.Sensitivity;
            _camera.Pitch -= deltaY * Camera.Sensitivity;
        }
    }
}