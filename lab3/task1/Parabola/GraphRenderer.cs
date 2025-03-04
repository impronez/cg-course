using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Parabola
{
    public struct GraphArgs
    {
        public float Left;
        public float Top;
        public float Width;
        public float Height;

        public int MinValue;
        public int MaxValue;

        public Func<float, float> Function;

        public Color4 GraphColor;
        public Color4 AxesColor;
    }

    public class GraphRenderer
    {
        private GraphArgs _args;

        private const float _strokeStepRatio = 0.05f;

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private float _centerX;
        private float _centerY;

        public GraphRenderer(GraphArgs args)
        {
            _args = args;

            _centerX = args.Left + args.Width / 2;
            _centerY = args.Top - args.Height / 2;
        }

        public void Initialize()
        {
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
        }

        public void Render()
        {
            UpdateVertexInfo();
            DrawCoordAxes();
            DrawParabola();
        }

        private void DrawParabola()
        {
            float strokeWidth = GetMaxDimension() * 0.005f;
            float step = GetMaxDimension() * _strokeStepRatio;

            var vertices = new List<RGBVertex>();
            for (float x = _args.MinValue; x < _args.MaxValue; x += step)
            {
                vertices.Add(new RGBVertex(_centerX + x * step, _centerY + _args.Function(x) * step, _args.GraphColor));
            }

            DrawVertices(vertices, PrimitiveType.LineStrip, 0);
        }

        private void DrawCoordAxes()
        {
            var color = _args.AxesColor;

            var list = new List<RGBVertex>
            {
                new RGBVertex(_args.Left, _centerY, color),
                new RGBVertex(_args.Left + _args.Width, _centerY, color),

                new RGBVertex(_centerX, _args.Top, color),
                new RGBVertex(_centerX, _args.Top - _args.Height, color),
            };

            AddStrokes(list);
            AddArrows(list);

            DrawVertices(list, PrimitiveType.Lines, 0);
        }

        private void AddStrokes(List<RGBVertex> list)
        {
            float strokeWidth = GetMaxDimension() * _strokeStepRatio / 10f;
            float step = GetMaxDimension() * _strokeStepRatio;

            float right = _args.Left + _args.Width;
            float bottom = _args.Top - _args.Height;

            for (int i = 0; i < _args.Width / 2 / step; i++)
            {
                list.Add(new RGBVertex(_centerX - step * i, _centerY + strokeWidth / 2, _args.AxesColor));
                list.Add(new RGBVertex(_centerX - step * i, _centerY - strokeWidth / 2, _args.AxesColor));

                list.Add(new RGBVertex(_centerX + step * i, _centerY + strokeWidth / 2, _args.AxesColor));
                list.Add(new RGBVertex(_centerX + step * i, _centerY - strokeWidth / 2, _args.AxesColor));
            }

            for (int i = 0; i < _args.Height / 2 / step; i++)
            {
                list.Add(new RGBVertex(_centerX - strokeWidth / 2, _centerY - step * i, _args.AxesColor));
                list.Add(new RGBVertex(_centerX + strokeWidth / 2, _centerY - step * i, _args.AxesColor));

                list.Add(new RGBVertex(_centerX - strokeWidth / 2, _centerY + step * i, _args.AxesColor));
                list.Add(new RGBVertex(_centerX + strokeWidth / 2, _centerY + step * i, _args.AxesColor));
            }
        }

        private void AddArrows(List<RGBVertex> list)
        {
            float strokeWidth = GetMaxDimension() * 0.005f;

            list.Add(new RGBVertex(_centerX - strokeWidth, _args.Top - strokeWidth, _args.AxesColor));
            list.Add(new RGBVertex(_centerX, _args.Top, _args.AxesColor));
            list.Add(new RGBVertex(_centerX, _args.Top, _args.AxesColor));
            list.Add(new RGBVertex(_centerX + strokeWidth, _args.Top - strokeWidth, _args.AxesColor));

            list.Add(new RGBVertex(_args.Left + _args.Width - strokeWidth, _centerY - strokeWidth, _args.AxesColor));
            list.Add(new RGBVertex(_args.Left + _args.Width, _centerY, _args.AxesColor));
            list.Add(new RGBVertex(_args.Left + _args.Width, _centerY, _args.AxesColor));
            list.Add(new RGBVertex(_args.Left + _args.Width - strokeWidth, _centerY + strokeWidth, _args.AxesColor));
        }

        private float GetMaxDimension()
        {
            return _args.Width > _args.Height ? _args.Width : _args.Height;
        }

        private void DrawVertices(List<RGBVertex> list, PrimitiveType mode, int firstElem)
        {
            var array = list.SelectMany(v => RGBVertex.ToFloatArray(v)).ToArray();

            GL.BufferData(BufferTarget.ArrayBuffer, array.Length * sizeof(float), array, BufferUsageHint.StaticDraw);
            GL.DrawArrays(mode, firstElem, list.Count);
        }

        private void UpdateVertexInfo()
        {
            // Взаимодействие с шейдерами
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, RGBVertex.VertexSize * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false,
                RGBVertex.VertexSize * sizeof(float), RGBVertex.ColorIndex * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }
    }
}
