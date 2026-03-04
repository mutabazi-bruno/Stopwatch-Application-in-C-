using System;
using System.Windows.Forms;

namespace StopwatchApp
{
    /// <summary>
    /// Main form for the Stopwatch application.
    /// Hooks up the buttons to the StopwatchEngine and updates the display every second.
    /// </summary>
    public partial class Form1 : Form
    {
        private StopwatchEngine _engine;

        /// <summary>
        /// Initializes the form and creates a new engine instance.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            _engine = new StopwatchEngine();
        }

        /// <summary>
        /// Fires every second to tick the engine and refresh the time label.
        /// </summary>
        private void TickTimer_Tick(object? sender, EventArgs e)
        {
            _engine.Tick();
            UpdateDisplay();
        }

        /// <summary>
        /// Starts the stopwatch from 00:00:00 and kicks off the timer.
        /// </summary>
        private void BtnStart_Click(object? sender, EventArgs e)
        {
            _engine.Start();
            tickTimer.Start();

            UpdateDisplay();
            SetButtonStates(running: true, paused: false);
            lblStatus.Text = "Running...";
        }

        /// <summary>
        /// Pauses the stopwatch — time stops but isn't lost.
        /// </summary>
        private void BtnPause_Click(object? sender, EventArgs e)
        {
            _engine.Pause();
            tickTimer.Stop();

            SetButtonStates(running: true, paused: true);
            lblStatus.Text = "Paused at " + _engine.GetFormattedTime();
        }

        /// <summary>
        /// Resumes from wherever we left off.
        /// </summary>
        private void BtnResume_Click(object? sender, EventArgs e)
        {
            _engine.Resume();
            tickTimer.Start();

            SetButtonStates(running: true, paused: false);
            lblStatus.Text = "Running...";
        }

        /// <summary>
        /// Resets the timer back to 00:00:00 without stopping.
        /// </summary>
        private void BtnReset_Click(object? sender, EventArgs e)
        {
            _engine.Reset();
            UpdateDisplay();
            lblStatus.Text = "Reset — timer restarted from 00:00:00";
        }

        /// <summary>
        /// Stops the stopwatch entirely and shows the final recorded time.
        /// </summary>
        private void BtnStop_Click(object? sender, EventArgs e)
        {
            _engine.Stop();
            tickTimer.Stop();

            UpdateDisplay();
            SetButtonStates(running: false, paused: false);
            lblStatus.Text = "Stopped at " + _engine.LastRecordedTime;
        }

        /// <summary>
        /// Refreshes the timer label with the current formatted time from the engine.
        /// </summary>
        private void UpdateDisplay()
        {
            lblTimer.Text = _engine.GetFormattedTime();
        }

        /// <summary>
        /// Enables/disables buttons based on whether the stopwatch is running or paused.
        /// Keeps the UI intuitive — you can't pause something that isn't running, etc.
        /// </summary>
        /// <param name="running">Whether the stopwatch is currently active.</param>
        /// <param name="paused">Whether the stopwatch is currently paused.</param>
        private void SetButtonStates(bool running, bool paused)
        {
            if (running)
            {
                btnStart.Enabled = false;
                btnPause.Enabled = !paused;
                btnResume.Enabled = paused;
                btnReset.Enabled = true;
                btnStop.Enabled = true;
            }
            else
            {
                // stopped state — only start is available
                btnStart.Enabled = true;
                btnPause.Enabled = false;
                btnResume.Enabled = false;
                btnReset.Enabled = false;
                btnStop.Enabled = false;
            }
        }
    }
}
