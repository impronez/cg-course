using FivePointedStar.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace FivePointedStar;

public class ViewWindow : GameWindow
{
    private Shader _shader;

    private int _vertexArrayObject;
    private int _vertexBufferObject;

    private Matrix4 _projectionMatrix;
    
    private readonly float[] _vertices =
    [
        -1.0f, -1.0f,
        1.0f, -1.0f,
        1.0f, 1.0f,

        -1.0f, -1.0f,
        1.0f, 1.0f,
        -1.0f, 1.0f
    ];

    public ViewWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0f, 0f, 0f, 0f);

        _shader = new Shader("Shaders/vert.glsl", "Shaders/frag.glsl");

        _vertexArrayObject = GL.GenVertexArray();
        _vertexBufferObject = GL.GenBuffer();

        GL.BindVertexArray(_vertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices,
            BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        UpdateOrthographicMatrix();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        _shader.Use();

        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length);

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