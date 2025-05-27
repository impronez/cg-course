using _3dsExmaple;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

var native = new NativeWindowSettings
{
    ClientSize = new Vector2i(800, 600),
    Title = "Assimp + OpenTK"
};

using var window = new ViewWindow(native);
window.Run();