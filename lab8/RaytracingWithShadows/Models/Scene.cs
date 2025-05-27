using System.Numerics;
using RaytracingWithShadows.Models.Shapes;

namespace RaytracingWithShadows.Models;

public class Scene
{
    public readonly List<IShape> Shapes = new();
    
    public Vector3 LightPosition { get; set; } = new(0, 0, 5);
    public Vector3 LightColor { get; set; } = new(1, 1, 1);

    public Scene()
    {
        Matrix4x4 rotation = Matrix4x4.CreateRotationX(MathF.PI / 4);
        Shapes.Add(new Cube(new Vector3(0f, 0f, 0f), new Vector3(0.3f, 0.3f, 0.8f), 1f));
        Shapes.Add(new Cube(new Vector3(0f, 0f, -2f), new Vector3(0.5f, 0.3f, 0.8f), 2f, rotation));
    }
}