using System;
using System.Collections.Generic;

namespace tetris
{
    public class Tetrominoe
    {
        private int _x = 2;
        private int _y;
        private int[,] _currentHold = null;
        private int[,] _upNext = null;
        private int _rotation;

        private readonly int _maxTime = 50;
        private int _time = 0;
        
        private readonly Board _board;

        private readonly Data _data = new Data();

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
            
            // Console.SetCursorPosition(12, 2);
            // Console.Write($"x = {_x} y = {_y}");
            
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
            var holdIndex = random.Next(_data.Shapes.Count);
            var nextIndex = random.Next(_data.Shapes.Count);
            
            _currentHold = _currentHold == null ? _data.Shapes[holdIndex] : _upNext;
            _upNext = _data.Shapes[nextIndex];
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
            
            var rotation = _rotation < 3 ? _rotation + 1 : 0;
            
            if (!WallKicks(newHold, rotation)) return;

            _rotation = rotation;
            _currentHold = newHold;
        }

        bool WallKicks(int[,] peace, int rotation)
        {
            var wallKicks = peace.GetLength(0) == 4
                ? _data.WallKicksI
                : _data.WallKicks;

            for (var i = 0; i < wallKicks.GetLength(1); i++)
            {
                var x = _x + wallKicks[rotation, i, 0];
                var y = _y + wallKicks[rotation, i, 1];

                if (!_board.Collisions(peace, x, y))
                {
                    _x = x;
                    _y = y;
                    return true;
                }
                
            }

            return false;
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