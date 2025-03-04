using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Meadow
{
    class MeadowRenderer : IRenderer
    {        
        private readonly float _left;
        private readonly float _top;
        private readonly float _width;
        private readonly float _height;

        private const float _skyRatio = 0.7f;
        private const float _fieldRatio = 0.3f;

        public MeadowRenderer(
            float left,
            float top,
            float width,
            float height)
        {
            _left = left;
            _top = top;
            _width = width;
            _height = height;
        }

        public void Render()
        {
            ConfigureVertexAttributes();

            DrawSky();
            DrawField();
            DrawSun();
            DrawClouds();
            DrawGrass();
            DrawFlowers();
            DrawButterflies();
        }

        private void RenderVertices(List<RGBVertex> list, PrimitiveType mode)
        {
            var array = list.SelectMany(v => RGBVertex.ToFloatArray(v)).ToArray();

            GL.BufferData(BufferTarget.ArrayBuffer, array.Length * sizeof(float), array, BufferUsageHint.StaticDraw);
            GL.DrawArrays(mode, 0, array.Length / 6);
        }

        private void ConfigureVertexAttributes()
        {
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, RGBVertex.VertexSize * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
         
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false,
                RGBVertex.VertexSize * sizeof(float), RGBVertex.ColorIndex * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }

        private void DrawSky()
        {
            DrawFilledRectangle(_left, _top, _width, _height *_skyRatio, Color4.DeepSkyBlue);
        }

        private void DrawField()
        {
            DrawFilledRectangle(_left, (_top - _height) + _height * _fieldRatio, _width, _height * _fieldRatio, Color4.Green);
        }

        private void DrawSun()
        {
            float radius = 0.15f;
            float x = _left + _width - 2 * radius;
            float y = _top - 2 * radius;

            DrawFilledEllipse(x, y, radius, radius, Color4.Yellow);
        }

        private void DrawClouds()
        {
            float x = _left + _width / 2;
            float y = _top - _height * 0.1f;

            float rx = _width * 0.1f;
            float ry = _height * 0.05f;

            DrawTripleCloud(x, y, rx, ry, Color4.White);

            x = _left + _width / 4;
            y = _top - _height * 0.2f;

            DrawTripleCloud(x, y, rx, ry, Color4.White);
        }

        private void DrawTripleCloud(float cx, float cy, float rx, float ry, Color4 color)
        {
            DrawFilledEllipse(cx - rx / 2, cy, ry * 0.7f, ry * 0.7f, color);
            DrawFilledEllipse(cx, cy, rx / 2, ry, color);
            DrawFilledEllipse(cx + rx / 2, cy, ry * 0.7f, ry * 0.7f, color);
        }

        private void DrawGrass()
        {
            DrawGrassFirstType();
            DrawGrassSecondType();
            DrawGrassThirdType();
        }

        private void DrawGrassFirstType()
        {
            Color4 color = Color4.LightGreen;
            var list = new List<RGBVertex>();

            var cx = _left + _width / 10f;
            var cy = -_height + _top + _height / 10f;

            for (int i = 10; i <= 170; i += 5)
            {
                double angle = Math.PI * 2 * i / 360;

                float x = cx + (float)Math.Cos(angle) * 0.1f;
                float y = cy + (float)Math.Sin(angle) * 0.1f;

                list.Add(new RGBVertex(cx, cy, color));
                list.Add(new RGBVertex(x, y, color));
                list.Add(new RGBVertex(cx, cy, color));
                list.Add(new RGBVertex(x, y, color));
            }

            RenderVertices(list, PrimitiveType.Lines);
        }

        private void DrawGrassSecondType()
        {
            float startX = _left + _width / 2f * 0.7f;
            float startY = -_height + _top + _height * 0.1f;
            float width = _width / 15f;
            float height = _height / 18f;

            var list = new List<RGBVertex>();
            var color = Color4.DarkSeaGreen;

            float stepX = width / 15f;

            for (float i = 0; i < 10; i++)
            {
                float x = startX + i * stepX;
                float y1 = startY + height * 0.1f;
                float y2 = y1 + height;

                list.Add(new RGBVertex(x, y1, color));
                list.Add(new RGBVertex(x, y2, color));
            }

            RenderVertices(list, PrimitiveType.Lines);
        }

        private void DrawGrassThirdType()
        {
            float startX = _left + _width * 0.5f;
            float startY = (_top - _height) + _height / 10f;
            float width = _width * 0.1f;
            float height = _height * 0.1f;

            var list = new List<RGBVertex>();
            var color = Color4.LawnGreen;

            float stepX = width / 10f;
            float stepY = height / 10f;

            for (float i = 0; i < 10; i++)
            {
                for (float j = 0; j < 5; j++)
                {
                    float x1 = startX + i * stepX;
                    float y1 = startY + j * stepY;
                    float x2 = x1 + 0.01f;
                    float y2 = y1 + 0.02f;

                    list.Add(new RGBVertex(x1, y1, color));
                    list.Add(new RGBVertex(x2, y2, color));
                }
            }

            RenderVertices(list, PrimitiveType.Polygon);
        }

        private void DrawFlowers()
        {
            DrawFlowerFirstType();
            DrawFlowerSecondType();
            DrawFlowerThirdType();
        }

        private void DrawFlowerFirstType()
        {
            float x = _left + _width / 4f;
            float y = (_top - _height) + _height * 0.35f;
            float size = _height / 2f * 0.03f;

            DrawFlowerStem(x - 0.0025f, y, 0.005f, _height * 0.1f, Color4.DarkGreen);

            for (int i = 0; i < 360; i += 72)
            {
                double angle = Math.PI * i / 180.0;
                float petalX = x + (float)Math.Cos(angle) * size * 0.9f;
                float petalY = y + (float)Math.Sin(angle) * size * 0.9f;

                DrawFilledEllipse(petalX, petalY, size, size, Color4.Pink);
            }

            DrawFilledEllipse(x, y, _width / 100f, _height / 100f, Color4.Yellow);
        }

        private void DrawFlowerSecondType()
        {
            float x = _left + _width / 2f * 0.9f;
            float y = (_top - _height) + _height * 0.25f;
            float size = _height / 2f * 0.03f;

            DrawFlowerStem(x - 0.0025f, y, 0.005f, _height * 0.1f, Color4.DarkGreen);

            var list = new List<RGBVertex>();
            Color4 color = Color4.Red;
            for (int i = 0; i < 5; i++)
            {
                double angle = Math.PI * (72 * i) / 180.0;
                float petalX = x + (float)Math.Cos(angle) * size * 2f;
                float petalY = y + (float)Math.Sin(angle) * size * 2f;

                list.Add(new RGBVertex(x, y, color));
                list.Add(new RGBVertex(petalX, petalY, color));
                list.Add(new RGBVertex(x + (float)Math.Cos(angle + Math.PI / 5) * size * 1.5f,
                    y + (float)Math.Sin(angle + Math.PI / 5) * size * 1.5f, color));
            }

            RenderVertices(list, PrimitiveType.TriangleFan);

            DrawFilledEllipse(x, y, 0.01f, 0.01f, Color4.Orange);
        }

        private void DrawFlowerThirdType()
        {
            var list = new List<RGBVertex>();
            var color = Color4.Red;

            float x = _left + _width * 0.8f; 
            float y = (_top - _height) + _height * 0.3f;
            float size = _height / 20f;

            DrawFlowerStem(x - 0.0025f, y, 0.005f, _height * 0.1f, Color4.DarkGreen);

            for (int i = 0; i <= 180; i += 5)
            {
                double angle = Math.PI * 2 * -i / 360;

                float offsetX = x + (float)Math.Cos(angle) * x / 20f;
                float offsetY = y + (float)Math.Sin(angle) * size * 0.4f;

                list.Add(new RGBVertex(offsetX, offsetY, color));
            }

            RenderVertices(list, PrimitiveType.TriangleFan);

            list.Clear();

            list.Add(new RGBVertex(x - x / 20f, y, color));
            list.Add(new RGBVertex(x - x * 0.03f, y + size * 0.2f, color));
            list.Add(new RGBVertex(x, y, color));
            list.Add(new RGBVertex(x, y, color));
            list.Add(new RGBVertex(x + x * 0.03f, y + size * 0.2f, color));
            list.Add(new RGBVertex(x + x / 20f, y, color));

            RenderVertices(list, PrimitiveType.TriangleFan);
        }

        private void DrawButterflies()
        {
            DrawButterflyFirstType();
            DrawButterflySecondType();
            DrawButterflyThirdType();
        }

        private void DrawButterflyFirstType()
        {
            float cx = _left + _width * 0.3f;
            float cy = (_top - _height) + _height * 0.4f;
            float radius = _width / 100f;

            DrawFilledEllipse(cx + _width / 200f, cy + _height / 200f, radius - _width / 400f, radius - _height / 400f, Color4.Sienna);
            DrawFilledEllipse(cx + _width / 200f, cy + _height / 200f, radius - _width / 2000f * 15f, radius - _height / 2000f * 15f, Color4.Salmon);

            DrawFilledEllipse(cx + _width / 200f * 3f, cy + _height / 200f, radius -_width / 400f, radius - _height / 400f, Color4.Sienna);
            DrawFilledEllipse(cx + _width / 200f * 3f, cy + _height / 200f, radius - _width / 2000f * 15f, radius - _height / 2000f * 15f, Color4.Salmon);

            DrawFilledTriangle(cx + _width / 100f, cy + _height / 200f,
                cx - _width / 100f, cy + _height / 100f * 3f,
                cx + _width / 100f, cy + _height / 100f * 3f, Color4.Sienna);
            DrawFilledEllipse(cx + _width / 200f, cy + _height / 2000f * 45f, radius - _width / 2000f * 15f, radius - _height / 2000f * 15f, Color4.Salmon);

            DrawFilledTriangle(cx + _width / 100f, cy + _height / 200f,
                cx + _width / 100f * 3f, cy + _height / 100f * 3f,
                cx + _width / 100f, cy + _height / 100f * 3f, Color4.Sienna);
            DrawFilledEllipse(cx + _width / 200f * 3f, cy + _height / 2000f * 45f, radius - _width / 2000f * 15f, radius - _height / 2000f * 15f, Color4.Salmon);

            var list = new List<RGBVertex>()
            {
                new RGBVertex(cx + _width / 100f, cy + _height / 50f, Color4.Black),
                new RGBVertex(cx + _width / 200f, cy + _height / 25f, Color4.Black),
                new RGBVertex(cx + _width / 100f, cy + _height / 50f, Color4.Black),
                new RGBVertex(cx + _width / 200f * 3f, cy + _height / 25f, Color4.Black)
            };

            RenderVertices(list, PrimitiveType.Lines);
        }

        private void DrawButterflySecondType()
        {
            float cx = _left + _width / 2f;
            float cy = (_top - _height) + _height * 0.4f;

            DrawFilledTriangle(cx, cy, cx - _width / 200f * 3f, cy + _height / 50f, cx, cy + _height / 40f, Color4.BurlyWood);
            DrawFilledEllipse(cx - _width / 200f, cy + _height / 2000f * 32f, _width / 400f, _height / 400f, Color4.Beige);

            DrawFilledTriangle(cx, cy, cx + _width / 200f * 3f, cy + _height / 50f, cx, cy + _height / 40f, Color4.BurlyWood);
            DrawFilledEllipse(cx + _width / 200f, cy + _height / 2000f * 32f, _width / 400f, _height / 400f, Color4.Beige);

            DrawFilledTriangle(cx, cy + _height / 200f,
                cx - _width / 100f, cy - _height / 1000f * 9f,
                cx, cy - _height / 1000f * 9f, Color4.BurlyWood);
            DrawFilledTriangle(cx, cy + _height / 200f,
                cx + _width / 100f,cy - _height / 1000f * 9f,
                cx, cy - _height / 1000f * 9f, Color4.BurlyWood);

            DrawFilledTriangle(cx - _width / 200f, cy - _height / 200f,
                cx, cy - _height / 2000f,
                cx + _width / 200f, cy - _height / 200f, Color4.Beige);

            var list = new List<RGBVertex>()
            {
                new RGBVertex(cx, cy + _height / 50f, Color4.Black),
                new RGBVertex(cx - _width / 200f, cy + _height / 25f, Color4.Black),
                new RGBVertex(cx, cy + _height / 50f, Color4.Black),
                new RGBVertex(cx + _width / 200f, cy + _height / 25f, Color4.Black)
            };

            RenderVertices(list, PrimitiveType.Lines);
        }

        private void DrawButterflyThirdType()
        {
            float cx = _left + _width * 0.8f;
            float cy = _top - _width * 0.5f;
            float radius = _width / 100f;

            DrawFilledEllipse(cx + _width / 200f, cy + _height / 200f, radius - _width / 400f, radius - _height / 400f, Color4.BlueViolet);
            DrawFilledEllipse(cx + _width / 200f, cy + _height / 200f, radius - _width / 400f * 3f, radius - _height / 400f * 3f, Color4.OrangeRed);

            DrawFilledEllipse(cx + _width / 200f * 3f, cy + _height/ 200f, radius - _width / 400f, radius - _height / 400f, Color4.BlueViolet);
            DrawFilledEllipse(cx + _width / 200f * 3f, cy + _height / 200f, radius - _width / 400f * 3f, radius - _height / 400f * 3f, Color4.OrangeRed);

            DrawFilledEllipse(cx + _width / 400f, cy + _height / 400f * 7f, radius, radius, Color4.BlueViolet);
            DrawFilledEllipse(cx + _width / 400f, cy + _height / 400f * 7f, radius - _width / 200f, radius - _height/ 200f, Color4.OrangeRed);

            DrawFilledEllipse(cx + _width / 400f * 7f, cy + _height / 400f * 7f, radius, radius, Color4.BlueViolet);
            DrawFilledEllipse(cx + _width / 400f * 7f, cy + _height / 400f * 7f, radius - _width / 200f, radius - _height / 200f, Color4.OrangeRed);


            var list = new List<RGBVertex>()
            {
                new RGBVertex(cx + _width / 100f, cy + _height / 50f, Color4.Black),
                new RGBVertex(cx + _width / 200f, cy + _height / 25f, Color4.Black),
                new RGBVertex(cx + _width / 100f, cy + _height / 50f, Color4.Black),
                new RGBVertex(cx + _width / 200f * 3f, cy + _height / 25f, Color4.Black)
            };

            RenderVertices(list, PrimitiveType.Lines);
        }

        private void DrawFlowerStem(float x, float y, float width, float height, Color4 color)
        {
            var list = new List<RGBVertex>()
            {
                new RGBVertex(x, y, color),
                new RGBVertex(x + width, y, color),
                new RGBVertex(x + width, y - height, color),
                new RGBVertex(x, y - height, color),
                new RGBVertex(x, y, color)
            };

            RenderVertices(list, PrimitiveType.Polygon);
        }

        private void DrawFilledRectangle(float left, float top, float width, float height, Color4 color)
        {
            var list = new List<RGBVertex>()
            {
                new RGBVertex(left, top, color),
                new RGBVertex(left + width, top, color),
                new RGBVertex(left + width, top - height, color),
                new RGBVertex(left, top - height, color),
                new RGBVertex(left, top, color)
            };

            RenderVertices(list, PrimitiveType.Polygon);
        }

        private void DrawFilledEllipse(float cx, float cy, float rx, float ry, Color4 color)
        {
            var list = new List<RGBVertex>();
            int segments = 50;

            for (int i = 0; i < segments; i++)
            {
                double angle = Math.PI * 2 * i / segments;

                float x = cx + (float)Math.Cos(angle) * rx;
                float y = cy + (float)Math.Sin(angle) * ry;

                list.Add(new RGBVertex(x, y, color));
            }

            RenderVertices(list, PrimitiveType.Polygon);
        }

        private void DrawFilledTriangle(float x1, float y1, float x2, float y2, float x3, float y3, Color4 color)
        {
            var list = new List<RGBVertex>()
            {
                new RGBVertex(x1, y1, color),
                new RGBVertex(x2, y2, color),
                new RGBVertex(x3, y3, color)
            };

            RenderVertices(list, PrimitiveType.Polygon);
        }
    }
}
