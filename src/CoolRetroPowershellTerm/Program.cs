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

            var buffer = new TextBuffer(25, 80);
            buffer.WriteString(0, 0, "Welcome to Cool Retro Powershell Term!");
            buffer.WriteString(1, 0, "This is a simulated terminal output.");

            using (var window = new GameWindow(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.UpdateFrame += (frame) =>
                {
                    // Simulate output: update buffer here if needed
                };
                window.RenderFrame += (frame) =>
                {
                    // Rendering will be implemented in the next task
                };
                window.Run();
            }
        }
    }
}
