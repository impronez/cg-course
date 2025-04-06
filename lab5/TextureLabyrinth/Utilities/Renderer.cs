using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TextureLabyrinth.Shaders;

namespace TextureLabyrinth.Utilities;

public class Renderer
{
    private bool _disposed;
    
    private readonly Shader _shader;

    private readonly int _vertexArrayObject;
    private readonly int _vertexBufferObject;
    private readonly int _elementBufferObject;

    public Renderer(Shader shader)
    {
        _shader = shader;
        
        _vertexArrayObject = GL.GenVertexArray();
        _vertexBufferObject = GL.GenBuffer();
        _elementBufferObject = GL.GenBuffer();

        GL.BindVertexArray(_vertexArrayObject);
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, 0, IntPtr.Zero, BufferUsageHint.StaticDraw);
        
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, 0, IntPtr.Zero, BufferUsageHint.DynamicDraw); 

        var positionLocation = _shader.GetAttribLocation("aPosition");
        GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, RGBAVertex.Size * sizeof(float), 0);
        GL.EnableVertexAttribArray(positionLocation);

        var colorLocation = _shader.GetAttribLocation("aColor");
        GL.VertexAttribPointer(colorLocation, 3, VertexAttribPointerType.Float, false,
            RGBAVertex.Size * sizeof(float), RGBAVertex.ColorOffset * sizeof(float));
        GL.EnableVertexAttribArray(colorLocation);
        
        var normalLocation = _shader.GetAttribLocation("aNormal");
        GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false,
            RGBAVertex.Size * sizeof(float), RGBAVertex.NormalOffset * sizeof(float));
        GL.EnableVertexAttribArray(normalLocation);
    }

    public void DrawElements(PrimitiveType primitiveType, 
        float[] vertices, 
        int[] indices, 
        Vector3 modelMatrixPosition,
        int thickness = 1)
    {
        _shader.Use();
        
        var model = Matrix4.CreateTranslation(modelMatrixPosition);
        _shader.SetMatrix4("model", model);
        
        UpdateBuffers(vertices, indices);
        
        GL.LineWidth(thickness);
        
        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawElements(primitiveType, indices.Length, DrawElementsType.UnsignedInt, 0);
    }
    
    private void UpdateBuffers(float[] vertices, int[] indices)
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.DynamicDraw);
    }
    
    public void Dispose()
    {
        if (_disposed) return;

        GL.DeleteVertexArray(_vertexArrayObject);
        GL.DeleteBuffer(_vertexBufferObject);
        GL.DeleteBuffer(_elementBufferObject);

        _disposed = true;
    }

    ~Renderer()
    {
        Dispose();
    }
}