using System;

namespace tetris
{
    public class Tetrominoe
    {
        private int _x;
        private int _y;
        public int Rotation { get; set; }

        private readonly int[] _i = { 1, 1, 1, 1 };

        public void Spawn(Board board)
        {
            var random = new Random();
            _x = random.Next(0, board.Cols - _i.Length);
            _y = 0;

            for (var i = 0; i < _i.Length; i++)
            {
                board.Field[_y, _x + i] = _i[i];
            }
        }

        public void Fall(Board board)
        {
            if (_y == board.Rows) return;
            
            // clear prev location
            for (var i = 0; i < _i.Length; i++)
            {
                board.Field[_y, _x + i] = 0;
            }
            
            // set new location
            _y += 1;
            for (var i = 0; i < _i.Length; i++)
            {
                board.Field[_y, _x + i] = _i[i];
            }
        }
    }
}