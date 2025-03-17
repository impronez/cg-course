using Cuboctahedron.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Cuboctahedron.Utilities;

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
        GL.BufferData(BufferTarget.ArrayBuffer, 0, IntPtr.Zero, BufferUsageHint.DynamicDraw);
        
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, 0, IntPtr.Zero, BufferUsageHint.DynamicDraw); 

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, RGBVertex.Size * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, RGBVertex.Size * sizeof(float), RGBVertex.ColorIndex * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }

    public void DrawElements(PrimitiveType primitiveType, 
        List<RGBVertex> verticesList, 
        int[] indices, 
        Vector3 modelMatrixPosition,
        int thickness = 1)
    {
        _shader.Use();
        
        var model = Matrix4.CreateTranslation(modelMatrixPosition);
        _shader.SetMatrix4("model", model);

        var verticesArray = verticesList.SelectMany(v => v.ToArray()).ToArray();
        
        UpdateBuffers(verticesArray, indices);
        
        GL.LineWidth(thickness);
        
        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawElements(primitiveType, indices.Length, DrawElementsType.UnsignedInt, 0);
    }
    
    private void UpdateBuffers(float[] vertices, int[] indices)
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices.ToArray(), BufferUsageHint.DynamicDraw);

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