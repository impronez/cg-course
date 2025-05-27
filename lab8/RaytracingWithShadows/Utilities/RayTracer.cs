using System.Numerics;
using RaytracingWithShadows.Models;
using RaytracingWithShadows.Models.Shapes;

namespace RaytracingWithShadows.Utilities;

public class RayTracer
{
    private const float Epsilon = 1e-4f;
    private const float MaxDistance = 100f;
    private const int MaxSteps = 200;
    private static readonly Vector3 BackgroundColor = new(0f, 0f, 0f);

    private readonly Scene _scene;

    public RayTracer(Scene scene)
    {
        _scene = scene;
    }

    public Vector3 TraceRay(Ray ray, Vector3 cameraPosition)
    {
        float distanceTraveled = 0f;

        for (int i = 0; i < MaxSteps; i++)
        {
            Vector3 currentPoint = ray.Origin + ray.Direction * distanceTraveled;

            float minDistance = float.MaxValue;
            IShape? hitShape = null;

            foreach (var shape in _scene.Shapes)
            {
                float dist = shape switch
                {
                    Cube cube => DistanceToCube(currentPoint, cube),
                    _ => float.MaxValue
                };

                if (dist < minDistance)
                {
                    minDistance = dist;
                    hitShape = shape;
                }
            }

            if (minDistance < Epsilon && hitShape != null)
            {
                return GetShapeColor(currentPoint, hitShape, cameraPosition);
            }

            distanceTraveled += minDistance;
            if (distanceTraveled > MaxDistance)
                break;
        }

        return BackgroundColor;
    }

    private bool IsInShadow(Ray ray)
    {
        float totalDistance = 0f;

        for (int i = 0; i < MaxSteps; i++)
        {
            Vector3 point = ray.Origin + ray.Direction * totalDistance;
            float distance = MinDistanceToScene(point);
            if (distance < Epsilon)
            {
                return true;
            }

            totalDistance += distance;
            if (totalDistance >= MaxDistance)
            {
                break;
            }
        }

        return false;
    }

    private float MinDistanceToScene(Vector3 point)
    {
        float minDistance = float.MaxValue;

        foreach (var shape in _scene.Shapes)
        {
            float dist = shape switch
            {
                Cube cube => DistanceToCube(point, cube),
                _ => float.MaxValue
            };

            if (dist < minDistance)
                minDistance = dist;
        }

        return minDistance;
    }

    private Vector3 GetShapeColor(Vector3 currentPoint, IShape shape, Vector3 cameraPosition)
    {
        Vector3 normal = EstimateNormal(currentPoint);

        Vector3 toLight = Vector3.Normalize(_scene.LightPosition - currentPoint);
        Ray shadowRay = new Ray(currentPoint + normal * Epsilon * 2, toLight);

        bool inShadow = IsInShadow(shadowRay);

        Vector3 objectColor = shape.Color;

        float ambient = 0.4f;

        Vector3 color = objectColor * ambient;
        if (!inShadow)
        {
            float diffuse = MathF.Max(0, Vector3.Dot(normal, toLight));
            Vector3 viewDir = Vector3.Normalize(cameraPosition - currentPoint);
            Vector3 halfway = Vector3.Normalize(toLight + viewDir);
            float spec = MathF.Pow(MathF.Max(0, Vector3.Dot(normal, halfway)), 64);

            color += objectColor * diffuse + _scene.LightColor * spec;
        }

        color = Vector3.Clamp(color, Vector3.Zero, Vector3.One);
        return color;
    }

    private Vector3 EstimateNormal(Vector3 point)
    {
        const float delta = 1e-4f;

        float dx = MinDistanceToScene(point + new Vector3(delta, 0, 0)) -
                   MinDistanceToScene(point - new Vector3(delta, 0, 0));
        float dy = MinDistanceToScene(point + new Vector3(0, delta, 0)) -
                   MinDistanceToScene(point - new Vector3(0, delta, 0));
        float dz = MinDistanceToScene(point + new Vector3(0, 0, delta)) -
                   MinDistanceToScene(point - new Vector3(0, 0, delta));

        return Vector3.Normalize(new Vector3(dx, dy, dz));
    }

    private float DistanceToCube(Vector3 worldPoint, Cube cube)
    {
        Vector3 localPoint = Vector3.Transform(worldPoint - cube.Position, cube.InverseTransform);

        Vector3 halfSize = new Vector3(cube.Size * 0.5f);
        Vector3 d = Vector3.Abs(localPoint) - halfSize;

        Vector3 maxD0 = new Vector3(
            MathF.Max(d.X, 0),
            MathF.Max(d.Y, 0),
            MathF.Max(d.Z, 0)
        );

        float lengthMaxD0 = maxD0.Length();
        float maxComponent = MathF.Max(d.X, MathF.Max(d.Y, d.Z));
        float minMaxComponent0 = MathF.Min(maxComponent, 0);

        return lengthMaxD0 + minMaxComponent0;
    }
}