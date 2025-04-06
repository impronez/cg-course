using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Cube;

class Program
{
    static void Main(string[] args)
    {
        var nativeWindowSettings = new NativeWindowSettings
        {
            ClientSize = new Vector2i(800, 900),
            Title = "Cube",

            Flags = ContextFlags.Default,
            APIVersion = new Version(3, 3),
            Profile = ContextProfile.Compatability
        };

        using ViewWindow game = new ViewWindow(GameWindowSettings.Default, nativeWindowSettings);
        game.Run();
    }
}