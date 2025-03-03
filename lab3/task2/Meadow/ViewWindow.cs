using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Meadow
{
    public class ViewWindow : GameWindow
    {
        private Shader _shader;
        private Matrix4 _projection;
        private IPainter _painter;

        public ViewWindow(NativeWindowSettings nativeWindowSettings) 
            : base(GameWindowSettings.Default, nativeWindowSettings)
        {
            Console.WriteLine(GL.GetString(StringName.Version));

            VSync = VSyncMode.On;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Use();
            UpdateVertexFormat();

            //Painter.DrawMeadow(AddArrayToArrayBuffer);
            _painter.Draw();

            SwapBuffers();

            base.OnRenderFrame(args);
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

            _painter = new MeadowPainter(AddArrayToArrayBuffer, -1.0f, 1.0f, 2.0f, 2.0f);

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
