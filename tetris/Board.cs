using System;

namespace tetris
{
    public class Board
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public readonly int[,] Field;

        public Board(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Field = new int[rows, cols];
        }
        public void Draw()
        {
            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Cols; c++)
                {
                    Console.ForegroundColor = (ConsoleColor)Field[r, c];
                    Console.SetCursorPosition(c, r);
                    Console.Write('*');
                    Console.ResetColor();
                }
            }
        }
    }
}