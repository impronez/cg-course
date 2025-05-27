using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Transformation.Shaders;

namespace Transformation.Models;

public class MorphShape
{
    private float[] _vertices;
    private uint[] _indices;

    private int _vertexArrayObject;
    private int _vertexBufferObject;
    private int _elementBufferObject;
    
    public MorphShape(int uCount, int vCount)
    {
        _vertices = GenerateVertices(uCount, vCount).ToArray();
        _indices = GenerateIndices(uCount, vCount).ToArray();

        SetupConfiguration();
    }

    public void Render(Shader shader)
    {
        shader.Use();
        
        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        GL.BindVertexArray(0);
    }

    private List<float> GenerateVertices(int uCount, int vCount)
    {
        var vertices = new List<float>();
        for (int j = 0; j < vCount; j++)
        {
            for (int i = 0; i < uCount; i++)
            {
                float u = (float)i / (uCount - 1);
                float v = (float)j / (vCount - 1);
                vertices.Add(u);
                vertices.Add(v);
            }
        }
        return vertices;
    }

    private List<uint> GenerateIndices(int uCount, int vCount)
    {
        var indices = new List<uint>();

        for (int j = 0; j < vCount - 1; j++)
        {
            for (int i = 0; i < uCount - 1; i++)
            {
                uint topLeft = (uint)(j * uCount + i);
                uint topRight = topLeft + 1;
                uint bottomLeft = topLeft + (uint)uCount;
                uint bottomRight = bottomLeft + 1;

                // Первый треугольник
                indices.Add(topLeft);
                indices.Add(bottomLeft);
                indices.Add(topRight);

                // Второй треугольник
                indices.Add(topRight);
                indices.Add(bottomLeft);
                indices.Add(bottomRight);
            }
        }

        return indices;
    }

    private void SetupConfiguration()
    {
        _vertexArrayObject = GL.GenVertexArray();
        _vertexBufferObject = GL.GenBuffer();
        _elementBufferObject = GL.GenBuffer();

        GL.BindVertexArray(_vertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

        GL.BindVertexArray(0);
    }

}