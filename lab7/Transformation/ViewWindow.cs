using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Transformation.Models;
using Transformation.Shaders;
using Transformation.Utilities;

namespace Transformation;

public class ViewWindow : GameWindow
{
    private Shader _shader;

    private MorphShape _shape;

    private bool _isPhased = true;
    
    private float _phase = 0.0f;
    private float _phaseSpeed = 0.2f;
    private int _phaseDirection = 1;

    private Matrix4 _modelMatrix;

    private Camera _camera;

    public ViewWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0.9f, 0.9f, 0.9f, 0.9f);
        GL.Enable(EnableCap.DepthTest);
        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

        _shader = new Shader("Shaders/vert.glsl", "Shaders/frag.glsl");

        _camera = new Camera(new Vector3(0f, 0f, 5f), (float)Size.X / Size.Y);

        _shape = new MorphShape(10, 50);

        _modelMatrix = Matrix4.CreateScale(0.8f);
        
        CursorState = CursorState.Grabbed;
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        UpdatePhase((float)args.Time);

        _shader.Use();
        _shader.SetFloat("phase", _phase);
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        _shader.SetMatrix4("model", _modelMatrix);

        _shape.Render(_shader);        

        SwapBuffers();
    }

    private void UpdatePhase(float deltaTime)
    {
        if (!_isPhased)
        {
            return;
        }
        
        _phase += _phaseDirection * _phaseSpeed * deltaTime;

        if (_phase >= 1.0f)
        {
            _phase = 1.0f;
            _phaseDirection = -1;
        }
        else if (_phase <= 0.0f)
        {
            _phase = 0.0f;
            _phaseDirection = 1;
        }
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        if (!IsFocused) return;

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
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

    protected override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.Key == Keys.Space)
        {
            _isPhased = !_isPhased;
        }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, Size.X, Size.Y);
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        _shader.Dispose();
    }
}