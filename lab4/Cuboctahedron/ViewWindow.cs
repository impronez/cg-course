using Cuboctahedron.Shaders;
using Cuboctahedron.Utilities;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Cuboctahedron;

public class ViewWindow : GameWindow
{
    private Shader _shader;
    
    private Cuboctahedron _cuboctahedron;

    private Camera _camera;
    
    private bool _firstMove = true;

    private Vector2 _lastPos;

    private Renderer _renderer;

    public ViewWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {}

    protected override void OnLoad()
    {
        base.OnLoad();
        
        GL.ClearColor(0.9f, 0.9f, 0.9f, 1.0f);
        
        GL.Enable(EnableCap.DepthTest);
        
        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
        _shader.Use();

        _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

        CursorState = CursorState.Grabbed;

        _renderer = new Renderer(_shader);
        
        _cuboctahedron = new Cuboctahedron();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        _shader.Use();
        
        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        
        _cuboctahedron.Draw(_renderer, new Vector3(0.0f, 0.0f, 0.0f));        
        
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        if (!IsFocused) return;

        ProcessKeyboard(e.Time);

        ProcessMouseMovement();
    }

    private void ProcessMouseMovement()
    {
        if (_firstMove)
        {
            _lastPos = new Vector2(MouseState.X, MouseState.Y);
            _firstMove = false;
        }
        else
        {
            var deltaX = MouseState.X - _lastPos.X;
            var deltaY = MouseState.Y - _lastPos.Y;
            _lastPos = new Vector2(MouseState.X, MouseState.Y);

            _camera.Yaw += deltaX * Camera.Sensitivity;
            _camera.Pitch -= deltaY * Camera.Sensitivity;
        }
    }

    private void ProcessKeyboard(double time)
    {
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        if (KeyboardState.IsKeyDown(Keys.W))
        {
            _camera.Position += _camera.Front * Camera.Speed * (float)time;
        }
        
        if (KeyboardState.IsKeyDown(Keys.S))
        {
            _camera.Position -= _camera.Front * Camera.Speed * (float)time;
        }
        if (KeyboardState.IsKeyDown(Keys.A))
        {
            _camera.Position -= _camera.Right * Camera.Speed * (float)time;
        }
        if (KeyboardState.IsKeyDown(Keys.D))
        {
            _camera.Position += _camera.Right * Camera.Speed * (float)time;
        }
        if (KeyboardState.IsKeyDown(Keys.Space))
        {
            _camera.Position += _camera.Up * Camera.Speed * (float)time;
        }
        if (KeyboardState.IsKeyDown(Keys.LeftShift))
        {
            _camera.Position -= _camera.Up * Camera.Speed * (float)time;
        }
    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        base.OnMouseWheel(e);

        _camera.FieldOfView -= e.OffsetY;
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, Size.X, Size.Y);
        
        _camera.AspectRatio = Size.X / (float)Size.Y;
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        
        _renderer.Dispose();
    }
}