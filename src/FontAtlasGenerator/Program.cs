using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.IO;

namespace FontAtlasGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use absolute path for font and output
            string fontPath = Path.GetFullPath("../../assets/fonts/LightChop-7x9.ttf");
            string outputPath = Path.GetFullPath("../../assets/fonts/ascii_16x16.png");
            int glyphWidth = 7;
            int glyphHeight = 9;
            int gridCols = 16;
            int gridRows = 16;
            int cellWidth = 10; // Add padding for alignment
            int cellHeight = 12;
            int imageWidth = gridCols * cellWidth;
            int imageHeight = gridRows * cellHeight;

            using var bmp = new Bitmap(imageWidth, imageHeight);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(fontPath);
            using var font = new Font(pfc.Families[0], glyphHeight, FontStyle.Regular, GraphicsUnit.Pixel);
            using var brush = new SolidBrush(Color.White);

            for (int i = 0; i < 256; i++)
            {
                int row = i / gridCols;
                int col = i % gridCols;
                float x = col * cellWidth;
                float y = row * cellHeight;
                string ch = ((char)i).ToString();
                g.DrawString(ch, font, brush, x, y);
            }

            bmp.Save(outputPath, ImageFormat.Png);
            Console.WriteLine($"Font atlas generated: {outputPath}");
        }
    }
}
