using System;

namespace tetris
{
    public class Board
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int[,] Field { get; set; }
        public readonly Data Data = new Data();
        private int _clearRows;
        
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
        
        public int ClearLines(int maxTime)
        {
            var row = Rows - 1;

            while (row >= 0)
            {
                if (IsRowFull(row))
                {
                    ClearLine(row);
                    
                    maxTime = AddPoints(maxTime);

                    MoveRowsDown(row);
                }
                else
                {
                    row--;
                }
            }

            return maxTime;
        }

        private void MoveRowsDown(int row)
        {
            for (var r = row; r > 0; r--)
            {
                for (var c = 0; c < Cols; c++)
                {
                    Field[r, c] = Field[r - 1, c];
                }
                
                ClearLine(r - 1);
            }
        }

        private void ClearLine(int row)
        {
            for (var c = 0; c < Cols; c++)
            {
                Field[row, c] = 0;
            }
        }

        private bool IsRowFull(int row)
        {
            for (var c = 0; c < Cols; c++)
            {
                if (Field[row, c] == 0) return false;
            }
            
            return true;
        }

        private int AddPoints(int maxTime)
        {
            _clearRows += 1;

            if (_clearRows < 5) return maxTime;
            
            _clearRows = 0;
            return maxTime - 5 >= 0 ? maxTime - 5 : 0;
        }
    }
}