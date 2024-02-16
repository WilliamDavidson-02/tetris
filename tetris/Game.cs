using System;
using System.Security.Cryptography;

namespace tetris
{
    public class Game
    {
        private ScheduleTimer _timer;
        
        public bool Paused { get; private set; }
        public bool GameOver { get; private set; }

        public void Start()
        {
            Console.WriteLine("Start");
            ScheduleNextTick();
        }

        public void Pause()
        {
            Console.WriteLine("Pause");
            Paused = true;
            _timer.Pause();
        }

        public void Resume()
        {
            Console.WriteLine("Resume");
            Paused = false;
            _timer.Resume();
        }

        public void Stop()
        {
            Console.WriteLine("Stop");
            GameOver = true;
        }

        public void Input(ConsoleKey key)
        {
            Console.WriteLine($"Player pressed key: {key}");
        }

        private void Tick()
        {
            Console.WriteLine("Tick");
            ScheduleNextTick();
        }

        private void ScheduleNextTick()
        {
            _timer = new ScheduleTimer(500, Tick);
        }
    }
}