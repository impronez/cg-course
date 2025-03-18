using MobiusStrip.Utilities;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace MobiusStrip;

public class MobiusStrip
{
    private const float MinU = 0f;
    private const float MaxU = MathHelper.TwoPi;
    
    private const float MinV = -1f;
    private const float MaxV = 1f;

    private const int SegmentsU = 100;
    private const int SegmentsV = 2;

    private float[] _vertices;
    private List<RGBAVertex> _verticesList;
    
    public MobiusStrip()
    {
        InitializeVertices();
    }
    
    public void Draw(Renderer renderer, Vector3 position)
    {
        renderer.DrawElements(PrimitiveType.Quads, _vertices, position);
    }

    private void InitializeVertices()
    {
        _verticesList = new List<RGBAVertex>();

        for (int i = 0; i < SegmentsU - 1; i++)
        {
            var u1 = MathHelper.Lerp(MinU, MaxU, (float)i / (SegmentsU - 1));
            var u2 = MathHelper.Lerp(MinU, MaxU, (float)(i + 1) / (SegmentsU - 1));
            
            var v1 = new Vector3(GetX(u1, MinV), GetY(u1, MinV), GetZ(u1, MinV));
            var v2 = new Vector3(GetX(u1, MaxV), GetY(u1, MaxV), GetZ(u1, MaxV));
            var v3 = new Vector3(GetX(u2, MaxV), GetY(u2, MaxV), GetZ(u2, MaxV));
            var v4 = new Vector3(GetX(u2, MinV), GetY(u2, MinV), GetZ(u2, MinV));
            
            _verticesList.Add(new RGBAVertex(v1, Color4.Coral));
            _verticesList.Add(new RGBAVertex(v2, Color4.Coral));
            _verticesList.Add(new RGBAVertex(v3, Color4.Coral));
            _verticesList.Add(new RGBAVertex(v4, Color4.Coral));
        }

        _vertices = _verticesList.SelectMany(v => v.ToArray()).ToArray();
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