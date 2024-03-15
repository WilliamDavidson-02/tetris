using System;

namespace tetris
{
    public class Game
    {
        private ScheduleTimer _timer;
        private Board _board;
        private Tetrominoe _shape;
        
        public bool GameOver { get; private set; }

        public Game()
        {
            _board = new Board(16, 10);
            _shape = new Tetrominoe(_board);
        }

        public void Start()
        {
            GameOver = false;
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
                case ConsoleKey.Spacebar:
                    _shape.Drop();
                    break;
            }
        }

        private void Tick()
        {
            if (IsGameOver()) return;
            
            _board.Draw();
            _shape.RenderHold();
            _shape.RenderNext();
            ScheduleNextTick();
        }

        private bool IsGameOver()
        {
            for (var c = 0; c < _board.Cols; c++)
            {
                if (_board.Field[0, c] == 0) continue;
                
                Stop();
                return true;
            }

            return false;
        }

        private void ScheduleNextTick()
        {
            _timer = new ScheduleTimer(20, Tick);
        }
    }
}