using Assimp;

namespace _3dsExmaple.Models;

public class Model
{
    public float[] Vertices;
    public uint[] Indices;

    public Model(string path)
    {
        var importer = new AssimpContext();
        var scene = importer.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals);

        var mesh = scene.Meshes[0];

        Vertices = new float[mesh.VertexCount * 3];
        for (int i = 0; i < mesh.VertexCount; i++)
        {
            Vertices[i * 3 + 0] = mesh.Vertices[i].X;
            Vertices[i * 3 + 1] = mesh.Vertices[i].Y;
            Vertices[i * 3 + 2] = mesh.Vertices[i].Z;
        }

        Indices = mesh.Faces.SelectMany(f => f.Indices).Select(i => (uint)i).ToArray();
    }
}