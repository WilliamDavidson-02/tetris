using System;

namespace tetris
{
    public class Tetrominoe
    {
        private int _x;
        private int _y;
        private int _rotation = 0;
        private readonly Board _board;

        private readonly int[] _i = { 11, 11, 11, 11 }; // 11 = Cyan ConsoleColor

        public Tetrominoe(Board board)
        {
            _board = board;
        }

        private void Draw() 
        {
            for (var i = 0; i < _i.Length; i++)
            {
                if (_rotation == 0 || _rotation == 180)
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
                if (_rotation == 0 || _rotation == 180)
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
            var random = new Random();
            _x = random.Next(0, _board.Cols - _i.Length);
            _y = 0;
            
            Draw();
        }

        public void Fall()
        {
            if (_y == _board.Rows) return;
            
            // clear prev location
            Clear();
            
            // set new location
            _y += 1;
            Draw();
        }

        public void Rotate()
        {
            var isVertical = _rotation + 90 == 90 || _rotation + 90 == 270;
            if (_y + _i.Length >= _board.Rows && !isVertical) return;
            
            Clear();
            
            _rotation += 90;
            if (_rotation >= 360) _rotation = 0;
            _x += _rotation == 90 || _rotation == 270 ? 1 : -1;  
            
            Draw();
        }
    }
}