using _3dsScene.Models;
using _3dsScene.Shaders;
using _3dsScene.Utilities;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace _3dsScene;

public class ViewWindow : GameWindow
{
    private Shader _shader;
    private readonly Vector3 _initCameraPosition = new(0f, -18f, 12f);
    private readonly Vector3 _lightPos = new(10, 0f, 10f);

    private Camera _camera;

    private Scene _scene;

    public ViewWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0.92f, 0.9f, 0.93f, 1f);

        GL.Enable(EnableCap.DepthTest);

        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

        _camera = new Camera(_initCameraPosition, (float)Size.X / Size.Y);

        _scene = new Scene();

        CursorState = CursorState.Grabbed;
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        _shader.Dispose();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        _shader.Use();
        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        _shader.SetMatrix4("model", Matrix4.Identity);
        _shader.SetFloat("ambientStrength", 0.3f);

        _shader.SetVector3("lightPos", _lightPos);

        _scene.Render(_shader, (float)e.Time);

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        if (!IsFocused) return;

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        if (KeyboardState.IsKeyDown(Keys.Enter))
        {
            _scene.Move();
        }

        _camera.Rotate(MousePosition);
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        base.OnMouseDown(e);

        if (e.Button == MouseButton.Left)
        {
            _camera.AllowRotate(true);
        }
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        base.OnMouseUp(e);

        if (e.Button == MouseButton.Left)
        {
            _camera.AllowRotate(false);
        }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);

        _camera.AspectRatio = ((float)e.Width / e.Height);
    }
}