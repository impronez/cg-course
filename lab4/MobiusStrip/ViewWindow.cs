using MobiusStrip.Shaders;
using MobiusStrip.Utilities;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MobiusStrip;

public class ViewWindow : GameWindow
{
    private Shader _shader;

    private Renderer _renderer;
    
    private Camera _camera;
    
    private MobiusStrip _mobiusStrip;
    
    public ViewWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
        : base(gameWindowSettings, nativeWindowSettings)
    {}

    protected override void OnLoad()
    {
        base.OnLoad();
        
        GL.ClearColor(0.9f, 0.9f, 0.9f, 1f);
        
        GL.Enable(EnableCap.DepthTest);
        
        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
        _shader.Use();

        _mobiusStrip = new MobiusStrip();

        _camera = new Camera(new Vector3(0f, 0f, 10f));
        
        _renderer = new Renderer(_shader);

        CursorState = CursorState.Grabbed;
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        _shader.Use();
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        
        _mobiusStrip.Draw(_renderer, Vector3.Zero);
        
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

    protected override void OnUnload()
    {
        base.OnUnload();
        
        _renderer.Dispose();
    }
}