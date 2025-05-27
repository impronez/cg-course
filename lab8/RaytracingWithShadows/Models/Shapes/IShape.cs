using System.Numerics;

namespace RaytracingWithShadows.Models.Shapes;

public interface IShape
{
    Vector3 Position { get; }
    Vector3 Color { get; }
}