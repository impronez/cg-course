using MemoryTrainer.ViewModels;
using OpenTK.GLControl;
using OpenTK.Mathematics;

public class MouseHandler
{
    private readonly GLControl _glControl;
    private readonly GameViewModel _gameViewModel;
    private readonly Matrix4 _viewMatrix;
    private readonly Matrix4 _projectionMatrix;
    private readonly Matrix4 _modelMatrix;  // Матрица модели

    public MouseHandler(GLControl glControl,
        GameViewModel gameViewModel,
        Matrix4 viewMatrix,
        Matrix4 projectionMatrix,
        Matrix4 modelMatrix)
    {
        _glControl = glControl;
        _gameViewModel = gameViewModel;
        _viewMatrix = viewMatrix;
        _projectionMatrix = projectionMatrix;
        _modelMatrix = modelMatrix;  // Инициализация матрицы модели
    }

    public void HandleClick(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;

        Vector2 mousePosition = new Vector2(e.X, e.Y);
        Vector3 worldPosition = UnProject(mousePosition, _glControl.Width, _glControl.Height);

        float centerRow = _gameViewModel.Rows / 2f - 0.5f;
        float centerCol = _gameViewModel.Columns / 2f - 0.5f;

        int row = (int)Math.Round(worldPosition.X + centerRow);
        int column = (int)Math.Round(worldPosition.Y + centerCol);

        if (row >= 0 && row < _gameViewModel.Rows && column >= 0 && column < _gameViewModel.Columns)
        {
            _gameViewModel.SelectTile(row, column);
        }
    }

    private Vector3 UnProject(Vector2 screenPosition, int screenWidth, int screenHeight)
    {
        // Объединенная матрица (матрица модели, вида и проекции)
        Matrix4 inversePV = Matrix4.Invert(_modelMatrix * _viewMatrix * _projectionMatrix);  // Сначала модель, затем вид, затем проекция

        float x = (2.0f * screenPosition.X) / screenWidth - 1.0f;
        float y = 1.0f + 0.07f - (2.0f * screenPosition.Y) / screenHeight;

        Vector4 nearPoint = new Vector4(x, y, -1.0f, 1.0f);
        Vector4 farPoint = new Vector4(x, y, 1.0f, 1.0f);

        // Используем оператор умножения, а не Transform
        Vector4 worldNear = inversePV * nearPoint;
        Vector4 worldFar = inversePV * farPoint;

        Vector3 near = new Vector3(worldNear.X, worldNear.Y, worldNear.Z) / worldNear.W;
        Vector3 far = new Vector3(worldFar.X, worldFar.Y, worldFar.Z) / worldFar.W;

        Vector3 direction = Vector3.Normalize(far - near);

        float t = -near.Z / direction.Z;
        Vector3 intersection = near + direction * t;

        return intersection;
    }
}
