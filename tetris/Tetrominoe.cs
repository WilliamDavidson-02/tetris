using System;

namespace tetris
{
    public class Tetrominoe
    {
        private int _x;
        private int _y;
        private int _rotation;
        private readonly Board _board;
        public bool IsSpawned = false;

        private readonly int[] _i = { 11, 11, 11, 11 }; // 11 = Cyan ConsoleColor

        public Tetrominoe(Board board)
        {
            _board = board;
        }

        private void Draw() 
        {
            for (var i = 0; i < _i.Length; i++)
            {
                if (!IsVertical())
                {
                    _board.Field[_y, _x + i] = _i[i];
                }
                else
                {
                    _board.Field[_y + i, _x] = _i[i];
                }
            }
        }
        
        private void Clear() 
        {
            for (var i = 0; i < _i.Length; i++)
            {
                if (!IsVertical())
                {
                    _board.Field[_y, _x + i] = 0;
                }
                else
                {
                    _board.Field[_y + i, _x] = 0;
                }
            }
        }

        public void Spawn()
        {
            IsSpawned = true;
            
            var random = new Random();
            _x = random.Next(0, _board.Cols - _i.Length - 1);
            _y = 0;
            _rotation = 0;
            
            Draw();
        }

        public void Fall()
        {
            Console.SetCursorPosition(15, 10);
            Console.Write($"Y = {_y}, X = {_x}");
            
            Console.SetCursorPosition(15, 11);
            Console.Write($"Rows = {_board.Rows}, Cols = {_board.Cols}");
            
            if (_y == _board.Rows - 1 || _y + _i.Length == _board.Rows && IsVertical())
            {
                IsSpawned = false;
                return;
            }
            
            if (!IsSpawned) return;
            
            // clear prev location
            Clear();
            
            // set new location
            _y += 1;
            Draw();
        }

        public void Rotate()
        {
            if (_y + _i.Length >= _board.Rows) return;
            
            Clear();
            
            _rotation += 90;
            if (_rotation >= 360) _rotation = 0;
            _x += IsVertical() ? 1 : -1;  
            
            Draw();
        }

        public void Left()
        {
            Clear();
            
            _x -= 1;
            if (_x < 0) _x = 0;
            
            Draw();
        }
        
        public void Right()
        {
            Clear();
            
            _x += 1;
            if (_x > _board.Cols - 1 && IsVertical()) _x = _board.Cols - 1;
            if (_x + _i.Length > _board.Cols - 1 && !IsVertical()) _x = _board.Cols - _i.Length;
            
            Draw();
        }

        private bool IsVertical()
        {
            return _rotation == 90 || _rotation == 270;
        }
    }
}