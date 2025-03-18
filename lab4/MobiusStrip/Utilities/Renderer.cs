using MobiusStrip.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace MobiusStrip.Utilities;

public class Renderer
{
    private bool _disposed;
    
    private readonly Shader _shader;

    private readonly int _vertexArrayObject;
    private readonly int _vertexBufferObject;

    public Renderer(Shader shader)
    {
        _shader = shader;
        
        _vertexArrayObject = GL.GenVertexArray();
        _vertexBufferObject = GL.GenBuffer();

        GL.BindVertexArray(_vertexArrayObject);
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, 0, IntPtr.Zero, BufferUsageHint.DynamicDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, RGBAVertex.Size * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, RGBAVertex.Size * sizeof(float), RGBAVertex.ColorIndex * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }

    public void DrawElements(PrimitiveType primitiveType, 
        float[] vertices, 
        Vector3 modelMatrixPosition)
    {
        _shader.Use();
        
        var model = Matrix4.CreateTranslation(modelMatrixPosition);
        _shader.SetMatrix4("model", model);
        
        UpdateBuffers(vertices);
        
        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawArrays(primitiveType, 0, vertices.Length);
    }
    
    private void UpdateBuffers(float[] vertices)
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
    }
    
    public void Dispose()
    {
        if (_disposed) return;

        GL.DeleteVertexArray(_vertexArrayObject);
        GL.DeleteBuffer(_vertexBufferObject);

        _disposed = true;
    }

    ~Renderer()
    {
        Dispose();
    }
}