using System;

namespace StopwatchApp
{
    public class StopwatchEngine
    {
        private int _elapsedSeconds;
        private bool _isRunning;
        private bool _isPaused;
        private string _lastRecordedTime;

        public int ElapsedSeconds => _elapsedSeconds;
        public bool IsRunning => _isRunning;
        public bool IsPaused => _isPaused;
        public string LastRecordedTime => _lastRecordedTime;

        public StopwatchEngine()
        {
            _elapsedSeconds = 0;
            _isRunning = false;
            _isPaused = false;
            _lastRecordedTime = "00:00:00";
        }

        public void Start()
        {
            if (_isRunning)
                return;

            _elapsedSeconds = 0;
            _isRunning = true;
            _isPaused = false;
        }

        public void Pause()
        {
            if (!_isRunning || _isPaused)
                return;

            _isPaused = true;
        }

        public void Resume()
        {
            if (!_isRunning || !_isPaused)
                return;

            _isPaused = false;
        }

        public void Reset()
        {
            _elapsedSeconds = 0;
            _isPaused = false;
        }

        public void Stop()
        {
            if (!_isRunning)
                return;

            _lastRecordedTime = GetFormattedTime();
            _isRunning = false;
            _isPaused = false;
        }

        public void Tick()
        {
            if (_isRunning && !_isPaused)
            {
                _elapsedSeconds++;
            }
        }

        public string GetFormattedTime()
        {
            int hours = _elapsedSeconds / 3600;
            int minutes = (_elapsedSeconds % 3600) / 60;
            int seconds = _elapsedSeconds % 60;
            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        }
    }
}
