using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Meadow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(1200, 1200),
                MinimumClientSize = new Vector2i(600, 900),
                MaximumClientSize = new Vector2i(1600, 1280),
                Location = new Vector2i(370, 300),
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,
                Title = "Meadow",

                Flags = ContextFlags.Default,
                APIVersion = new Version(3, 3),
                Profile = ContextProfile.Compatability
            };

            using (ViewWindow game = new ViewWindow(nativeWindowSettings))
            {
                game.Run();
            }
        }
    }
}
