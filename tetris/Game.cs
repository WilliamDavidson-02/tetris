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
            _shape.NewShape();
            ScheduleNextTick();
        }

        private void Stop()
        {
            Console.Clear();
            GameOver = true;
        }

        public void Input()
        {
            if (!Console.KeyAvailable) return;
            
            var input = Console.ReadKey(true);

            switch (input.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    _shape.Rotate();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    _shape.Right();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    _shape.Fall();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    _shape.Left();
                    break;
                case ConsoleKey.Escape:
                    Stop();
                    break;
                case ConsoleKey.R:
                    // Restart
                    break;
                case ConsoleKey.Spacebar:
                    _shape.Drop();
                    break;
            }
        }

        private void Tick()
        {
            _board.Draw();
            _shape.RenderHold();
            _shape.RenderNext();
            ScheduleNextTick();
        }

        private void ScheduleNextTick()
        {
            _timer = new ScheduleTimer(20, Tick);
        }
    }
}