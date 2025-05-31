using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using CoolRetroPowershellTerm;

namespace CoolRetroPowershellTerm
{
    class Program
    {
        static int shaderProgram = -1;
        static int vao = -1;
        static int vbo = -1;
        static int uMvpLocation = -1;
        static int uColorLocation = -1;
        static int uUseTextureLocation = -1;
        static Matrix4 ortho;
        static BitmapFont? font = null;
        static string fontPath = "assets/fonts/ascii_16x16.png";

        static void Main(string[] args)
        {
            // Adjust buffer and window size to fit glyphs
            int cols = 80;
            int rows = 25;
            int glyphW = 20; // doubled from 10
            int glyphH = 24; // doubled from 12
            // Resolve font path relative to executable
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string resolvedFontPath = System.IO.Path.Combine(exeDir, "assets", "fonts", "ascii_16x16.png");
            Logger.Info($"Resolved font path: {resolvedFontPath}");
            fontPath = resolvedFontPath;
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(cols * glyphW, rows * glyphH),
                Title = "Cool Retro Powershell Term"
            };

            var buffer = new TextBuffer(rows, cols);
            buffer.WriteString(0, 0, "Welcome to Cool Retro Powershell Term!");
            buffer.WriteString(1, 0, "This is a simulated terminal output.");
            // Move the welcome message to the top row
            // ...existing code...
            try
            {
                using (var window = new GameWindow(GameWindowSettings.Default, nativeWindowSettings))
                {
                    window.Load += () =>
                    {
                        // Set up orthographic projection
                        ortho = Matrix4.CreateOrthographicOffCenter(0, window.Size.X, window.Size.Y, 0, -1, 1);
                        // Compile shaders and set up VAO/VBO
                        shaderProgram = CreateShaderProgram();
                        vao = GL.GenVertexArray();
                        vbo = GL.GenBuffer();
                        GL.BindVertexArray(vao);
                        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                        GL.BufferData(BufferTarget.ArrayBuffer, 6 * 4 * sizeof(float), IntPtr.Zero, BufferUsageHint.StreamDraw);
                        GL.EnableVertexAttribArray(0); // pos
                        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
                        GL.EnableVertexAttribArray(1); // uv
                        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
                        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                        GL.BindVertexArray(0);
                        uMvpLocation = GL.GetUniformLocation(shaderProgram, "uMVP");
                        uColorLocation = GL.GetUniformLocation(shaderProgram, "uColor");
                        uUseTextureLocation = GL.GetUniformLocation(shaderProgram, "uUseTexture");
                        // Font must be loaded after OpenGL context is ready
                        try {
                            Logger.Info($"Checking font file at: {fontPath}");
                            if (System.IO.File.Exists(fontPath))
                            {
                                font = new BitmapFont(fontPath, glyphW, glyphH, 16, 16);
                                Logger.Info($"Font loaded successfully: {fontPath}");
                                // Set font texture filtering to NEAREST for pixel-perfect rendering
                                GL.BindTexture(TextureTarget.Texture2D, font.TextureId);
                                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                                GL.BindTexture(TextureTarget.Texture2D, 0);
                            }
                            else
                            {
                                Logger.Error($"Font not found: {fontPath}");
                                Environment.Exit(1);
                            }
                        } catch (Exception ex) {
                            Logger.Error($"Failed to load font: {fontPath}", ex);
                            Environment.Exit(1);
                        }
                    };
                    window.RenderFrame += (frame) =>
                    {
                        try
                        {
                            GL.ClearColor(0.1f, 0.1f, 0.15f, 1.0f);
                            GL.Clear(ClearBufferMask.ColorBufferBit);
                            if (font != null)
                            {
                                GL.UseProgram(shaderProgram);
                                GL.BindVertexArray(vao);
                                GL.UniformMatrix4(uMvpLocation, false, ref ortho);
                                // Set texture uniform to texture unit 0
                                int texLocation = GL.GetUniformLocation(shaderProgram, "tex");
                                if (texLocation != -1)
                                    GL.Uniform1(texLocation, 0);
                                int marginTop = 12; // Add a top margin in pixels (half a glyph height)
                                for (int row = 0; row < buffer.Rows; row++)
                                {
                                    for (int col = 0; col < buffer.Cols; col++)
                                    {
                                        var entry = buffer.Buffer[row, col];
                                        float x = col * font.GlyphWidth;
                                        float y = marginTop + row * font.GlyphHeight; // Add margin to y
                                        // Draw background quad with dark blue color (matches ClearColor)
                                        float[] bgColor = new float[] { 0.1f, 0.1f, 0.15f, 1.0f }; // dark blue
                                        GL.Uniform4(uColorLocation, 1, bgColor);
                                        GL.Uniform1(uUseTextureLocation, 0);
                                        DrawQuad(x, y, font.GlyphWidth, font.GlyphHeight, 0, 0, 1, 1);
                                        if (entry.Value != ' ')
                                        {
                                            int charCode = (int)entry.Value;
                                            int gridX = charCode % font.GridCols;
                                            int gridY = charCode / font.GridCols;
                                            float u0 = gridX / (float)font.GridCols;
                                            float v0 = gridY / (float)font.GridRows;
                                            float u1 = (gridX + 1) / (float)font.GridCols;
                                            float v1 = (gridY + 1) / (float)font.GridRows;
                                            GL.ActiveTexture(TextureUnit.Texture0);
                                            GL.BindTexture(TextureTarget.Texture2D, font.TextureId);
                                            float[] fgColor = new float[] { 0.8f, 1.0f, 0.8f, 1.0f };
                                            GL.Uniform4(uColorLocation, 1, fgColor);
                                            GL.Uniform1(uUseTextureLocation, 1);
                                            DrawQuad(x, y, font.GlyphWidth, font.GlyphHeight, u0, v0, u1, v1);
                                        }
                                    }
                                }
                                GL.BindTexture(TextureTarget.Texture2D, 0);
                                GL.BindVertexArray(0);
                                GL.UseProgram(0);
                            }
                            else
                            {
                                // Draw a message if font failed to load
                                // Optionally: render a colored background or error
                            }
                            window.SwapBuffers();
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Error during rendering.", ex);
                            Environment.Exit(1);
                        }
                    };
                    window.Run();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Windowing or render loop error.", ex);
                Environment.Exit(1);
            }
        }

        // Draw a quad using two triangles (6 vertices), with UVs
        static void DrawQuad(float x, float y, float w, float h, float u0, float v0, float u1, float v1)
        {
            float[] vertices = new float[]
            {
                // x, y, u, v
                x, y, u0, v0, // top-left
                x + w, y, u1, v0, // top-right
                x + w, y + h, u1, v1, // bottom-right
                x, y, u0, v0, // top-left
                x + w, y + h, u1, v1, // bottom-right
                x, y + h, u0, v1 // bottom-left
            };
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Length * sizeof(float), vertices);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        // Shader setup
        static int CreateShaderProgram()
        {
            // Use real newlines in shader source
            string vert = @"#version 330 core
layout(location = 0) in vec2 inPos;
layout(location = 1) in vec2 inUV;
uniform mat4 uMVP;
out vec2 vUV;
void main() {
    gl_Position = uMVP * vec4(inPos, 0, 1);
    vUV = inUV;
}";
            string frag = @"#version 330 core
in vec2 vUV;
uniform sampler2D tex;
uniform vec4 uColor;
uniform int uUseTexture;
out vec4 outColor;
void main() {
    if (uUseTexture == 1) {
        vec4 texColor = texture(tex, vUV);
        outColor = texColor * uColor;
    } else {
        outColor = uColor;
    }
}";
            int vs = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vs, vert);
            GL.CompileShader(vs);
            int fs = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fs, frag);
            GL.CompileShader(fs);
            int prog = GL.CreateProgram();
            GL.AttachShader(prog, vs);
            GL.AttachShader(prog, fs);
            GL.LinkProgram(prog);
            GL.DeleteShader(vs);
            GL.DeleteShader(fs);
            return prog;
        }
    }
}
