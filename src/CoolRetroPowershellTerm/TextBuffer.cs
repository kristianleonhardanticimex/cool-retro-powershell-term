using System;

namespace CoolRetroPowershellTerm
{
    public struct CharEntry
    {
        public char Value;
        public float TimeWritten;
        public bool IsNewlyWritten;
    }

    public class TextBuffer
    {
        private readonly int rows;
        private readonly int cols;
        private readonly CharEntry[,] buffer;

        public int Rows => rows;
        public int Cols => cols;
        public CharEntry[,] Buffer => buffer;

        public TextBuffer(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            buffer = new CharEntry[rows, cols];
            Clear();
        }

        public void WriteChar(int row, int col, char value)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols) return;
            buffer[row, col] = new CharEntry
            {
                Value = value,
                TimeWritten = (float)DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                IsNewlyWritten = true
            };
        }

        public void WriteString(int row, int col, string value)
        {
            for (int i = 0; i < value.Length && (col + i) < cols; i++)
            {
                WriteChar(row, col + i, value[i]);
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
