using System.Numerics;

namespace RaytracingTorus.Models.Shapes;

public interface IShape
{
    Vector3 Position { get; }
    Vector3 Color { get; }
}