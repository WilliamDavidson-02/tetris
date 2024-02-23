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

        private readonly int[] _i = { 1, 1, 1, 1 };

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
            
            CheckStopPos();
            if (!IsSpawned) return;
            
            Clear();
            
            _y += 1;
            Draw();
        }

        public void Rotate()
        {
            if (_y + _i.Length >= _board.Rows || !IsSpawned) return;
            
            // Check if rotation is going to cause collision
            if (!IsVertical())
            {
                for (var i = 1; i < _i.Length; i++)
                {
                    if (_board.Field[_y + i, _x + 1] != 0) return;
                }
            }
            else
            {
                if (_x != 0 && _board.Field[_y, _x - 1] != 0) return;
                var rightLength = _x >= _board.Cols - _i.Length ? _board.Cols - 1 - _x : _i.Length; 
                for (var i = 1; i < rightLength; i++)
                {
                    if (_board.Field[_y, _x + i] != 0) return;
                }
            }
            
            Clear();
            
            _rotation += 90;
            if (_rotation >= 360) _rotation = 0;
            _x += IsVertical() ? 1 : -1;
            if (_x < 0) _x = 0;
            if (!IsVertical() && _x > _board.Cols - _i.Length) _x = _board.Cols - _i.Length; 
            
            Draw();
        }

        public void Left()
        {
            if (_x == 0 || HasNeighbour(_x - 1)) return;
            CheckStopPos();
            if (!IsSpawned) return;
            
            Clear();
            _x -= 1;
            Draw();
        }
        
        public void Right()
        {
            if (_x + 1 > _board.Cols - 1 && IsVertical()) return;
            if (_x + _i.Length > _board.Cols - 1 && !IsVertical()) return;
            CheckStopPos();
            if (!IsSpawned) return;
            if (HasNeighbour(IsVertical() ? _x + 1 : _x + _i.Length)) return;
            
            Clear();
            _x += 1;
            Draw();
        }

        private void CheckStopPos()
        {
            var width = IsVertical() ? 1 : _i.Length;
            var stepDown = IsVertical() ? _y + _i.Length : _y + 1;

            if (stepDown > _board.Rows - 1)
            {
                IsSpawned = false;
                return;
            }
            
            for (var c = 0; c < width; c++)
            {
                if (_board.Field[stepDown, c + _x] != 0)
                {
                    IsSpawned = false;
                }
            }
        }

        private bool HasNeighbour(int neighbourPos)
        {
            var height = IsVertical() ? _i.Length : 1;

            for (var i = 0; i < height; i++)
            {
                if (_board.Field[_y + i, neighbourPos] != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsVertical()
        {
            return _rotation == 90 || _rotation == 270;
        }
    }
}