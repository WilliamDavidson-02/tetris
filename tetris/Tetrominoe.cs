using System;
using System.Collections.Generic;

namespace tetris
{
    public class Tetrominoe
    {
        private int _x = 2;
        private int _y;
        private int _rotation = 1;
        private int[,] _currentHold = null;
        private int[,] _upNext = null;

        private readonly int _maxTime = 50;
        private int _time = 0;
        
        private readonly Board _board;

        // Shapes
        private static readonly int[,] I =
        {
            { 0, 0, 0, 0 },
            { 11, 11, 11, 11 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        private static readonly int[,] J =
        {
            { 0, 0, 0 },
            { 1, 1, 1 }, 
            { 0, 0, 1 },
        };
        private static readonly int[,] L =
        {
            { 0, 0, 0 },
            { 6, 6, 6 }, 
            { 6, 0, 0 }
        };
        private static readonly int[,] O =
        {
            { 14, 14 }, 
            { 14, 14 }
        };
        private static readonly int[,] S =
        {
            { 0, 2, 2 }, 
            { 2, 2, 0 },

        };
        private static readonly int[,] T =
        {
            { 0, 0, 0 },
            { 5, 5, 5 }, 
            { 0, 5, 0 }
        };
        private static readonly int[,] Z =
        {
            { 10, 10, 0 }, 
            { 0, 10, 10 },
        };

        private static readonly List<int[,]> Shapes = new List<int[,]>() { I, J, L, O, S, T, Z }; 

        public Tetrominoe(Board board)
        {
            _board = board;
        }

        public void RenderHold() 
        {
            if (_time >= _maxTime)
            {
                if (!_board.Collisions(_currentHold, _x, _y + 1))
                {
                    // Make shape fall down
                    _y++;
                }
                else
                {
                    // Move peace to board and get a new peace
                }
                _time = 0;
            }

            _time++;
            
            for (var r = 0; r < _currentHold.GetLength(0); r++)
            {
                for (var c = 0; c < _currentHold.GetLength(1); c++)
                {
                    if (_currentHold[r,c] == 0) continue;
                    
                    Console.ForegroundColor = (ConsoleColor)_currentHold[r, c];
                    Console.SetCursorPosition(_x + c, _y + r);
                    Console.Write(GetLetterRepresentation(_currentHold[r, c]));
                    Console.ResetColor();
                }
            }
        }
        
        public void RenderNext() 
        {
            for (var r = 0; r < _upNext.GetLength(0); r++)
            {
                for (var c = 0; c < _upNext.GetLength(1); c++)
                {
                    
                    if (_upNext[r,c] == 0) continue;
                    
                    Console.ForegroundColor = (ConsoleColor)_upNext[r, c];
                    Console.SetCursorPosition(12 + c, 5 + r);
                    Console.Write(GetLetterRepresentation(_upNext[r, c]));
                    Console.ResetColor();
                }
            }
        }

        private string GetLetterRepresentation(int code)
        {
            var letter = "";
            
            switch (code)
            {   
                case 11:
                    letter = "I";
                    break;
                case 1:
                    letter = "J";
                    break;
                case 6:
                    letter = "L";
                    break;
                case 14:
                    letter = "O";
                    break;
                case 2:
                    letter = "S";
                    break;
                case 5:
                    letter = "T";
                    break;
                case 10:
                    letter = "Z";
                    break;
            }

            return letter;
        }

        public void NewShape()
        {
            var random = new Random();
            
            _currentHold = _currentHold == null ? Shapes[random.Next(Shapes.Count)] : _upNext;
            _upNext = Shapes[random.Next(Shapes.Count)];
        }

        public void Fall()
        {

        }

        public void Rotate()
        {
            var height = _currentHold.GetLength(0);
            var width = _currentHold.GetLength(1);

            var newHold = new int[width, height]; 

            for (var r = 0; r < height; r++)
            {
                for (var c = 0; c < width; c++)
                {
                    newHold[c, height - r - 1] = _currentHold[r, c];
                }
            }

            _currentHold = newHold;
        }

        public void Left()
        {
            if (!_board.Collisions(_currentHold, _x - 1, _y)) _x--;
        }
        
        public void Right()
        {
            if (!_board.Collisions(_currentHold, _x + 1, _y)) _x++;
        }
    }
}