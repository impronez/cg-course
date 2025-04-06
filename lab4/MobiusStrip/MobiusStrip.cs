using MobiusStrip.Utilities;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace MobiusStrip;

public class MobiusStrip
{
    private static readonly Color4 Color = Color4.Cyan; 
    
    private const float MinU = 0f;
    private const float MaxU = MathHelper.TwoPi;
    
    private const float MinV = -1f;
    private const float MaxV = 1f;

    private const int SegmentsU = 100;

    private float[] _vertices;
    private List<RGBAVertex> _verticesList;
    
    public MobiusStrip()
    {
        InitializeVertices();
    }
    public void Draw(Renderer renderer, Vector3 position)
    {
        renderer.DrawElements(PrimitiveType.QuadStrip, _vertices, position);
    }

    private void InitializeVertices()
    {
        _verticesList = [];

        var u0 = MathHelper.Lerp(MinU, MaxU, 0f);
        _verticesList.Add(GetRGBAVertex(u0, MinV));
        _verticesList.Add(GetRGBAVertex(u0, MaxV));

        for (int i = 1; i < SegmentsU; i++)
        {
            var u = MathHelper.Lerp(MinU, MaxU, (float)i / (SegmentsU - 1));
            _verticesList.Add(GetRGBAVertex(u, MinV));
            _verticesList.Add(GetRGBAVertex(u, MaxV));
        }

        _vertices = _verticesList.SelectMany(v => v.ToArray()).ToArray();
    }

    private static RGBAVertex GetRGBAVertex(float u, float v)
    {
        var position = new Vector3(GetX(u, v), GetY(u, v), GetZ(u, v));
    
        var color = new Color4(
            position.X,
            position.Y,
            position.Z + 0.4f,
            Color.A);
    
        return new RGBAVertex(position, color);
    }

    private static float GetX(float u, float v)
    {
        return (float)(MathHelper.Cos(u) * (1 + v/2 * MathHelper.Cos(u/2)));
    }

    private static float GetY(float u, float v)
    {
        return (float)(MathHelper.Sin(u) * (1 + v/2 * MathHelper.Cos(u/2)));
    }

    private static float GetZ(float u, float v)
    {
        return (float)(v/2 * MathHelper.Sin(u/2));
    }
}