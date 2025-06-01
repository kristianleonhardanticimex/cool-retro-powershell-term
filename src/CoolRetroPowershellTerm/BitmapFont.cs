// Placeholder for a bitmap font atlas loader and renderer.
// Place your ASCII grid font PNG in /assets/fonts/ (e.g., ascii_16x16.png)

using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using StbTrueTypeSharp;
using StbImageSharp;
using StbImageWriteSharp;

namespace CoolRetroPowershellTerm
{
    public class BitmapFont
    {
        public int TextureId { get; private set; }
        public int GlyphWidth { get; }
        public int GlyphHeight { get; }
        public int GridCols { get; }
        public int GridRows { get; }

        public BitmapFont(string ttfPath, int glyphWidth, int glyphHeight, int gridCols, int gridRows)
        {
            GlyphWidth = glyphWidth;
            GlyphHeight = glyphHeight;
            GridCols = gridCols;
            GridRows = gridRows;
            GenerateAtlasFromTTF(ttfPath);
        }

        private unsafe void GenerateAtlasFromTTF(string ttfPath)
        {
            byte[] ttf = File.ReadAllBytes(ttfPath);
            fixed (byte* ttfPtr = ttf)
            {
                var fontInfo = new StbTrueType.stbtt_fontinfo();
                StbTrueType.stbtt_InitFont(fontInfo, ttfPtr, 0);
                int atlasWidth = GlyphWidth * GridCols;
                int atlasHeight = GlyphHeight * GridRows;
                byte[] atlas = new byte[atlasWidth * atlasHeight];
                for (int i = 0; i < GridCols * GridRows; i++)
                {
                    int ch = i + 32; // ASCII 32 offset
                    int col = i % GridCols;
                    int row = i / GridCols;
                    int x = col * GlyphWidth;
                    int y = row * GlyphHeight;
                    float scale = StbTrueType.stbtt_ScaleForPixelHeight(fontInfo, GlyphHeight);
                    int gw = 0, gh = 0, gx = 0, gy = 0;
                    byte* glyphBitmap = StbTrueType.stbtt_GetCodepointBitmap(fontInfo, 0, scale, ch, &gw, &gh, &gx, &gy);
                    for (int yy = 0; yy < gh; yy++)
                    for (int xx = 0; xx < gw; xx++)
                    {
                        int srcIdx = yy * gw + xx;
                        int dstIdx = (y + yy) * atlasWidth + (x + xx);
                        if (dstIdx < atlas.Length)
                            atlas[dstIdx] = glyphBitmap[srcIdx];
                    }
                    StbTrueType.stbtt_FreeBitmap(glyphBitmap, null);
                }
                // Convert to RGBA (white text, alpha from glyph)
                byte[] rgba = new byte[atlasWidth * atlasHeight * 4];
                for (int i = 0; i < atlas.Length; i++)
                {
                    byte a = atlas[i];
                    rgba[i * 4 + 0] = 255;
                    rgba[i * 4 + 1] = 255;
                    rgba[i * 4 + 2] = 255;
                    rgba[i * 4 + 3] = a;
                }
                // Save debug PNG for inspection
                SaveRgbaPngDebug(rgba, atlasWidth, atlasHeight);
                TextureId = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, TextureId);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, atlasWidth, atlasHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, rgba);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
        }

        private void SaveRgbaPngDebug(byte[] rgba, int width, int height)
        {
            // Save as a PNG using System.Drawing (Windows-only, for debug)
            try
            {
                string debugPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "font-atlas-debug.png");
                using (var bmp = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    var data = bmp.LockBits(
                        new System.Drawing.Rectangle(0, 0, width, height),
                        System.Drawing.Imaging.ImageLockMode.WriteOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    System.Runtime.InteropServices.Marshal.Copy(rgba, 0, data.Scan0, rgba.Length);
                    bmp.UnlockBits(data);
                    bmp.Save(debugPath, System.Drawing.Imaging.ImageFormat.Png);
                }
                Console.WriteLine($"[BitmapFont] Font atlas debug PNG saved to: {debugPath}");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to save font atlas debug PNG", ex);
            }
        }
    }
}
