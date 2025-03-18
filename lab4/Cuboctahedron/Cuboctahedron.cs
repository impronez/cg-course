using Cuboctahedron.Utilities;
using OpenTK.Mathematics;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Cuboctahedron;

public class Cuboctahedron
{
    private static readonly Color4 EdgeColor = Color4.Black;
    private static readonly Color4 TriangleColor = Color4.Goldenrod;
    private static readonly Color4 SquareColor = Color4.SteelBlue;
    
    private readonly Vector3[] _vertices =
    {
        // Все против часовой стрелки
        // Верхние вершины
        new(-1,  1,  0),  // 0
        new( 0,  1,  1),  // 1
        new( 1,  1,  0),  // 2
        new( 0,  1, -1),  // 3

        // Нижние вершины
        new(-1, -1,  0),  // 4
        new( 0, -1,  1),  // 5
        new( 1, -1,  0),  // 6
        new( 0, -1, -1),  // 7

        // Средние вершины
        new(-1,  0,  1),  // 8
        new( 1,  0,  1),  // 9
        new(1,  0, -1),  // 10
        new(-1,  0, -1)   // 11
    };

    private static readonly int[] EdgeIndices =
    [
        0, 1, 1, 2, 2, 3, 0, 3,
        4, 5, 5, 6, 6, 7, 7, 4,
        0, 11, 11, 4, 4, 8, 8, 0,
        1, 8, 8, 5, 5, 9, 9, 1,
        2, 9, 9, 6, 6, 10, 10, 2,
        3, 10, 10, 7, 7, 11, 11, 3
    ];

    private static readonly int[] SquareIndices =
    [
        // Грани, параллельные оси x
        0, 1, 2, 3,
        7, 6, 5, 4,
        // Грани, перпендикулярные оси х
        0, 11, 4, 8,
        1, 8, 5, 9,
        2, 9, 6, 10,
        3, 10, 7, 11
    ];

    private static readonly int[] TriangleIndices =
    [
        3, 11, 0,
        4, 11, 7,
        0, 8, 1,
        5, 8, 4,
        1, 9, 2,
        6, 9, 5,
        2, 10, 3,
        7, 10, 6
    ];
    
    private readonly List<RGBVertex> _rgbVerticesList;

    public Cuboctahedron()
    {
        _rgbVerticesList = _vertices.Select(v => new RGBVertex(v)).ToList();
    }
    
    public void Draw(Renderer renderer, Vector3 position)
    {
        SetVerticesColor(EdgeColor);
        renderer.DrawElements(PrimitiveType.Lines, _rgbVerticesList, EdgeIndices, position, 2);
        
        SetVerticesColor(SquareColor);
        renderer.DrawElements(PrimitiveType.Quads, _rgbVerticesList, SquareIndices, position);
        
        SetVerticesColor(TriangleColor);
        renderer.DrawElements(PrimitiveType.Triangles, _rgbVerticesList, TriangleIndices, position);
    }

    private void SetVerticesColor(Color4 color)
    {
        for (int i = 0; i < _rgbVerticesList.Count; i++)
        {
            _rgbVerticesList[i] = new RGBVertex(_rgbVerticesList[i].Position, color);
        }
    }
}