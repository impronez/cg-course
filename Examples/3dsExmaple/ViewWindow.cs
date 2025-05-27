using _3dsExmaple.Models;
using _3dsExmaple.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace _3dsExmaple;

public class ViewWindow : GameWindow
{
    private Shader _shader;
    private Model _model;

    private int _vao, _vbo, _ebo;
    private Camera _camera;
    private readonly Vector3 _initCameraPosition = new(0f, 0f, 100f);

    private bool _firstMouseMove = true;

    public ViewWindow(NativeWindowSettings settings)
        : base(GameWindowSettings.Default, settings)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.9f, 0.9f, 0.9f, 1.0f);
        GL.Enable(EnableCap.DepthTest);

        _shader = new Shader("Shaders/vertex.glsl", "Shaders/fragment.glsl");
        _model = new Model("model1.3ds");

        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();
        _ebo = GL.GenBuffer();

        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, _model.Vertices.Length * sizeof(float), _model.Vertices,
            BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _model.Indices.Length * sizeof(uint), _model.Indices,
            BufferUsageHint.StaticDraw);
        _camera = new Camera(_initCameraPosition, (float)Size.X / Size.Y);

        _shader.Use();
        
        CursorState = CursorState.Grabbed;
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        _shader.Use();

        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        _shader.SetMatrix4("model", Matrix4.Identity);

        GL.BindVertexArray(_vao);
        GL.DrawElements(PrimitiveType.Triangles, _model.Indices.Length, DrawElementsType.UnsignedInt, 0);

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

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        
        GL.Viewport(0, 0, e.Width, e.Height);
        
        _camera.AspectRatio = ((float)e.Width / e.Height);
    }
    
    protected override void OnUnload()
    {
        base.OnUnload();
        GL.DeleteBuffer(_vbo);
        GL.DeleteBuffer(_ebo);
        GL.DeleteVertexArray(_vao);
        _shader.Dispose();
    }
}