using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace Parabola
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

            Func<float, float> function = x => 2 * x * x - 3 * x - 8;

            GraphArgs argss = new()
            {
                Left = -1.0f,
                Top = 1.0f,
                Width = 2.0f,
                Height = 2.0f,
                MinValue = -2,
                MaxValue = 3,
                Function = function,
                GraphColor = Color4.OrangeRed,
                AxesColor = Color4.White
            };

            using (ViewWindow game = new ViewWindow(GameWindowSettings.Default, nativeWindowSettings, argss))
            {
                game.Run();
            }
        }
    }
}
