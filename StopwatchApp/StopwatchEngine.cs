using System;

namespace StopwatchApp
{
    /// <summary>
    /// Core stopwatch logic — tracks elapsed time, manages running/paused state,
    /// and formats the output as hh:mm:ss. Kept separate from the UI for testability.
    /// </summary>
    public class StopwatchEngine
    {
        private int _elapsedSeconds;
        private bool _isRunning;
        private bool _isPaused;
        private string _lastRecordedTime;

        /// <summary>Gets total elapsed seconds since the stopwatch started.</summary>
        public int ElapsedSeconds => _elapsedSeconds;

        /// <summary>True if the stopwatch is actively counting.</summary>
        public bool IsRunning => _isRunning;

        /// <summary>True if the stopwatch is paused (time preserved but not ticking).</summary>
        public bool IsPaused => _isPaused;

        /// <summary>Holds the formatted time from the last stop, so we can display it afterwards.</summary>
        public string LastRecordedTime => _lastRecordedTime;

        /// <summary>
        /// Sets up a fresh stopwatch with everything at zero.
        /// </summary>
        public StopwatchEngine()
        {
            _elapsedSeconds = 0;
            _isRunning = false;
            _isPaused = false;
            _lastRecordedTime = "00:00:00";
        }

        /// <summary>
        /// Starts the stopwatch from zero. Ignored if already running.
        /// </summary>
        public void Start()
        {
            if (_isRunning)
                return;

            _elapsedSeconds = 0;
            _isRunning = true;
            _isPaused = false;
        }

        /// <summary>
        /// Pauses the stopwatch so the counter stops incrementing.
        /// Only works when running and not already paused.
        /// </summary>
        public void Pause()
        {
            if (!_isRunning || _isPaused)
                return;

            _isPaused = true;
        }

        /// <summary>
        /// Resumes the stopwatch from where it was paused.
        /// </summary>
        public void Resume()
        {
            if (!_isRunning || !_isPaused)
                return;

            _isPaused = false;
        }

        /// <summary>
        /// Resets the elapsed time back to zero without stopping the stopwatch.
        /// </summary>
        public void Reset()
        {
            _elapsedSeconds = 0;
            _isPaused = false;
        }

        /// <summary>
        /// Stops the stopwatch and saves the current time so it can be shown later.
        /// </summary>
        public void Stop()
        {
            if (!_isRunning)
                return;

            _lastRecordedTime = GetFormattedTime();
            _isRunning = false;
            _isPaused = false;
        }

        /// <summary>
        /// Called once per second by the UI timer. Adds a second if we're running.
        /// </summary>
        public void Tick()
        {
            if (_isRunning && !_isPaused)
            {
                _elapsedSeconds++;
            }
        }

        /// <summary>
        /// Converts elapsed seconds into hh:mm:ss format for display.
        /// </summary>
        /// <returns>Time string like "01:05:30".</returns>
        public string GetFormattedTime()
        {
            int hours = _elapsedSeconds / 3600;
            int minutes = (_elapsedSeconds % 3600) / 60;
            int seconds = _elapsedSeconds % 60;
            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        }
    }
}
