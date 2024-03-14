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
                    HandleStopPos(_currentHold, _x, _y);
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
                    Console.Write(_board.Data.GetLetterRepresentation(_currentHold[r, c]));
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
                    Console.Write(_board.Data.GetLetterRepresentation(_upNext[r, c]));
                    Console.ResetColor();
                }
            }
        }

        public void ClearNextSpot(int[,] peace, int left, int top)
        {
            for (var r = 0; r < peace.GetLength(0); r++)
            {
                for (var c = 0; c < peace.GetLength(1); c++)
                {
                    Console.SetCursorPosition(left + c, top + r);
                    Console.Write(' ');
                }
            }
        }

        public void NewShape()
        {
            var random = new Random();
            var holdIndex = random.Next(_board.Data.Shapes.Count);
            var nextIndex = random.Next(_board.Data.Shapes.Count);

            if (_currentHold != null) ClearNextSpot(_upNext, 12, 5);
            
            _currentHold = _currentHold == null ? _board.Data.Shapes[holdIndex] : _upNext;
            _upNext = _board.Data.Shapes[nextIndex];

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

        private bool WallKicks(int[,] peace, int rotation)
        {
            var wallKicks = peace.GetLength(0) == 4
                ? _board.Data.WallKicksI
                : _board.Data.WallKicks;

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
        
        private void HandleStopPos(int[,] peace, int x, int y)
        {
            // Move Tetrominoe to field
            for (var r = 0; r < peace.GetLength(0); r++)
            {
                for (var c = 0; c < peace.GetLength(1); c++)
                {
                    if (peace[r, c] == 0) continue;
                    
                    _board.Field[y + r, x + c] = peace[r, c];
                }
            }
            
            // reset values
            _x = 2;
            _y = 0;
            _rotation = 0;
            
            NewShape();
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