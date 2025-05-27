using MemoryTrainer;
using MemoryTrainer.Utilities;
using MemoryTrainer.ViewModels;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class MouseHandler
{
    private readonly ViewWindow _viewWindow;
    private readonly GameViewModel _gameViewModel;
    private readonly float _tileWidth = Renderer.TileWidth;
    private readonly float _tileHeight = Renderer.TileHeight;

    public MouseHandler(ViewWindow viewWindow, GameViewModel gameViewModel)
    {
        _viewWindow = viewWindow;
        _gameViewModel = gameViewModel;
    }

    public void HandleMouseClick(MouseButtonEventArgs e)
    {
        if (e.Button != MouseButton.Left) return;

        // Получаем координаты клика через MouseState
        var mousePosition = _viewWindow.MouseState.Position;

        // Проекцируем точку из оконных координат в мировые координаты
        Vector3 worldPosition = UnProject(mousePosition);

        // Рассчитываем, на какую плитку был клик
        int row = (int)Math.Floor(worldPosition.X + _gameViewModel.Rows / 2f);
        int column = (int)Math.Floor(worldPosition.Y + _gameViewModel.Columns / 2f);

        // Проверяем, что индексы плитки находятся в пределах допустимого диапазона
        if (row >= 0 && row < _gameViewModel.Rows && column >= 0 && column < _gameViewModel.Columns)
        {
            // Вызываем метод SelectTile в GameViewModel, передаем координаты плитки
            _gameViewModel.SelectTile(row, column);
        }
    }

    private Vector3 UnProject(Vector2 screenPosition)
    {
        Matrix4 projection = _viewWindow.ProjectionMatrix;
        Matrix4 view = _viewWindow.ViewMatrix;
        Matrix4 inversePV = Matrix4.Invert(view * projection);

        float x = (2.0f * screenPosition.X) / _viewWindow.Size.X - 1.0f;
        float y = 1.0f - (2.0f * screenPosition.Y) / _viewWindow.Size.Y;

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