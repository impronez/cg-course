using Cannabis.Shaders;
using OpenTK.Graphics.OpenGL;

namespace Cannabis;

public class CannabisRenderer
{
    private float[] _positions;

    private Shader _shader;
    
    private readonly int _vertexArrayObject;
    private readonly int _vertexBufferObject;
    
    public CannabisRenderer(Shader shader)
    {
        _shader = shader;
        
        _positions = SetupPositions();
        
        _vertexArrayObject = GL.GenVertexArray();
        _vertexBufferObject = GL.GenBuffer();
        
        GL.BindVertexArray(_vertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _positions.Length * sizeof(float), _positions, BufferUsageHint.StaticDraw);
        
        var positionLocation = _shader.GetAttribLocation("component");
        GL.VertexAttribPointer(positionLocation, 1, VertexAttribPointerType.Float, false, sizeof(float), 0);
        GL.EnableVertexAttribArray(positionLocation);
    }

    public void Render()
    {
        _shader.Use();
        
        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawArrays(PrimitiveType.LineStrip, 0, _positions.Length);
    }

    private static float[] SetupPositions()
    {
        const float step = float.Pi / 1000f;

        List<float> positions = new();
        
        for (float i = 0; i < 2 * float.Pi; i += step)
        {
            positions.Add(i);
        }
        
        return positions.ToArray();
    }
}