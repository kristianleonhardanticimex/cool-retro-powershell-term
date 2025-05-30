using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace CoolRetroPowershellTerm
{
    class Program
    {
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Cool Retro Powershell Term"
            };

            using (var window = new GameWindow(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
