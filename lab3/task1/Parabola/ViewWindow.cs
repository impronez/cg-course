using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Parabola
{
    public class ViewWindow : GameWindow
    {
        private Shader _shader;
        private Matrix4 _projection;

        private ParabolaArgs _args;

        public ViewWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, ParabolaArgs args) 
            : base(gameWindowSettings, nativeWindowSettings)
        {
            Console.WriteLine(GL.GetString(StringName.Version));

            VSync = VSyncMode.On;

            _args = args;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Use();
            UpdateVertexFormat();

            DrawCoordinateAxes();

            DrawParabola();

            SwapBuffers();

            base.OnRenderFrame(args);
        }

        private void DrawParabola()
        {
            var vertices = Parabola.GetVertices(_args.Function, _args.MinValue, _args.MaxValue, _args.Color);
            
            AddArrayToArrayBuffer(vertices.ToArray());
            GL.DrawArrays(PrimitiveType.LineStrip, 0, vertices.Count / 6);
        }

        private void DrawCoordinateAxes()
        {
            float color = 0.5f;
            float colorR = color;
            float colorG = color;
            float colorB = color;

            List<float> axesList = new()
            {
                0f, -1f, 0f, colorR, colorG, colorB,
                0f, 1f, 0f, colorR, colorG, colorB,
                -1f, 0f, 0f, colorR, colorG, colorB,
                1f, 0f, 0f, colorR, colorG, colorB,
            };

            AddStrokesToAxes(axesList, colorR, colorG, colorB);
            AddArrowsToAxes(axesList, colorR, colorG, colorB);

            AddArrayToArrayBuffer(axesList.ToArray());
            GL.DrawArrays(PrimitiveType.Lines, 0, axesList.Count / 6);
        }

        private void AddArrowsToAxes(List<float> axesList, float colorR, float colorG, float colorB)
        {
            float width = 0.02f;
            float indent = 0.03f;

            List<float> arrowsList = new()
            {
                -width, 1 - indent, 0f, colorR, colorG, colorB,
                0f, 1f, 0f, colorR, colorG, colorB,
                width, 1 - indent, 0f, colorR, colorG, colorB,
                0f, 1f, 0f, colorR, colorG, colorB,

                1 - indent, width, 0f, colorR, colorG, colorB,
                1f, 0f, 0f, colorR, colorG, colorB,
                1 - indent, -width, 0f, colorR, colorG, colorB,
                1f, 0f, 0f, colorR, colorG, colorB,
            };

            axesList.AddRange(arrowsList);
        }

        private void AddStrokesToAxes(List<float> axesList, float colorR, float colorG, float colorB)
        {
            float strokeWidth = 0.01f;

            for (float i = -0.9f; i < 1.0f;  i += 0.1f)
            {
                axesList.AddRange([i, strokeWidth, 0.0f, colorR, colorG, colorB]);
                axesList.AddRange([i, -strokeWidth, 0.0f, colorR, colorG, colorB]);

                axesList.AddRange([strokeWidth, i, 0.0f, colorR, colorG, colorB]);
                axesList.AddRange([-strokeWidth, i, 0.0f, colorR, colorG, colorB]);
            }
        }

        private void AddArrayToArrayBuffer(float[] array)
        {
            GL.BufferData(BufferTarget.ArrayBuffer, array.Length * sizeof(float), array, BufferUsageHint.StaticDraw);
        }

        private void UpdateVertexFormat()
        {
            // Вершины
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            // Цвет
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }

        private void UpdateOrthographicMatrix()
        {
            float aspectRatio = (float)Size.X / Size.Y;

            if (aspectRatio > 1)
            {
                _projection = Matrix4.CreateOrthographic(aspectRatio * 2.0f, 2.0f, -1.0f, 1.0f);
            }
            else
            {
                _projection = Matrix4.CreateOrthographic(2.0f, 2.0f / aspectRatio, -1.0f, 1.0f);
            }

            _shader.SetMatrix4("projection", _projection);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            int vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            int VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            _shader = new Shader("shader.vert", "shader.frag");

            UpdateOrthographicMatrix();
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            _shader.Dispose();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
            UpdateOrthographicMatrix();
        }
    }
}
