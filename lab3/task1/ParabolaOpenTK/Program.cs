using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace ParabolaOpenTK
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
                Title = "Parabola",

                Flags = ContextFlags.Default,
                APIVersion = new Version(3, 3),
                Profile = ContextProfile.Compatability
            };

            using (Game game = new Game(GameWindowSettings.Default, nativeWindowSettings))
            {
                // Парабола вида у = 2x^2 - 3x - 8 на интервале [-2;3] 
                game.Run();
            }
        }
    }
}
