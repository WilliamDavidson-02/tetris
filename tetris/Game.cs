using System;

namespace tetris
{
    public class Game
    {
        private ScheduleTimer _timer;
        private readonly Board _board = new Board(16, 10);
        private Tetrominoe _shape;
        
        public bool GameOver { get; private set; }

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
            }
        }

        private void Tick()
        {
            _board.Draw();
            if (_shape == null)
            {
                _shape = new Tetrominoe(_board);
                _shape.Spawn();
            }
            else
            {
                _shape.Fall();
            }
            ScheduleNextTick();
        }

        private void ScheduleNextTick()
        {
            _timer = new ScheduleTimer(1000, Tick);
        }
    }
}