using Labyrinth.Utilities;
using OpenTK.Mathematics;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Labyrinth.Models;

public class Labyrinth
{
    private static readonly Color4 InSideColor = Color4.Violet;
    private static readonly Color4 UpSideColor = Color4.Aqua;
    private static readonly Color4 BottomSideColor = Color4.Coral;
    private static readonly Color4 OutSideColor = Color4.OrangeRed;
    
    private readonly float[] _blockVertices;
    private readonly float[] _blockEdgesVertices;
    private readonly float[] _boxVertices;
    
    public readonly Vector3[] BlockPositions;

    public Labyrinth()
    {
        var blockVerticesList = Block.GetVerticesList(InSideColor);
        _blockVertices = blockVerticesList.SelectMany(vert => vert.ToArray()).ToArray();
        
        BlockPositions = LabyrinthLayout.GetBlockPositions();
        
        var edgesVerticesList = Block.GetEdgeVerticesList(Color4.BlueViolet);
        _blockEdgesVertices = edgesVerticesList.SelectMany(vert => vert.ToArray()).ToArray();
        
        var boxVerticesList = LabyrinthLayout.GetBoxVertices(OutSideColor, UpSideColor, BottomSideColor);
        _boxVertices = boxVerticesList.SelectMany(vert => vert.ToArray()).ToArray();
    }

    public void Draw(Renderer renderer)
    {
        foreach (var position in BlockPositions)
        {
            renderer.DrawElements(PrimitiveType.Quads, _blockVertices, Block.SideIndices, position);
            renderer.DrawElements(PrimitiveType.Lines, _blockEdgesVertices, Block.EdgeIndices, position, 2);
        }

        renderer.DrawElements(PrimitiveType.Quads, _boxVertices, LabyrinthLayout.BoxIndices, Vector3.Zero);
    }
}