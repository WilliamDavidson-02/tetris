using System;

namespace tetris
{
    public class Game
    {
        private ScheduleTimer _timer;
        private readonly Board _board;
        private readonly Tetrominoe _shape;
        private readonly int _interval;
        
        public bool GameOver { get; private set; }

        public Game()
        {
            _board = new Board(16, 10);
            _shape = new Tetrominoe(_board);
            _interval = 500;
        }

        public void Start()
        {
            ScheduleNextTick();
        }

        public void Stop()
        {
            Console.Clear();
            GameOver = true;
        }

        public void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _shape.Rotate();
                    break;
                case ConsoleKey.LeftArrow:
                    _shape.Left();
                    break;
                case ConsoleKey.RightArrow:
                    _shape.Right();
                    break;
                case ConsoleKey.DownArrow:
                    _shape.Fall();
                    break;
            }
        }

        private void Tick()
        {
            _board.FindFilledRow();
            
            if (!_board.IsClearingRow)
            {
                if (!_shape.IsSpawned) _shape.Spawn();

                _shape.Fall();
            }
            
            _board.Draw();
            ScheduleNextTick();
        }

        private void ScheduleNextTick()
        {
            _timer = new ScheduleTimer(_interval, Tick);
        }
    }
}