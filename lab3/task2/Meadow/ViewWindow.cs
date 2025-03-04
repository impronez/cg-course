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
        private IRenderer _painter;

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

            _painter.Render();

            SwapBuffers();

            base.OnRenderFrame(args);
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

            _painter = new MeadowRenderer(-1.0f, 1.0f, 2.0f, 2.0f);

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
