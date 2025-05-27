using System.Numerics;

namespace RaytracingTorus.Models.Shapes;

public struct Torus : IShape
{
    public Torus(Vector3 center, Vector3 color, float R, float r, Matrix4x4? transform = null)
    {
        Position = center;
        Color = color;
        MajorRadius = R;
        MinorRadius = r;
        Transform = transform ?? Matrix4x4.Identity;
        InverseTransform = Matrix4x4.Invert(Transform, out var inv) ? inv : Matrix4x4.Identity;
    }

    public Vector3 Position { get; }
    public Vector3 Color { get; }
    public float MajorRadius { get; }
    public float MinorRadius { get; }
    public Matrix4x4 Transform { get; }
    public Matrix4x4 InverseTransform { get; }
}