using Cube2.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Cube2;

public class CubeRenderer
{
    private readonly float[] _vertices =
    [
        // positions        // colors
        -0.8f, -0.8f,  -0.8f,  1.0f, 0.0f, 0.0f, // 0
        0.8f, -0.8f,  -0.8f,  0.0f, 1.0f, 0.0f, // 1
        0.8f, 0.8f,  -0.8f,  0.0f, 0.0f, 1.0f, // 2
        -0.8f, 0.8f,  -0.8f,  0.0f, 0.0f, 1.0f, // 3
        -0.8f, -0.8f,  0.8f,  0.0f, 0.0f, 1.0f, // 4
        0.8f, -0.8f,  0.8f,  0.0f, 0.0f, 1.0f, // 5
        0.8f, 0.8f,  0.8f,  0.0f, 0.0f, 1.0f, // 6
        -0.8f, 0.8f,  0.8f,  0.0f, 0.0f, 1.0f // 7
    ];
    
    private readonly int[] _indices =
    [
        0, 7, 3,
        0, 4, 7,

        1, 2, 6,
        6, 5, 1,

        0, 2, 1,
        0, 3, 2,

        4, 5, 6,
        6, 7, 4,

        2, 3, 6,
        6, 3, 7,

        0, 1, 5,
        0, 5, 4
    ];
    
    private readonly int _vertexArrayObject;

    public CubeRenderer()
    {
        var vertexBufferObject = GL.GenBuffer();
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(int), _vertices, BufferUsageHint.StaticDraw);
        
        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);
        
        var elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }
    
    public void Draw(Shader shader, Vector3 position)
    {
        shader.Use();
        
        var model = Matrix4.CreateTranslation(position);
        shader.SetMatrix4("model", model);

        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}