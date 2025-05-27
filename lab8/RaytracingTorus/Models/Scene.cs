using System.Numerics;
using RaytracingTorus.Models.Shapes;

namespace RaytracingTorus.Models;

public class Scene
{
    public readonly List<IShape> Shapes = new();
    
    public Vector3 LightPosition { get; set; } = new(0, 2, 5);
    public Vector3 LightColor { get; set; } = new(1, 1, 1);

    public Scene()
    {
        float minorRadius = 0.2f;

        Shapes.Add(new Torus(new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f), 1.0f, minorRadius));
        Shapes.Add(new Torus(new Vector3(0f, 0.3f, 0f), new Vector3(0f, 1f, 0f), 0.85f, minorRadius));
        Shapes.Add(new Torus(new Vector3(0f, 0.6f, 0f), new Vector3(0f, 0f, 1f), 0.7f, minorRadius));
        Shapes.Add(new Torus(new Vector3(0f, 0.9f, 0f), new Vector3(1f, 1f, 0f), 0.55f, minorRadius));
    }
}