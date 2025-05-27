using Cannabis.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Cannabis;

public class ViewWindow : GameWindow
{
    private Shader _shader;
    
    private CannabisRenderer _cannabisRenderer;
    
    private Matrix4 _scaleMatrix;
    private Matrix4 _projectionMatrix;
    
    public ViewWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        
        GL.ClearColor(0, 0, 0, 0);

        _shader = new Shader("Shaders/vert.glsl", "Shaders/frag.glsl");
        
        _cannabisRenderer = new CannabisRenderer(_shader);
        
        _scaleMatrix = Matrix4.CreateScale(0.3f);
        
        UpdateOrthographicMatrix();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        _shader.Use();
        
        _shader.SetMatrix4("model", _scaleMatrix);
        
        _cannabisRenderer.Render();
        
        SwapBuffers();
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
    
    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
        UpdateOrthographicMatrix();
    }
    
    private void UpdateOrthographicMatrix()
    {
        float aspectRatio = (float)Size.X / Size.Y;

        if (aspectRatio > 1)
        {
            _projectionMatrix = Matrix4.CreateOrthographic(aspectRatio * 2.0f, 2.0f, -1.0f, 1.0f);
        }
        else
        {
            _projectionMatrix = Matrix4.CreateOrthographic(2.0f, 2.0f / aspectRatio, -1.0f, 1.0f);
        }

        _shader.SetMatrix4("projection", _projectionMatrix);
    }
}