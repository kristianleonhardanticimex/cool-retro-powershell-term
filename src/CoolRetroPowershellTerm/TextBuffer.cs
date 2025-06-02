using System;

namespace CoolRetroPowershellTerm
{
    public struct CharEntry
    {
        public char Value;
        public float TimeWritten;
        public bool IsNewlyWritten;
        public float BgR, BgG, BgB, BgA; // Background color (RGBA)
    }

    public class TextBuffer
    {
        private readonly int rows;
        private readonly int cols;
        private readonly CharEntry[,] buffer;

        public int Rows => rows;
        public int Cols => cols;
        public CharEntry[,] Buffer => buffer;

        public int CursorRow { get; set; } = 0;
        public int CursorCol { get; set; } = 0;
        public bool CursorVisible { get; set; } = true;
        public bool CursorBlink { get; set; } = true;
        public float CursorBlinkRate { get; set; } = 1.0f; // Hz
        public float CursorBlinkTimer { get; set; } = 0.0f;

        public TextBuffer(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            buffer = new CharEntry[rows, cols];
            Clear();
        }

        public void WriteChar(int row, int col, char value, float bgR = 0.1f, float bgG = 0.1f, float bgB = 0.15f, float bgA = 1.0f)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols) return;
            buffer[row, col] = new CharEntry
            {
                Value = value,
                TimeWritten = (float)DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                IsNewlyWritten = true,
                BgR = bgR,
                BgG = bgG,
                BgB = bgB,
                BgA = bgA
            };
        }

        public void WriteString(int row, int col, string value, float bgR = 0.1f, float bgG = 0.1f, float bgB = 0.15f, float bgA = 1.0f)
        {
            for (int i = 0; i < value.Length && (col + i) < cols; i++)
            {
                WriteChar(row, col + i, value[i], bgR, bgG, bgB, bgA);
            }
        }

        public void Clear()
        {
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    buffer[r, c] = new CharEntry { Value = ' ', TimeWritten = 0, IsNewlyWritten = false };
        }
    }
}
