using System;

namespace StopwatchApp
{
    /// <summary>
    /// Handles all the core stopwatch logic — tracking elapsed seconds,
    /// managing state (running, paused, stopped), and formatting the display.
    /// Separated from the UI so we can test it independently.
    /// </summary>
    public class StopwatchEngine
    {
        private int _elapsedSeconds;
        private bool _isRunning;
        private bool _isPaused;
        private string _lastRecordedTime;

        /// <summary>
        /// Gets the total number of seconds that have elapsed on the stopwatch.
        /// </summary>
        public int ElapsedSeconds => _elapsedSeconds;

        /// <summary>
        /// True when the stopwatch is actively counting.
        /// </summary>
        public bool IsRunning => _isRunning;

        /// <summary>
        /// True when the stopwatch has been paused (time preserved but not counting).
        /// </summary>
        public bool IsPaused => _isPaused;

        /// <summary>
        /// Stores the time string from the last stop action, so we can show it after stopping.
        /// </summary>
        public string LastRecordedTime => _lastRecordedTime;

        /// <summary>
        /// Sets up a fresh stopwatch with everything zeroed out.
        /// </summary>
        public StopwatchEngine()
        {
            _elapsedSeconds = 0;
            _isRunning = false;
            _isPaused = false;
            _lastRecordedTime = "00:00:00";
        }

        /// <summary>
        /// Starts the stopwatch from zero. If it's already running we just ignore it.
        /// </summary>
        public void Start()
        {
            if (_isRunning)
                return;

            // fresh start — wipe the counter
            _elapsedSeconds = 0;
            _isRunning = true;
            _isPaused = false;
        }

        /// <summary>
        /// Pauses the stopwatch so the time stops incrementing.
        /// Only works if it's currently running and not already paused.
        /// </summary>
        public void Pause()
        {
            if (!_isRunning || _isPaused)
                return;

            _isPaused = true;
        }

        /// <summary>
        /// Picks up from where we paused.
        /// Does nothing if we're not actually paused.
        /// </summary>
        public void Resume()
        {
            if (!_isRunning || !_isPaused)
                return;

            _isPaused = false;
        }

        /// <summary>
        /// Brings everything back to 00:00:00 but keeps the stopwatch in a running state.
        /// Useful when the user wants to restart without pressing stop then start.
        /// </summary>
        public void Reset()
        {
            _elapsedSeconds = 0;
            _isPaused = false;
            // keep running status as-is so the timer continues from 0
        }

        /// <summary>
        /// Stops the stopwatch completely and saves the last displayed time.
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
        /// Called once per second by the UI timer to increment the counter.
        /// Only ticks if we're running and not paused.
        /// </summary>
        public void Tick()
        {
            if (_isRunning && !_isPaused)
            {
                _elapsedSeconds++;
            }
        }

        /// <summary>
        /// Converts the elapsed seconds into the hh:mm:ss format.
        /// We break it down manually so it's clear what's happening.
        /// </summary>
        /// <returns>Time formatted as "00:00:00".</returns>
        public string GetFormattedTime()
        {
            int hours = _elapsedSeconds / 3600;
            int minutes = (_elapsedSeconds % 3600) / 60;
            int seconds = _elapsedSeconds % 60;

            // pad each part to 2 digits
            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        }
    }
}
