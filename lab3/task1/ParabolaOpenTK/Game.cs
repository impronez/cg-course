using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace ParabolaOpenTK
{
    public class Game : GameWindow
    {
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
            : base(gameWindowSettings, nativeWindowSettings)
        {
            Console.WriteLine(GL.GetString(StringName.Version));

            VSync = VSyncMode.On;
        }

        private void DrawCoordinateAxes()
        {
            GL.Color3(1.0f, 1.0f, 1.0f);

            GL.Begin(PrimitiveType.Lines);

            GL.Vertex2(0.0f, 1.0f);
            GL.Vertex2(0.0f, -1.0f);

            GL.Vertex2(-1.0f, 0.0f);
            GL.Vertex2(1.0f, 0.0f);

            DrawDivisions();

            DrawArrows();

            GL.End();
        }

        private void DrawParabola()
        {
            GL.Color3(0.0f, 1.0f, 0.0f);

            GL.Begin(PrimitiveType.LineStrip);

            for (float x = -2.0f; x <= 3.0f; x += 0.01f)
            {
                float y = 2 * x * x - 3 * x - 8;
                GL.Vertex2(x * 0.1f, y * 0.1f);
            }

            GL.End();
        }

        private void DrawArrows()
        {
            GL.Vertex2(1.0f, 0.0f);
            GL.Vertex2(0.975f, 0.02f);
            GL.Vertex2(1.0f, 0.0f);
            GL.Vertex2(0.975f, -0.02f);

            GL.Vertex2(0.0f, 1.0f);
            GL.Vertex2(0.02f, 0.975f);
            GL.Vertex2(0.0f, 1.0f);
            GL.Vertex2(-0.02f, 0.975f);
        }

        private void DrawDivisions()
        {
            for (float i = -0.9f; i <= 1.0f;  i += 0.1f)
            {
                GL.Vertex2(i, 0.01f);
                GL.Vertex2(i, -0.01f);

                GL.Vertex2(0.01f, i);
                GL.Vertex2(-0.01f, i);
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color4.Black);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            DrawCoordinateAxes();
            DrawParabola();

            SwapBuffers();

            base.OnRenderFrame(args);
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);
            // Изменение матрицы проекции в зависимости от размера окна
            GL.Viewport(0, 0, e.Width, e.Height);
        }
    }
}
