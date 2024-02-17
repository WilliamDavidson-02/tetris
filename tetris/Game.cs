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

        public void Input(ConsoleKey key) {}

        private void Tick()
        {
            _board.Draw();
            if (_shape == null)
            {
                _shape = new Tetrominoe();
                _shape.Spawn(_board);
            }
            else
            {
                _shape.Fall(_board);
            }
            ScheduleNextTick();
        }

        private void ScheduleNextTick()
        {
            _timer = new ScheduleTimer(500, Tick);
        }
    }
}