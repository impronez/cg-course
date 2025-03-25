using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using Labyrinth.Shaders;
using Labyrinth.Utilities;

namespace Labyrinth;

public class ViewWindow : GameWindow
    {
        private Renderer _renderer;

        private Shader _shader;

        private Camera _camera;

        private Models.Labyrinth _labyrinth;

        private bool _firstMove = true;

        private readonly Vector3 _lightColor = new (1.0f, 1.0f, 1.0f);
        private readonly Vector3 _initCameraPosition = new (0, 0, 8.0f);

        private Vector2 _lastPos;
        
        private InputHandler _inputHandler;
        private CollisionHandler _collisionHandler;

        public ViewWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.8f, 0.8f, 0.8f, 1.0f);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            _camera = new Camera(_initCameraPosition, Size.X / (float)Size.Y);
            
            _renderer = new Renderer(_shader);

            _labyrinth = new Models.Labyrinth();

            _collisionHandler = new CollisionHandler(_labyrinth);
            _inputHandler = new InputHandler(_camera, _collisionHandler);
            
            CursorState = CursorState.Grabbed;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _shader.SetVector3("lightColor", _lightColor);
            _shader.SetVector3("lightPos", _camera.Position);
            _shader.SetVector3("viewPos", _camera.Position);

            _labyrinth.Draw(_renderer);
            
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused)
            {
                return;
            }

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            _inputHandler.ProcessKeyboard(KeyboardState, (float)e.Time);
            _inputHandler.ProcessMouse(MouseState, ref _lastPos, ref _firstMove);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _camera.Fov -= e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float)Size.Y;
        }
    }