using System.Numerics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RaytracingWithShadows.Models;
using RaytracingWithShadows.Utilities;

namespace RaytracingWithShadows;

public partial class MainWindow
{
    private const int WindowWidth = 1200;
    private const int WindowHeight = 800;
    
    private readonly Camera _camera;
    private readonly Scene _scene;
    private RayTracer _rayTracer;
    
    public MainWindow()
    {
        InitializeComponent();
        Width = WindowWidth;
        Height = WindowHeight;
        
        _camera = new Camera(
            position: new Vector3(5, 0, 5),
            target: Vector3.Zero,
            aspectRatio: (float)WindowWidth / WindowHeight);

        _scene = new Scene();
        _rayTracer = new RayTracer(_scene);
        
        Render();
    }

    private void Render()
    {
        WriteableBitmap bmp = new WriteableBitmap(WindowWidth, WindowHeight, 96, 96, PixelFormats.Bgra32, null);
        byte[] pixels = new byte[WindowWidth * WindowHeight * 4];
        
        for (int y = 0; y < WindowHeight; y++)
        {
            for (int x = 0; x < WindowWidth; x++)
            {
                float ndcX = (x + 0.5f) / WindowWidth * 2 - 1;
                float ndcY = 1 - (y + 0.5f) / WindowHeight * 2;

                Vector3 dir = _camera.GetRayDirection(ndcX, ndcY);
                Ray ray = new Ray(_camera.Position, dir);

                Vector3 color = _rayTracer.TraceRay(ray, _camera.Position);

                int index = (y * WindowWidth + x) * 4;
                pixels[index + 0] = (byte)(Math.Clamp(color.Z, 0, 1) * 255); // B
                pixels[index + 1] = (byte)(Math.Clamp(color.Y, 0, 1) * 255); // G
                pixels[index + 2] = (byte)(Math.Clamp(color.X, 0, 1) * 255); // R
                pixels[index + 3] = 255; // Alpha
            }
        }
        
        Dispatcher.Invoke(() =>
        {
            bmp.WritePixels(new Int32Rect(0, 0, WindowWidth, WindowHeight), pixels, WindowWidth * 4, 0);
            RenderImage.Source = bmp;
        });
    }
}