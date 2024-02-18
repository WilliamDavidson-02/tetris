using System;

namespace tetris
{
    public class Game
    {
        private ScheduleTimer _timer;
        private readonly Board _board;
        private readonly Tetrominoe _shape;
        
        public bool GameOver { get; private set; }

        public Game()
        {
            _board = new Board(16, 10);
            _shape = new Tetrominoe(_board);
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
            }
        }

        private void Tick()
        {
            if (!_shape.IsSpawned) _shape.Spawn();
            
            _shape.Fall();
            
            _board.Draw();
            ScheduleNextTick();
        }

        private void ScheduleNextTick()
        {
            _timer = new ScheduleTimer(500, Tick);
        }
    }
}