// Placeholder for a bitmap font atlas loader and renderer.
// Place your ASCII grid font PNG in /assets/fonts/ (e.g., ascii_16x16.png)

using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace CoolRetroPowershellTerm
{
    public class BitmapFont
    {
        public int TextureId { get; private set; }
        public int GlyphWidth { get; }
        public int GlyphHeight { get; }
        public int GridCols { get; }
        public int GridRows { get; }

        public BitmapFont(string path, int glyphWidth, int glyphHeight, int gridCols, int gridRows)
        {
            GlyphWidth = glyphWidth;
            GlyphHeight = glyphHeight;
            GridCols = gridCols;
            GridRows = gridRows;
            LoadTexture(path);
        }

        private void LoadTexture(string path)
        {
            using var stream = File.OpenRead(path);
            var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            TextureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureId);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
