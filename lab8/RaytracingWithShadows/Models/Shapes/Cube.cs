using System.Numerics;

namespace RaytracingWithShadows.Models.Shapes;

public struct Cube : IShape
{
    public Cube(Vector3 position, Vector3 color, float size, Matrix4x4? transform = null)
    {
        Position = position;
        Color = color;
        Size = size;

        Transform = transform ?? Matrix4x4.Identity;
        Matrix4x4.Invert(Transform, out var inverse);
        InverseTransform = inverse;
    }

    public float Size { get; }
    public Vector3 Position { get; }
    public Vector3 Color { get; }

    public Matrix4x4 Transform { get; }
    public Matrix4x4 InverseTransform { get; }
}