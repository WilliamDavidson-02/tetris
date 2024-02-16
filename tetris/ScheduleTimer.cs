using System;

namespace tetris
{
    public class ScheduleTimer : IDisposable
    {
        private bool Aborted { get; set;  }

        private bool _active = true;

        private long _start;
        private long _time;

        private System.Timers.Timer _timer;
        private readonly Action _action;

        public ScheduleTimer(int time, Action action)
        {
            _time = time;
            _action = () =>
            {
                _active = false;
                action();
            };
            
            Resume();
        }

        public void Abort()
        {
            Aborted = true;
            Invalidate();
        }

        public void Pause()
        {
            if (!_active && Aborted) return;
            
            Invalidate();
            _time = Math.Max(1, _time - (DateTimeOffset.Now.ToUnixTimeMilliseconds() - _start));
        }

        public void Resume()
        {
            if (!_active && Aborted) return;
            
            _start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _timer = new System.Timers.Timer(_time);
            _timer.Elapsed += (sender, arg) => _action();
            _timer.AutoReset = false;
            _timer.Start();
        }

        private void Invalidate()
        {
            if (_timer == null) return;

            _timer.Enabled = false;
            _timer.Close();
            _timer = null;
        }

        public void Dispose() => Invalidate();
    }
}