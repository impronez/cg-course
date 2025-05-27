using MemoryTrainer.Models;
using MemoryTrainer.Utilities;
using MemoryTrainer.ViewModels;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace MemoryTrainerForms
{
    public partial class Form1 : Form
    {
        private static readonly Color4 BackgroundColor = new(0.92f, 0.91f, 0.95f, 1.0f);
        private readonly Vector3 _cameraPosition = new(0f, -5f, 6f);

        private const float AmbientStrength = 0.3f;
        private const float DiffuseStrength = 0.5f;

        private Matrix4 _projectionMatrix;
        private Matrix4 _viewMatrix;
        private Matrix4 _modelMatrix;

        private Renderer _renderer;

        private GameViewModel _gameViewModel;
        private MouseHandler _mouseHandler;

        private DateTime _lastTime;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;

            Application.Idle += Application_Idle;
        }

        private void GameViewModel_GameStateChanged(object sender, GameState newState)
        {
            if (newState == GameState.Win)
            {
                glControl1.Visible = false;
                panel1.Visible = true;
            }
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            glControl1.Visible = false;
            glControl1.CreateControl();
            glControl1.MakeCurrent();
        }

        private void GLControlLoad(object sender, EventArgs args)
        {
            _lastTime = DateTime.Now;

            GL.ClearColor(BackgroundColor.R, BackgroundColor.G, BackgroundColor.B, BackgroundColor.A);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Multisample);
            GL.Enable(EnableCap.Texture2D);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);
            SetLightProperties();

            _viewMatrix = Matrix4.LookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);
            _modelMatrix = Matrix4.Identity;
            ResizeViewport();

            var gameModel = new GameModel();
            _gameViewModel = new GameViewModel(gameModel);

            _mouseHandler = new MouseHandler(glControl1, _gameViewModel, _viewMatrix, _projectionMatrix, _modelMatrix);

            _renderer = new Renderer(_gameViewModel);

            _gameViewModel.GameStateChanged += GameViewModel_GameStateChanged;
        }

        private void GLControlMouseDown(object sender, MouseEventArgs args)
        {
            _mouseHandler.HandleClick(sender, args);
        }

        private void GLControlRender(object sender, PaintEventArgs args)
        {
            TimeSpan elapsedTime = DateTime.Now - _lastTime;
            _lastTime = DateTime.Now;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref _viewMatrix);
            SetLightProperties();

            GL.Enable(EnableCap.Texture2D);
            _gameViewModel.Update((float)elapsedTime.TotalSeconds);

            _renderer.Draw();

            glControl1.SwapBuffers();
        }

        private void GLControlResize(object sender, EventArgs args)
        {
            ResizeViewport();
        }

        private void ResizeViewport()
        {
            GL.Viewport(0, 0, Width, Height);
            float aspectRatio = (float)Width / (float)Height;
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f),
                aspectRatio,
                0.1f,
                100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref _projectionMatrix);
        }

        private void SetLightProperties()
        {
            var lightPosition = new Vector3(0f, 0f, 2f);
            float[] lightPos = [lightPosition.X, lightPosition.Y, lightPosition.Z, 1.0f];

            GL.Light(LightName.Light0, LightParameter.Position, lightPos);

            float[] lightColorAmbient = [AmbientStrength, AmbientStrength, AmbientStrength, 1.0f];
            float[] lightColorDiffuse = [DiffuseStrength, DiffuseStrength, DiffuseStrength, 1.0f];
            float[] lightColorSpecular = [1.0f, 1.0f, 1.0f, 1.0f];

            GL.Light(LightName.Light0, LightParameter.Ambient, lightColorAmbient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightColorDiffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, lightColorSpecular);

            GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
        }

        private void GameModeButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is string tag)
            {
                var parts = tag.Split('x');
                if (parts.Length == 2 && int.TryParse(parts[0], out int rows) && int.TryParse(parts[1], out int cols))
                {
                    _gameViewModel.Start(rows, cols);
                    panel1.Visible = false;
                    glControl1.Visible = true;
                }
            }
        }
    }
}
