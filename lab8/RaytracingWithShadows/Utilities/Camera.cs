using System.Numerics;

namespace RaytracingWithShadows.Utilities;

public class Camera
{
    public Vector3 Position { get; private set; }
    public Vector3 Target { get; private set; }
    public Vector3 Up { get; private set; } = Vector3.UnitY;

    public float FovDegrees { get; set; } = 60f;
    public float AspectRatio { get; set; } = 1.0f;

    public float Yaw { get; private set; }
    public float Pitch { get; private set; }
    public float Distance { get; private set; }

    public Camera(Vector3 position, Vector3 target, float aspectRatio)
    {
        Position = position;
        Target = target;
        AspectRatio = aspectRatio;

        UpdateYawPitchDistance();
    }

    private void UpdateYawPitchDistance()
    {
        Vector3 direction = Vector3.Normalize(Target - Position);
        Distance = Vector3.Distance(Position, Target);

        Pitch = MathF.Asin(direction.Y) * 180f / MathF.PI;
        Yaw = MathF.Atan2(direction.Z, direction.X) * 180f / MathF.PI;
    }

    public Vector3 Forward => Vector3.Normalize(Target - Position);

    public Vector3 Right => Vector3.Normalize(Vector3.Cross(Forward, Up));

    public Vector3 UpDirection => Vector3.Normalize(Vector3.Cross(Right, Forward));

    public Vector3 GetRayDirection(float ndcX, float ndcY)
    {
        float fovScale = MathF.Tan(FovDegrees * MathF.PI / 360f);

        Vector3 ray =
            Forward +
            Right * ndcX * AspectRatio * fovScale +
            UpDirection * ndcY * fovScale;

        return Vector3.Normalize(ray);
    }
}