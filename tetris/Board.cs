using System;

namespace tetris
{
    public class Board
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public readonly int[,] Field;
        public bool IsClearingRow { get; set; }

        public Board(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Field = new int[rows, cols];
            IsClearingRow = false;
        }

        public void FindFilledRow()
        {
            for (var r = 0; r < Rows; r++)
            {
                var isRowFilled = true;
                for (var c = 0; c < Cols; c++)
                {
                    if (Field[r, c] == 0)
                    {
                        isRowFilled = false;
                        break;
                    }
                }

                if (isRowFilled)
                {
                    ClearRow(r);
                }
            }
        }

        private void ClearRow(int row)
        {
            IsClearingRow = true;
            Console.SetCursorPosition(15, 5);
            Console.Write($"row: {row}");

            for (var r = row; r > 0; r--)
            {
                for (var c = 0; c < Cols; c++)
                {
                    var upperCell = Field[r - 1, c];
                    Field[r - 1, c] = 0;

                    Field[r, c] = upperCell;
                }
            }
            
            IsClearingRow = false;
            Draw();
        }
        
        public void Draw()
        {
            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Cols; c++)
                {
                    Console.ForegroundColor = (ConsoleColor)Field[r, c];
                    Console.SetCursorPosition(c, r);
                    Console.Write(Field[r, c]);
                    Console.ResetColor();
                }
            }
        }
    }
}