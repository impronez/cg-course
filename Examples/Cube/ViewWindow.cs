using Cube2.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Cube2;

public class ViewWindow : GameWindow
{
    private Shader _shader;
    
    private CubeRenderer _cubeRenderer;

    private Camera _camera;
    
    private bool _firstMove = true;

    private Vector2 _lastPos;

    public ViewWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {}

    protected override void OnLoad()
    {
        base.OnLoad();
        
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        
        GL.Enable(EnableCap.DepthTest);
        
        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
        _shader.Use();

        _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

        CursorState = CursorState.Grabbed;
        
        _cubeRenderer = new CubeRenderer();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        _shader.Use();
        
        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        
        _cubeRenderer.Draw(_shader, new Vector3(0.0f, 0.0f, 0.0f));        
        
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        if (!IsFocused) return;

        var input = KeyboardState;

        if (input.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        if (input.IsKeyDown(Keys.W))
        {
            _camera.Position += _camera.Front * Camera.Speed * (float)e.Time;
        }
        
        if (input.IsKeyDown(Keys.S))
        {
            _camera.Position -= _camera.Front * Camera.Speed * (float)e.Time; // Backwards
        }
        if (input.IsKeyDown(Keys.A))
        {
            _camera.Position -= _camera.Right * Camera.Speed * (float)e.Time; // Left
        }
        if (input.IsKeyDown(Keys.D))
        {
            _camera.Position += _camera.Right * Camera.Speed * (float)e.Time; // Right
        }
        if (input.IsKeyDown(Keys.Space))
        {
            _camera.Position += _camera.Up * Camera.Speed * (float)e.Time; // Up
        }
        if (input.IsKeyDown(Keys.LeftShift))
        {
            _camera.Position -= _camera.Up * Camera.Speed * (float)e.Time; // Down
        }

        var mouse = MouseState;

        if (_firstMove) // This bool variable is initially set to true.
        {
            _lastPos = new Vector2(mouse.X, mouse.Y);
            _firstMove = false;
        }
        else
        {
            var deltaX = mouse.X - _lastPos.X;
            var deltaY = mouse.Y - _lastPos.Y;
            _lastPos = new Vector2(mouse.X, mouse.Y);

            _camera.Yaw += deltaX * Camera.Sensitivity;
            _camera.Pitch -= deltaY * Camera.Sensitivity;
        }
    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        base.OnMouseWheel(e);

        _camera.Fov -= e.OffsetY;
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, Size.X, Size.Y);
        // We need to update the aspect ratio once the window has been resized.
        _camera.AspectRatio = Size.X / (float)Size.Y;
    }
}