using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using SpaceEngine;

namespace SpaceEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings gws = new GameWindowSettings();

            var settings = new NativeWindowSettings()
            {
                Size = new Vector2i(1280, 720),
                Title = "SpaceEngine",
                WindowState = WindowState.Normal,
                NumberOfSamples = 0,
                WindowBorder = OpenTK.Windowing.Common.WindowBorder.Resizable,
            };

            using (var window = new Game(gws, settings))
            {
                window.Run();
            }


        }
    }
}
