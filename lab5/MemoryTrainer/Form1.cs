using MemoryTrainer.Models;
using MemoryTrainer.Utilities;
using MemoryTrainer.ViewModels;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace MemoryTrainer
{
    public partial class Form1 : Form
    {
        private static readonly Color4 BackgroundColor = new(0.92f, 0.91f, 0.95f, 1.0f);
        private readonly Vector3 _cameraPosition = new(0f, -6f, 5f);

        private const float AmbientStrength = 0.3f;
        private const float DiffuseStrength = 0.5f;

        private Matrix4 _viewMatrix;
        private Matrix4 _projectionMatrix;

        private Renderer _renderer;
        private GameViewModel _gameViewModel;
        private MouseHandler _mouseHandler;

        private readonly int _rows;
        private readonly int _columns;

        public Form1(int rows, int columns)
        {
            InitializeComponent();

            _rows = rows;
            _columns = columns;
            
            Application.Idle += Application_Idle;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();

            GL.ClearColor(BackgroundColor);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Multisample);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);

            SetLightProperties();

            _viewMatrix = Matrix4.LookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
            MathHelper.DegreesToRadians(45f),
            glControl1.Width / (float)glControl1.Height,
                0.1f, 100f
            );

            var gameModel = new GameModel(_rows, _columns);
            _gameViewModel = new GameViewModel(gameModel);
            _gameViewModel.Start();

            _mouseHandler = new MouseHandler(null, _gameViewModel); // You'll need to adapt this to work with WinForms
            _renderer = new Renderer(_gameViewModel);
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
            {
                Render();
            }
        }

        private void Render()
        {
            glControl1.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref _projectionMatrix);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref _viewMatrix);

            _gameViewModel.Update(1f / 60f); // Approximate delta time
            _renderer.Draw();

            glControl1.SwapBuffers();
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!glControl1.Context.IsCurrent)
                glControl1.MakeCurrent();

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
            MathHelper.DegreesToRadians(45f),
            glControl1.Width / (float)glControl1.Height,
                0.1f, 100f
            );
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseHandler.HandleMouseClick(e); // You'll need to adapt this method to take System.Windows.Forms.MouseEventArgs
        }

        private void SetLightProperties()
        {
            var lightPosition = new Vector3(0f, 0f, 2f);
            float[] lightPos = { lightPosition.X, lightPosition.Y, lightPosition.Z, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPos);

            float[] lightColorAmbient = { AmbientStrength, AmbientStrength, AmbientStrength, 1.0f };
            float[] lightColorDiffuse = { DiffuseStrength, DiffuseStrength, DiffuseStrength, 1.0f };
            float[] lightColorSpecular = { 1.0f, 1.0f, 1.0f, 1.0f };

            GL.Light(LightName.Light0, LightParameter.Ambient, lightColorAmbient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightColorDiffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, lightColorSpecular);

            GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
        }
    }
}
