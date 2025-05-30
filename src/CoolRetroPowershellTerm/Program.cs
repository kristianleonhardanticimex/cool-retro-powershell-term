using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using CoolRetroPowershellTerm;

namespace CoolRetroPowershellTerm
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Info("Application starting.");
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Cool Retro Powershell Term"
            };

            var buffer = new TextBuffer(25, 80);
            buffer.WriteString(0, 0, "Welcome to Cool Retro Powershell Term!");
            buffer.WriteString(1, 0, "This is a simulated terminal output.");

            // Attempt to load font atlas (if present)
            BitmapFont? font = null;
            string fontPath = "assets/fonts/ascii_16x16.png";
            try
            {
                if (System.IO.File.Exists(fontPath))
                {
                    font = new BitmapFont(fontPath, 10, 12, 16, 16); // 10x12 cell, 16x16 grid
                    Logger.Info($"Font loaded: {fontPath}");
                }
                else
                {
                    Logger.Info($"Font not found: {fontPath}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to load font: {fontPath}", ex);
            }

            try
            {
                using (var window = new GameWindow(GameWindowSettings.Default, nativeWindowSettings))
                {
                    Logger.Info("Window created successfully.");
                    window.RenderFrame += (frame) =>
                    {
                        try
                        {
                            GL.ClearColor(0.1f, 0.1f, 0.15f, 1.0f); // Retro dark blue background
                            GL.Clear(ClearBufferMask.ColorBufferBit);
                            Logger.Info($"RenderFrame at {DateTime.Now:HH:mm:ss.fff}");
                            if (font != null)
                            {
                                // Render the text buffer using the font atlas
                                for (int row = 0; row < buffer.Rows; row++)
                                {
                                    for (int col = 0; col < buffer.Cols; col++)
                                    {
                                        var entry = buffer.Buffer[row, col];
                                        if (entry.Value != ' ')
                                        {
                                            int charCode = (int)entry.Value;
                                            int gridX = charCode % font.GridCols;
                                            int gridY = charCode / font.GridCols;
                                            float u0 = gridX / (float)font.GridCols;
                                            float v0 = gridY / (float)font.GridRows;
                                            float u1 = (gridX + 1) / (float)font.GridCols;
                                            float v1 = (gridY + 1) / (float)font.GridRows;
                                            float x = col * font.GlyphWidth;
                                            float y = row * font.GlyphHeight;
                                            // TODO: Draw textured quad at (x, y) with UV (u0,v0)-(u1,v1)
                                        }
                                    }
                                }
                            }
                            // Swap the front and back buffers to present the frame
                            window.SwapBuffers();
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Error during rendering.", ex);
                        }
                    };
                    window.Run();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Windowing or render loop error.", ex);
            }
            Logger.Info("Application exiting.");
        }
    }
}
