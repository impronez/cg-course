using _3dsScene.Shaders;
using _3dsScene.Utilities;
using Assimp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using PrimitiveType = OpenTK.Graphics.OpenGL.PrimitiveType;

namespace _3dsScene.Models;

public class Model
{
    private Assimp.Scene _scene;
    private Dictionary<Mesh, MeshData> _meshBuffer = new();

    private bool _isInverseColor;
    public Model(Vector3 position, float scale, string filePath, bool isInverseColor = false)
    {
        Position = position;
        Scale = scale;
        
        _isInverseColor = isInverseColor;

        Initialize(filePath);
    }
    
    public Vector3 Position { get; set; }
    public float Scale { get; set; }

    public void Render(Shader shader)
    {
        shader.SetMatrix4("model", GetModelMatrix());
        
        foreach (var mesh in _scene.Meshes)
        {
            MeshData meshData = _meshBuffer[mesh];

            GL.BindBuffer(BufferTarget.ArrayBuffer, meshData.Vbo);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, meshData.Ebo);

            SetVertexAttributes();

            SetColor(mesh, shader);

            GL.DrawElements(PrimitiveType.Triangles, mesh.GetIndices().Length, DrawElementsType.UnsignedInt,
                IntPtr.Zero);
        }
    }

    private Matrix4 GetModelMatrix()
    {
        return Matrix4.CreateScale(Scale) * Matrix4.CreateTranslation(Position);
    }

    private void SetColor(Mesh mesh, Shader shader)
    {
        if (_scene.Materials[mesh.MaterialIndex].HasColorAmbient)
        {
            Vector3 colorAmbient = !_isInverseColor
                ? new Vector3(
                _scene.Materials[mesh.MaterialIndex].ColorAmbient.R,
                _scene.Materials[mesh.MaterialIndex].ColorAmbient.G,
                _scene.Materials[mesh.MaterialIndex].ColorAmbient.B)
                : new Vector3(
                    1 - _scene.Materials[mesh.MaterialIndex].ColorAmbient.R,
                    1 - _scene.Materials[mesh.MaterialIndex].ColorAmbient.G,
                    1 - _scene.Materials[mesh.MaterialIndex].ColorAmbient.B);
            
            shader.SetVector3("ambientColor", colorAmbient);
        }

        if (_scene.Materials[mesh.MaterialIndex].HasColorDiffuse)
        {
            Vector3 colorDiffuse = !_isInverseColor
                ? new Vector3(
                    _scene.Materials[mesh.MaterialIndex].ColorDiffuse.R,
                    _scene.Materials[mesh.MaterialIndex].ColorDiffuse.G,
                    _scene.Materials[mesh.MaterialIndex].ColorDiffuse.B)
                : new Vector3(
                    1 - _scene.Materials[mesh.MaterialIndex].ColorDiffuse.R,
                    1 - _scene.Materials[mesh.MaterialIndex].ColorDiffuse.G,
                    1 - _scene.Materials[mesh.MaterialIndex].ColorDiffuse.B);

            shader.SetVector3("diffuseColor", colorDiffuse);
        }
    }

    private static void SetVertexAttributes()
    {
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Size * sizeof(float),
            Vertex.PositionOffset);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vertex.Size * sizeof(float),
            Vertex.NormalOffset * sizeof(float));
        GL.EnableVertexAttribArray(1);

        GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Vertex.Size * sizeof(float),
            Vertex.ColorOffset * sizeof(float));
        GL.EnableVertexAttribArray(2);
    }

    private void Initialize(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        AssimpContext context = new();
        _scene = context.ImportFile(filePath,
            PostProcessSteps.Triangulate |
            PostProcessSteps.GenerateNormals |
            PostProcessSteps.CalculateTangentSpace);

        foreach (var mesh in _scene.Meshes)
        {
            ConfigurateMeshData(mesh);
        }
    }

    private void ConfigurateMeshData(Mesh mesh)
    {
        Vector3[] vertices = mesh.Vertices
            .Select(v => new Vector3(v.X, v.Y, v.Z))
            .ToArray();

        Vector3[] normals = mesh.Normals
            .Select(v => new Vector3(v.X, v.Y, v.Z))
            .ToArray();

        int[] indices = mesh.GetIndices();

        List<float> vertexData = new();

        foreach (int index in indices)
        {
            vertexData.AddRange(GetVertexData(vertices[index],
                normals[index],
                _scene.Materials[mesh.MaterialIndex].ColorDiffuse));
        }

        int vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexData.Count * sizeof(float), vertexData.ToArray(),
            BufferUsageHint.StaticDraw);

        int ebo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices,
            BufferUsageHint.StaticDraw);

        _meshBuffer[mesh] = new MeshData(vbo, ebo);
    }

    private float[] GetVertexData(Vector3 position, Vector3 normal, Color4D inColor)
    {
        Color4 color = new(inColor.R,
            inColor.G,
            inColor.B,
            inColor.A);

        Vertex vertex = new(position, normal, color);

        return vertex.ToFloatArray();
    }
}