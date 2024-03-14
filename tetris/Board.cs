using System;

namespace tetris
{
    public class Board
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int[,] Field { get; set; }

        public readonly Data Data = new Data();
        
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
                    var cell = Field[r, c] == 0
                        ? '#'
                        : Data.GetLetterRepresentation(Field[r, c]);
                    
                    Console.ForegroundColor = (ConsoleColor)Field[r, c];
                    Console.SetCursorPosition(c, r);
                    Console.Write(cell);
                }
            }
            Console.ResetColor();
        }
        
        public bool Collisions(int[,] peace, int x, int y)
        {
            for (var r = 0; r < peace.GetLength(0); r++)
            {
                for (var c = 0; c < peace.GetLength(1); c++)
                {
                    if (peace[r, c] == 0) continue;
                    
                    // Check if out of bounds
                    if (!CheckBounds(y + r, x + c)) return true;
                    
                    // Check if a cell is already taken 
                    if (Field[y + r, x + c] != 0) return true;
                }
            }
            
            return false;
        }

        private bool CheckBounds(int y, int x)
        {
            if (x < 0 || x >= Cols) return false;

            if (y >= Rows) return false;
            
            return true;
        }
    }
}